using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponType weaponType;
    public Transform spawnPoint;
    public GameObject projectilePrefab;
    public Vector3 aimDirection; // forward direction of the weapon when aiming
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private GameObject fireEffectObject;
    [SerializeField] private AudioSource audioSource;

    private GameObject fixedAmmo;
    private Collider weaponCollider;
    
    
    void Start()
    {
        // ResetAmmo();
        TryGetComponent(out weaponCollider);
    }

    public void StartAttack(bool isAimedWeapon)
    {
        if(weaponCollider != null)
        {
            weaponCollider.enabled = true;
        }
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        if (fireEffectObject != null)
        {
            fireEffectObject.SetActive(true);
        }
        if (weaponType.UsesProjectile()) // Check if the weapon type uses projectiles
        {
            StartCoroutine(FireProjectile(weaponType.GetWeaponSpawnDelay(), isAimedWeapon));
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
        if(weaponType == WeaponType.RPG || weaponType == WeaponType.Grenade)
        {
            if (fixedAmmo != null && fixedAmmo.transform.parent == spawnPoint)
            {
                Destroy(fixedAmmo);
            }

            fixedAmmo = Instantiate(
                projectilePrefab, 
                spawnPoint.position, 
                spawnPoint.rotation,
                spawnPoint
            );
            fixedAmmo.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            fixedAmmo.SetActive(true);
            Rigidbody rigidbody = fixedAmmo.GetComponent<Rigidbody>();
            // rigidbody.freezeRotation = true;
            rigidbody.isKinematic = true;
            // rigidbody.useGravity = false;
            rigidbody.detectCollisions = false;
            fixedAmmo.GetComponent<Collider>().enabled = false;
            fixedAmmo.GetComponent<TrailRenderer>().enabled = false; // Disable trail renderer until fired
        }
    }

    System.Collections.IEnumerator FireProjectile(float delay, bool isAimedWeapon)
    {
        yield return new WaitForSeconds(delay);
        
        Vector3 direction;
        if (isAimedWeapon)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if(Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                direction = (hitInfo.point - spawnPoint.position).normalized;
            }
            else
            {
                direction =   (ray.GetPoint(10f) - spawnPoint.position).normalized; // Use a point far away in the direction of the camera
            }
        }
        else
        {
            direction = spawnPoint.forward.normalized * -1; // Use the opposite direction
        }
        
        Rigidbody rb;
        ProjectileManager projectileManager;
        if((weaponType == WeaponType.RPG || weaponType == WeaponType.Grenade) && fixedAmmo != null)
        {
            rb = fixedAmmo.GetComponent<Rigidbody>();
            projectileManager = fixedAmmo.GetComponent<ProjectileManager>();
            fixedAmmo.GetComponent<TrailRenderer>().enabled = true; // Enable trail renderer when fired
            fixedAmmo.transform.SetParent(null, true);
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.detectCollisions = true;
            fixedAmmo.GetComponent<Collider>().enabled = true;
        }
        else
        {   
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(direction));
            projectileManager = projectile.GetComponent<ProjectileManager>();
            rb = projectile.GetComponent<Rigidbody>();
        }
        if(projectileManager != null)
        {
            projectileManager.Activate(); // Start the projectile's lifecycle
        }
        if (rb != null)
        {
            rb.useGravity = true; // Enable gravity for the projectile
            rb.AddForce(direction * weaponType.GetWeaponFireForce(), ForceMode.Impulse);
        }
    }
}
