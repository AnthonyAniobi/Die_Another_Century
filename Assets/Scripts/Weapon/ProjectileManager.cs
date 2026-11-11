using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private float delayBeforeDestroy = 5f; // Time in seconds before the projectile is destroyed
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private bool isExplosive = false; // Whether the projectile is explosive or not
    [SerializeField] private List<ParticleSystem> explosionEffects; // List of particle systems for explosion effects
    [SerializeField] private GameObject fireEffectObject; // The fire effect object to activate when the projectile is fired
    public void Activate()
    {
        StartCoroutine(
            DeactivateAfterDelay(delayBeforeDestroy)
        );
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
        Detonate();
    }

    void Detonate()
    {
        StartCoroutine(_DetonateEffects());

        IEnumerator _DetonateEffects()
        {
            if (isExplosive)
            {
                if (fireEffectObject != null)
                {
                    GetComponent<Rigidbody>().isKinematic = true; // Make the projectile kinematic to stop it from moving
                    // GetComponent<Rigidbody>().velocity = Vector3.zero; // Stop the projectile's movement
                    fireEffectObject.GetComponent<ParticleSystem>().Play(); // Play the fire effect
                    yield return new WaitForSeconds(2f); // Small delay before playing the fire effect
                }
            }
            // yield return null; // Wait for the next frame
            Destroy(gameObject); // Destroy the projectile after detonation
        }
    }

    
}
