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
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        Debug.Log($"{healthType} took " + damage + " damage!");
        animator?.SetTrigger("HIT");
        currentHealth -= damage;
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
        if(healthType == HealthType.NPC)
        {
            GameLevelManager.instance.EndGame(false, "The civilian was killed!");
        }else if(healthType == HealthType.player)
        {
            GameLevelManager.instance.EndGame(false, "You were killed!");
        }
    }


}
