using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3; // maximum health of the enemy
    [SerializeField] private HealthType healthType; // type of health (Enemy, NPC, player)
    private Animator animator;
    private int currentHealth; 
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

        Debug.Log("Enemy took " + damage + " damage!");
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
        animator?.SetBool("DIE", true);
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

    public void EndEnemyAttack()
    {
        EnemyController enemyController = GetComponentInParent<EnemyController>();
        enemyController?.StopAttack();
    }

    public void EnemyAttackHit()
    {
        EnemyController enemyController = GetComponentInParent<EnemyController>();
        enemyController?.HitPlayer();
    }
}
