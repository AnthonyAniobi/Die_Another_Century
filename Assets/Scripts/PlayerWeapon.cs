using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
[SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 500f;

    [SerializeField] private float spreadIntensity = 0.1f;
    [SerializeField] private bool allowResetShooting = true;

    [SerializeField] private float shootResetDelay = 0.12f;

    // [SerializeField] private GameObject muzzleFlash;

    [SerializeField] private float reloadTime;
    [SerializeField] private bool isReloading = false;


    private Animator animator;
    
    public bool  readyToShoot = true;

    
    
    void Start()
    {
        ResetShooting();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameLevelManager.instance != null && !GameLevelManager.instance.gameStarted)
        {
            return; // Do not process movement if the game hasn't started
        }

        if (GameLevelManager.instance.shootButton != null && GameLevelManager.instance.shootButton.IsPressed())

        
        // InputAction fireBullet = InputSystem.actions.FindAction("Attack");

        if (fireBullet.IsPressed() && readyToShoot)
        {
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        readyToShoot = false;
        //
        animator.SetTrigger("RECOIL");
        // muzzleFlash.GetComponent<ParticleSystem>().Play();
        SoundManager.instance.PlayShootingSound();

        Vector3 shootingDirection = GetBulletDirection().normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.transform.forward = shootingDirection;
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletSpeed, ForceMode.Impulse);
        
        if (allowResetShooting)
        {
            Invoke("ResetShooting", shootResetDelay);
            allowResetShooting = false;
        }
    }


   

    private Vector3 GetBulletDirection()
    {   
        // raycast from the center of the camera
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        
        Vector3 targetPoint;
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100f);
        }

        Vector3 direction = targetPoint - bulletSpawnPoint.position;
        float x = Random.Range(-spreadIntensity, spreadIntensity);
        float y = Random.Range(-spreadIntensity, spreadIntensity);
        direction += new Vector3(x, y, 0f);
        return direction;
    }

    private void ResetShooting()
    {
        readyToShoot = true;
        allowResetShooting = true;
    }


}
