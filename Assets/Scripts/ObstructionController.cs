using UnityEngine;

public class ObstructionController : MonoBehaviour
{
    [SerializeField] private int hitsToDestroy = 3;
    private int currentHits = 0;


    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile") || collision.gameObject.CompareTag("EnemyProjectile"))
        {
            currentHits++;
            if (currentHits >= hitsToDestroy)
            {
                DestroyObstruction();
            }
        }
    }


    private void DestroyObstruction()
    {
        Destroy(gameObject);
    }
}
