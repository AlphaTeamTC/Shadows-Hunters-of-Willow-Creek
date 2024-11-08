using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Slider in the UI
    [SerializeField] private Slider slider;

    // Initializes the max value the health can have 
    public void SetMaxHealth(float health)
    {
        // Max health value
        slider.maxValue = health;
        // Each character health with the maximum value
        slider.value = health;
    }

    // Set the health to a given value
    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
