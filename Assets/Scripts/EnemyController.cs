using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private bool canSearchForPlayer = true; // whether the enemy can search for the player or waits for player to come into its search area
    [SerializeField] private float searchAreaRadius = 10f; // radius of the search area
    [SerializeField] private float walkSpeed = 2f; // speed at which the enemy walks
    [SerializeField] private float chaseSpeed = 4f; // speed at which the enemy chases the player
    
    Transform playerTransform;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
