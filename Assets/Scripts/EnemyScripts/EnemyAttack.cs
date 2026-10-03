using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] private int damage = 1; // Damage dealt to the player
    [SerializeField] private float attackRadius = 2f; // Range within which the enemy can attack
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyAttackVictim;
    [SerializeField] private float damageInterval = 1.5f; // Time between attacks to prevent continuous damage
    [SerializeField] private bool canDamage = true; // Flag to control attack timing

    private EnemyController enemyController;

    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
        if (attackPoint == null)
        {
            Debug.LogError("Attack point is not assigned in the inspector.");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
     // reference of the attack point

    void Update()
    {
        if (canDamage && IsEnemyAttacking())
        {
            canDamage = false; // Prevent further damage until the interval has passed
            Invoke(nameof(ResetDamage), damageInterval); // Reset the canDamage flag after the interval
            CheckForPlayerInDamageRange();
        } 
    }

    private void CheckForPlayerInDamageRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, attackRadius);//, enemyAttackVictim);
        foreach(var collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                print("Enemy hit a target!");
                collider.GetComponent<HealthController>()?.TakeDamage(damage);
                break;
            }
        }
    }

    private void ResetDamage()
    {
        canDamage = true; // Allow damage again after the interval
    }

    private bool IsEnemyAttacking()
    {
        return enemyController.currentState == EnemyController.EnemyState.Attack;
    }
    
}
