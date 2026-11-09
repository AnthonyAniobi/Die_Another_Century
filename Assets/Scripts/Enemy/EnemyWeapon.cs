using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming the player has a HealthController component
            HealthController playerHealth = other.GetComponent<HealthController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1); // Assuming 1 is the damage value for the enemy weapon
                Debug.Log("Player took damage from enemy weapon!");
            }
        }
    }
}
