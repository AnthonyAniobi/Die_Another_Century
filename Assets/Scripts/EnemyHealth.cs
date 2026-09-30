using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3; // maximum health of the enemy
    [SerializeField] private int currentHealth; 

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // Implement your damage logic here
        Debug.Log("Enemy took " + damage + " damage!");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Implement your death logic here
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }
    
}
