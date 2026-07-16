using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
#endif

namespace Unity.AI.Assistant.PlayModeTest
{
    [InitializeOnLoad]
    internal static class PlayModeTestRunner
    {
        private const string StateKey = "PlayModeTest.State";
        private const string ResultKey = "PlayModeTest.Result";
        private const string ScriptPathKey = "PlayModeTest.ScriptPath";
        private const string SentinelLog = "PLAY_MODE_TEST_COMPLETE";

        private static readonly int WaitFrames = SessionState.GetInt("PlayModeTest.WaitFrames", 5);
        private static readonly float TestTimeout = SessionState.GetFloat("PlayModeTest.TestTimeout", 15.0f);

        private static List<string> _capturedLogs = new List<string>();
        private const int MaxCapturedLogs = 50;

        static PlayModeTestRunner()
        {
            string state = SessionState.GetString(StateKey, "Idle");

            switch (state)
            {
                case "Idle":
                    break;

                case "WaitingForCompile":
                    Debug.Log("[PlayModeTest] Bootstrap compiled. Scheduling Play Mode entry.");
                    EditorApplication.delayCall += () =>
                    {
                        SessionState.SetString(StateKey, "EnteringPlayMode");
                        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
                        EditorApplication.isPlaying = true;
                    };
                    break;

                case "EnteringPlayMode":
                    EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
                    if (EditorApplication.isPlaying)
                    {
                        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
                        SessionState.SetString(StateKey, "InPlayMode");
                        EditorApplication.update += WaitFramesThenRun;
                    }
                    break;

                case "InPlayMode":
                    if (EditorApplication.isPlaying)
                    {
                        EditorApplication.update += WaitFramesThenRun;
                    }
                    break;

                case "Done":
                    Debug.Log(SentinelLog);
                    EditorApplication.delayCall += SelfDestruct;
                    break;
            }
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange change)
        {
            if (change == PlayModeStateChange.EnteredPlayMode)
            {
                EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
                SessionState.SetString(StateKey, "InPlayMode");
                EditorApplication.update += WaitFramesThenRun;
            }
        }

        private static int _frameCount = 0;
        private static bool _setupDone = false;
        private static bool _testDone = false;
        private static double _testStartTime = 0;

        private static void WaitFramesThenRun()
        {
            _frameCount++;
            if (_frameCount < WaitFrames) return;
            if (_testDone) return;

            if (!_setupDone)
            {
                _setupDone = true;
                Application.logMessageReceived += OnLogMessage;
                _testStartTime = EditorApplication.timeSinceStartup;
                try
                {
                    Setup();
                }
                catch (System.Exception e)
                {
                    Debug.LogError("[PlayModeTest] Setup threw exception: " + e);
                    FinishTest(true, e.Message);
                    return;
                }
                return;
            }

            float elapsed = (float)(EditorApplication.timeSinceStartup - _testStartTime);
            bool timedOut = elapsed >= TestTimeout;

            try
            {
                bool complete = Tick(elapsed);
                if (complete || timedOut)
                {
                    if (timedOut && !complete)
                        Debug.LogWarning("[PlayModeTest] Test timed out after " + elapsed + "s");
                    FinishTest(timedOut && !complete, timedOut ? "Test timed out" : null);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("[PlayModeTest] Tick threw exception: " + e);
                FinishTest(true, e.Message);
            }
        }

        private static void FinishTest(bool isError, string errorMessage)
        {
            _testDone = true;
            EditorApplication.update -= WaitFramesThenRun;
            Application.logMessageReceived -= OnLogMessage;

            string resultJson;
            try
            {
                resultJson = GetResult();
            }
            catch (System.Exception e)
            {
                resultJson = JsonUtility.ToJson(new TestResult
                {
                    success = false,
                    error = "GetResult() threw: " + e.Message,
                    logs = _capturedLogs.ToArray()
                });
            }

            if (isError && errorMessage != null)
            {
                resultJson = JsonUtility.ToJson(new TestResult
                {
                    success = false,
                    error = errorMessage,
                    logs = _capturedLogs.ToArray()
                });
            }

            SessionState.SetString(ResultKey, resultJson);
            SessionState.SetString(StateKey, "Done");
            EditorApplication.isPlaying = false;
        }

        private static void OnLogMessage(string message, string stackTrace, LogType type)
        {
            if (_capturedLogs.Count >= MaxCapturedLogs) return;
            if (type == LogType.Error || type == LogType.Exception ||
                message.Contains("[Test]") || message.Contains("TEST_RESULT"))
            {
                _capturedLogs.Add("[" + type + "] " + message);
            }
        }

        private static void SelfDestruct()
        {
            string scriptPath = SessionState.GetString(ScriptPathKey, "");
            if (!string.IsNullOrEmpty(scriptPath) && AssetDatabase.AssetPathExists(scriptPath))
            {
                AssetDatabase.DeleteAsset(scriptPath);
            }
            SessionState.EraseString(StateKey);
            SessionState.EraseString(ScriptPathKey);
        }

        [System.Serializable]
        private class TestResult
        {
            public bool success;
            public string error;
            public string[] logs;
            public bool foundPanel;
            public bool activeAfterFirstPress;
            public bool activeAfterSecondPress;
        }

        // -------- Test state --------
        private static GameObject _mapPanel;
        private static int _phase = 0;
        private static bool _foundPanel = false;
        private static bool _activeAfterFirst = false;
        private static bool _activeAfterSecond = false;

        private static void Setup()
        {
            // Find the MapPanel inside the MapCanvas
            GameObject canvas = GameObject.Find("MapCanvas");
            if (canvas != null)
            {
                Transform t = canvas.transform.Find("MapPanel");
                if (t != null) _mapPanel = t.gameObject;
            }
            _foundPanel = _mapPanel != null;
            Debug.Log("[Test] MapPanel found: " + _foundPanel);
            if (_mapPanel != null)
                Debug.Log("[Test] Initial active state: " + _mapPanel.activeSelf);
        }

        private static bool Tick(float elapsed)
        {
            if (!_foundPanel) return true; // nothing to test

#if ENABLE_INPUT_SYSTEM
            // Phase 0: press M (first time)
            if (_phase == 0 && elapsed > 0.3f)
            {
                PressKey(Key.M, true);
                _phase = 1;
                return false;
            }
            // Phase 1: release M
            if (_phase == 1 && elapsed > 0.5f)
            {
                PressKey(Key.M, false);
                _phase = 2;
                return false;
            }
            // Phase 2: check active after first press
            if (_phase == 2 && elapsed > 0.9f)
            {
                _activeAfterFirst = _mapPanel.activeSelf;
                Debug.Log("[Test] Active after first M press: " + _activeAfterFirst);
                PressKey(Key.M, true);
                _phase = 3;
                return false;
            }
            // Phase 3: release M (second time)
            if (_phase == 3 && elapsed > 1.1f)
            {
                PressKey(Key.M, false);
                _phase = 4;
                return false;
            }
            // Phase 4: check active after second press
            if (_phase == 4 && elapsed > 1.5f)
            {
                _activeAfterSecond = _mapPanel.activeSelf;
                Debug.Log("[Test] Active after second M press: " + _activeAfterSecond);
                return true;
            }
            return false;
#else
            Debug.Log("[Test] New Input System not enabled — cannot simulate key");
            return true;
#endif
        }

#if ENABLE_INPUT_SYSTEM
        private static void PressKey(Key key, bool pressed)
        {
            var keyboard = InputSystem.GetDevice<Keyboard>();
            if (keyboard == null)
            {
                Debug.LogError("[Test] Keyboard device not found");
                return;
            }
            using (StateEvent.From(keyboard, out var eventPtr))
            {
                keyboard[key].WriteValueIntoEvent(pressed ? 1f : 0f, eventPtr);
                InputSystem.QueueEvent(eventPtr);
            }
        }
#endif

        private static string GetResult()
        {
            bool success = _foundPanel && _activeAfterFirst && !_activeAfterSecond;
            var result = new TestResult
            {
                success = success,
                foundPanel = _foundPanel,
                activeAfterFirstPress = _activeAfterFirst,
                activeAfterSecondPress = _activeAfterSecond,
                logs = _capturedLogs.ToArray()
            };
            Debug.Log("[Test] TEST_RESULT success=" + success);
            return JsonUtility.ToJson(result);
        }
    }
}
