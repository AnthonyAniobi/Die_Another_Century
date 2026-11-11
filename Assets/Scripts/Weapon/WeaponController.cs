using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponType weaponType;
    public Transform spawnPoint;
    public GameObject projectilePrefab;

    private GameObject fixedAmmo;
    private Collider weaponCollider;
    
    void Start()
    {
        ResetAmmo();
        TryGetComponent<Collider>(out weaponCollider);
    }

    public void StartAttack()
    {
        if(weaponCollider != null)
        {
            GetComponent<Collider>().enabled = true;
        }

        Vector3 direction = (spawnPoint.position - Camera.main.transform.forward).normalized;
        Rigidbody rb;
        if(weaponType == WeaponType.RPG && fixedAmmo != null)
        {
            rb = fixedAmmo.GetComponent<Rigidbody>();
            fixedAmmo.transform.parent = null; // Detach the projectile from the spawn point
        }
        else
        {   
            Debug.Log("Firing projectile from weapon: " + weaponType);
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(direction));
            rb = projectile.GetComponent<Rigidbody>();
        }
        if (rb != null)
        {
            rb.useGravity = true; // Enable gravity for the projectile
            Debug.Log("Applying force to projectile for weapon: " + weaponType);
            rb.AddForce(direction * weaponType.GetWeaponFireForce(), ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("Rigidbody not found on the projectile prefab for weapon: " + weaponType);
        }
    }

    public void StopAttack()
    {
        if(weaponCollider != null)
        {
            GetComponent<Collider>().enabled = false;
        }
        ResetAmmo();
    }

   

    void OnTriggerEnter(Collider other)
    {
        // check for weapon collision with enemy
        // this is only valid for sword type weapons
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(weaponType.GetWeaponDamage(), gameObject.transform.position);
            }
        }
    }

    void ResetAmmo()
    {
        if(weaponType == WeaponType.RPG)
        {
            fixedAmmo = Instantiate(
                projectilePrefab, 
                spawnPoint.position, 
                spawnPoint.rotation,
                spawnPoint.transform
            );
            fixedAmmo.transform.localPosition = Vector3.zero;
            fixedAmmo.SetActive(true);
            fixedAmmo.GetComponent<Rigidbody>().useGravity = false;
            
        }
    }
}
