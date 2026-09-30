using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3; // maximum health of the enemy
    [SerializeField] private HealthType healthType; // type of health (Enemy, NPC, player)
    private int currentHealth; 

    public enum HealthType
    {
        Enemy,
        NPC, 
        player,
    }

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
        if(healthType == HealthType.NPC)
        {
            GameLevelManager.instance.EndGame(false, "The civilian was killed!");
        }else if(healthType == HealthType.player)
        {
            GameLevelManager.instance.EndGame(false, "You were killed!");
        }
    }
}
