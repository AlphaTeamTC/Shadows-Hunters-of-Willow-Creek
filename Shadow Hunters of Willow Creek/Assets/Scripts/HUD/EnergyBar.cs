using UnityEngine;
using UnityEngine.UI;


public class EnergyBar : MonoBehaviour
{
    // Slider in the UI
    [SerializeField] private Slider slider;

    // Initializes the max value the energy can have 
    public void SetMaxEnergy(float energy)
    {
        // Max energy value
        slider.maxValue = energy;
        // Each character starts with 0 energy
        slider.value = 0;
    }

    // Set the energy to a given value
    public void SetEnergy(float energy)
    {
        slider.value = energy;
    }
}
