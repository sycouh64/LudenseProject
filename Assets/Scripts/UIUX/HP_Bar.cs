using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class HP_Bar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private IHasHP target;
    [SerializeField] private float maxHP;
    [SerializeField] private float currentHP;
    //private EnemyScript enemyHP;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        target = GetComponentInParent<IHasHP>();
    }

    private void Update()
    {
        if (target == null) return;
        slider.value = target.CurrentHP;
    }
}
