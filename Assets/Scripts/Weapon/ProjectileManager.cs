using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private float delayBeforeDestroy = 5f; // Time in seconds before the projectile is destroyed
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private bool isExplosive = false; // Whether the projectile is explosive or not
    [SerializeField] private List<ParticleSystem> explosionEffects; // List of particle systems for explosion effects
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
        
    }

    void Detonate()
    {
        StartCoroutine(_DetonateEffects());

        IEnumerator _DetonateEffects()
        {
            if (isExplosive)
            {
                yield return new WaitForSeconds(0.1f); // Wait for a short moment before detonating
                
            }
            // yield return null; // Wait for the next frame
            Destroy(gameObject); // Destroy the projectile after detonation
        }
    }

    
}
