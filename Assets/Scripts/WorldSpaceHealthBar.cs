using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class WorldSpaceHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float heightOffset = 1.5f;
    
    private PlayerHealth playerHealth;
    private EnemyScript enemyHealth;

    private void Start()
    {
        // Search parent hierarchy for health components
        playerHealth = GetComponentInParent<PlayerHealth>();
        enemyHealth = GetComponentInParent<EnemyScript>();

        if (slider == null)
        {
            slider = GetComponentInChildren<Slider>();
        }
    }

    private void Update()
    {
        if (playerHealth != null)
        {
            slider.maxValue = playerHealth.maxHealth;
            slider.value = playerHealth.CurrentHealth;
        }
        else if (enemyHealth != null)
        {
            slider.maxValue = enemyHealth.maxHp;
            slider.value = enemyHealth.enemyHp;
        }
    }

    private void LateUpdate()
    {
        if (transform.parent != null)
        {
            // Always keep the health bar centered horizontally and offset vertically
            transform.localPosition = new Vector3(0f, heightOffset, 0f);

            // Cancel out parent's scale to keep the health bar size and orientation constant
            Vector3 parentScale = transform.parent.localScale;
            float scaleX = parentScale.x != 0 ? Mathf.Abs(parentScale.x) : 1f;
            float scaleY = parentScale.y != 0 ? Mathf.Abs(parentScale.y) : 1f;
            float scaleZ = parentScale.z != 0 ? Mathf.Abs(parentScale.z) : 1f;

            transform.localScale = new Vector3(1f / scaleX, 1f / scaleY, 1f / scaleZ);
        }
    }
}