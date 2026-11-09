using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider; // Reference to the UI Slider for health
    private int maxHealth;
    private int currentHealth;

    public void Initialize(int maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        healthSlider.value = healthSliderValue; // Initialize the slider value
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = healthSliderValue; // Update the slider value

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle enemy death (e.g., play animation, disable enemy, etc.)
        Debug.Log("Enemy has died.");
        Destroy(gameObject); // Destroy the enemy GameObject
    }

    private float healthSliderValue => (float)currentHealth / maxHealth;
}
