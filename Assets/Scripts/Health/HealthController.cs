using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3; // maximum health of the enemy
    [SerializeField] private HealthType healthType; // type of health (Enemy, NPC, player)
    private Animator animator;
    [SerializeField] private int currentHealth; 
    private bool isDead;

    public enum HealthType
    {
        Enemy,
        NPC, 
        player,
    }

    void Start()
    {
        currentHealth = maxHealth;
        
        animator = GetComponent<Animator>();
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            TakeDamage(1, other.transform.forward); // Assuming 1 is the damage value for the bullet
            Destroy(other.gameObject); // Destroy the bullet after it hits
            Debug.Log($"{healthType} took damage from bullet!");
        }
        
    }

    public void TakeDamage(int damage, Vector3 hitDirection = default)
    {
        if (isDead)
        {
            return;
        }
        currentHealth -= damage;

        if(healthType == HealthType.Enemy)
        {
            GetComponentInParent<EnemyController>().EnemyHit(hitDirection);
        }
        else if(healthType == HealthType.NPC)
        {
            animator.SetTrigger("HIT");
        }
        else if(healthType == HealthType.player)
        {
            animator.SetTrigger("HIT");
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetBool("DIE", true);
        Debug.Log($"{healthType} has died!");
        Destroy(gameObject);
        // if(healthType == HealthType.NPC)
        // {
        //     GameLevelManager.instance.EndGame(false, "The civilian was killed!");
        // }else if(healthType == HealthType.player)
        // {
        //     GameLevelManager.instance.EndGame(false, "You were killed!");
        // }else if(healthType == HealthType.Enemy)
        // {
        //     // GameLevelManager.instance.UpdateEnemyCount();
        // }
    }


}
