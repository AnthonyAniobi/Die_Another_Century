using System.Collections;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private float delayBeforeDestroy = 5f; // Time in seconds before the projectile is destroyed
    [SerializeField] private WeaponType weaponType;
    public void Activate()
    {
        StartCoroutine(
            DeactivateAfterDelay(delayBeforeDestroy)
        ); // Deactivate after 5 seconds
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(
                    weaponType.GetWeaponDamage(), 
                    collision.contacts[0].point
                ); // Apply damage to the enemy
            }
        }
    }

    IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    
}
