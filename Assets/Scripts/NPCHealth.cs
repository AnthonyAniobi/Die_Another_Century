using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1; // maximum health of the enemy
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
        Debug.Log("NPC died!");
        Destroy(gameObject);
        GameLevelManager.instance.EndGame(false, "The civilian was killed!");
    }
    
}
