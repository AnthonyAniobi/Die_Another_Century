using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
private float bulletLifeTime = 2.0f;

    void Start()
    {
        StartCoroutine(RemoveBullet());
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstruction"))
        {
            collision.gameObject.TryGetComponent<ObstructionController>(out var obstruction);
            if (obstruction != null)
            {
                obstruction.HitObstruction();
            }
            print($"Hit Target {collision.gameObject.name}");
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.TryGetComponent<HealthController>(out var enemy);
            if (enemy != null)
            {
                enemy.TakeDamage(1); // Assuming 1 is the damage value for the bullet
            }
            print($"Hit Target {collision.gameObject.name}");
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("NPC"))
        {
            collision.gameObject.TryGetComponent<HealthController>(out var npc);
            if (npc != null)
            {
                npc.TakeDamage(1); // Assuming 1 is the damage value for the bullet
            }
            print($"Hit Target {collision.gameObject.name}");
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }
    }

    void CreateBulletImpactEffect(Collision objectHit)
    {
        // ContactPoint contact = objectHit.contacts.First();
        
        // GameObject hole = Instantiate(
        //     GlobalReferences.Instance.bulletImpactEffectPrefab,
        //     contact.point,
        //     Quaternion.LookRotation(contact.normal)
        // );
        // hole.transform.SetParent(objectHit.gameObject.transform);
    }

    private IEnumerator RemoveBullet()
    {
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(gameObject);
    }
}
