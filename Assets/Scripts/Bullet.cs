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
            print("Hit Target ${collision.gameObject.name}");
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
