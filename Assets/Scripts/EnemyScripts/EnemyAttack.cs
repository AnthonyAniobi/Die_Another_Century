using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] private int damage = 1; // Damage dealt to the player
    [SerializeField] private float attackRadius = 2f; // Range within which the enemy can attack
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyAttackVictim;

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
     // reference of the attack point

    public void AttackPlayer()
    {
        print("Enemy is attacking the player!");
        Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, attackRadius, enemyAttackVictim);
        foreach(var collider in hitColliders)
        {
            if (collider.CompareTag("NPC") || collider.CompareTag("Player"))
            {
                print("Enemy hit a target!");
                collider.GetComponent<HealthController>()?.TakeDamage(damage);
                break;
            }
        } 
    }

    
}
