using UnityEngine;

public class AmbStatistics : MonoBehaviour
{
    [SerializeField] private HealthBar healthbar;
    [SerializeField] private EnergyBar energybar;

    // Health
    private float maxHealth = 100;
    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    private float currentHealth;
    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    // Regeneration
    private bool isFullHealth;
    private float timeSinceRegen = 0;
    private float regenTime = 10f; //seconds
    private float regenAmount = 5f;
    public float RegenAmount
    {
        get { return regenAmount; }
        set { regenAmount = value; }
    }

    // Energy
    private float maxEnergy = 100;
    private float currentEnergy;
    public float CurrentEnergy
    {
        get { return currentEnergy; }
        set { currentEnergy = value; }
    }

    // Speed
    private float speed = 10f;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    // Strenght
    private float strenght;
    public float Strenght
    {
        get { return strenght; }
        set { strenght = value; }
    }

    // Magic
    private float magic;
    public float Magic
    {
        get { return magic; }
        set { magic = value; }
    }

    // Resistence
    private float resistence;
    public float Resistence
    {
        get { return resistence; }
        set { resistence = value; }
    }


    // Sets the maximum health and energy values
    // Sets the starting values
    public void Start(){
        currentHealth = maxHealth;
        currentEnergy = 0;
        healthbar.SetMaxHealth(maxHealth);
        energybar.SetMaxEnergy(maxEnergy);
        isFullHealth = true;
    }

    // Checks when the player should get the regen
    void Update(){

        // Check if is damaged
        if (!isFullHealth)
        {
            // Increment the timer by the time since the last frame
            timeSinceRegen += Time.deltaTime;

            // Check if the set interval have passed
            if (timeSinceRegen >= regenTime)
            {
                // Execute the function
                Heal(regenAmount);

                // Reset the timer
                timeSinceRegen = 0f;
            }
        }
    }

    // Adds the given value to the health up to the maximum and reflects the results
    public void Heal(float health)
    {
        if (currentHealth + health >= maxEnergy)
        {
            currentHealth = maxEnergy;
            healthbar.SetHealth(currentHealth);
            isFullHealth = true;
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

        isFullHealth = false;
        
        float hitDamage = damage - resistence;

        if (currentHealth - hitDamage <= 0)
        {
            currentHealth = 0;
            healthbar.SetHealth(currentHealth);
            return currentHealth;
            
            // Needs to display a death animation

        }
        else
        {
            currentHealth -= hitDamage;
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
