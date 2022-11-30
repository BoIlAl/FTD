using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetMaxHealth(short health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(short health)
    {
        slider.value = health;
    }
}
