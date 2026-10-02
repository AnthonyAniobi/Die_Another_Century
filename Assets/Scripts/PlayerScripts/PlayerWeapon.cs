using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
[SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 500f;

    [SerializeField] private float spreadIntensity = 0.1f;

    [SerializeField] private float shotDelay = 0.12f;

    private Animator animator;
    
    public bool  readyToShoot = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameInputManager.instance.IsAttackButtonPressed() && readyToShoot)
        {
            FireWeapon();
        }
    }



    private void FireWeapon()
    {
        readyToShoot = false;
        //
        animator.SetTrigger("RECOIL");
        if(SoundManager.instance != null)
        {
            SoundManager.instance.PlayShootingSound();
        }

        Vector3 shootingDirection = GetBulletDirection().normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.transform.forward = shootingDirection;
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletSpeed, ForceMode.Impulse);
        
        Invoke("ResetShot", shotDelay);
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

    private void ResetShot()
    {
        readyToShoot = true;
        
    }


}
