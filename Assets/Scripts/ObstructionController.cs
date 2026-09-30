using UnityEngine;

public class ObstructionController : MonoBehaviour
{
    [SerializeField] private int hitsToDestroy = 3;
    [SerializeField] private int currentHits = 0;


    // Update is called once per frame
    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("bullet"))
    //     {
    //         currentHits++;
    //         if (currentHits >= hitsToDestroy)
    //         {
    //             DestroyObstruction();
    //         }
    //     }
    // }

    public void HitObstruction()
    {
        currentHits++;
        if (currentHits >= hitsToDestroy)
        {
            DestroyObstruction();
        }
    }


    private void DestroyObstruction()
    {
        Destroy(gameObject);
    }
}
