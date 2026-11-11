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
        ResetAmmo();
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
            Vector3 direction;
            // if (isAimedWeapon)
            // {
            //     direction = (spawnPoint.position - Camera.main.transform.forward).normalized;
            // }
            // else
            // {
                direction = spawnPoint.forward.normalized * -1; // Use the opposite direction
            // }
            
            Rigidbody rb;
            if(weaponType == WeaponType.RPG && fixedAmmo != null)
            {
                rb = fixedAmmo.GetComponent<Rigidbody>();
                fixedAmmo.transform.parent = null; // Detach the projectile from the spawn point
                rb.isKinematic = false; // Make it respond to physics
            }
            else
            {   
                GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(direction));
                rb = projectile.GetComponent<Rigidbody>();
            }
            if (rb != null)
            {
                rb.useGravity = true; // Enable gravity for the projectile
                rb.AddForce(direction * weaponType.GetWeaponFireForce(), ForceMode.Impulse);
            }
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
            fixedAmmo.GetComponent<Rigidbody>().freezeRotation = true; // Freeze rotation to prevent it from falling over
            fixedAmmo.GetComponent<Rigidbody>().isKinematic = true; // Make it kinematic so it doesn't respond to physics until fired
            
        }
    }
}
