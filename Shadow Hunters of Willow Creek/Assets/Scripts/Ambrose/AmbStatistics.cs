using UnityEngine;

public class AmbStatistics : MonoBehaviour
{
    [SerializeField] private HealthBar healthbar;
    [SerializeField] private EnergyBar energybar;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float maxEnergy = 100;
    [SerializeField] private float currentHealth;
    [SerializeField] private float currentEnergy;


    // Sets the maximum health and energy values
    // Sets the starting values
    public void Start(){
        currentHealth = maxHealth;
        currentEnergy = 0;
        healthbar.SetMaxHealth(maxHealth);
        energybar.SetMaxEnergy(maxEnergy);
    }

    // Adds the given value to the health up to the maximum and reflects the results
    public void Heal(float health)
    {
        if (currentHealth + health > maxEnergy)
        {
            currentHealth = maxEnergy;
            healthbar.SetHealth(currentHealth);
        }
        else
        {
            currentHealth += health;
            healthbar.SetHealth(currentHealth);
        }
    }

    // Removes the given value from the health up to zero
    public float Damage(float damage)
    {
        GainEnergy(10);
        if (currentHealth - damage < 0)
        {
            currentHealth = 0;
            healthbar.SetHealth(currentHealth);
            return currentHealth;
            
            // Needs to display a death animation

        }
        else
        {
            currentHealth -= damage;
            healthbar.SetHealth(currentHealth);
            return currentHealth;
        }
    }

    // Adds the given value to the energy up to the maximum and reflects the results
    public void GainEnergy(float energy){
        if (currentEnergy + energy > maxEnergy)
        {
            currentEnergy = maxEnergy;
            energybar.SetEnergy(maxEnergy);
        }
        else
        {
            currentEnergy += energy;
            energybar.SetEnergy(currentEnergy);
        }
    }

    // Removes all energy since the only way to spend it is to use the ultimate ability
    public void SpendEnergy(){
        currentEnergy = 0;
        energybar.SetEnergy(currentEnergy);
    }
}
