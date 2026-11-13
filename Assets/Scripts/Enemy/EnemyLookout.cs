using UnityEngine;

public class EnemyLookout : MonoBehaviour
{
    [SerializeField] private float eyeDistance = 30f; // Distance the enemy can see
    [SerializeField] private Transform bruteEyeLevelTransform; // Reference to the player's eye level
    [SerializeField] private Transform ninjaEyeLevelTransform; // Reference to the player's eye level
    [SerializeField] private float viewRadius = 2f; // Radius of the lookout area
    
    private Transform playerEyeLevelTransform;
    public bool canSeePlayer = false; // Flag to indicate if the enemy can see the player
    public bool canAttackPlayer = false; // Flag to indicate if the enemy can attack the player

    public void Initialize(EnemyTypeEnum enemyType)
    {
        switch (enemyType)
        {
            case EnemyTypeEnum.Brute:
                playerEyeLevelTransform = bruteEyeLevelTransform;
                break;
            case EnemyTypeEnum.Ninja:
                playerEyeLevelTransform = ninjaEyeLevelTransform;
                break;
            default:
                Debug.LogWarning("Unknown enemy type: " + enemyType);
                break;
        }
    }

    void Update()
    {
        if(Physics.Raycast(
            playerEyeLevelTransform.position, 
            playerEyeLevelTransform.forward,
            out RaycastHit hit,
            eyeDistance
        ))
        {
            if(hit.collider.CompareTag("Player"))
            {
                canAttackPlayer = true;
                gameObject.GetComponent<EnemyController>().SetPlayerTransform(hit.transform.position);
            }
            else
            {
                canSeePlayer = false;
                canAttackPlayer = false;
            }
            DebugExtension.DrawRay(playerEyeLevelTransform.position, playerEyeLevelTransform.forward * hit.distance, Color.green);
        }
        else
        {
            canSeePlayer = false;
            canAttackPlayer = false;
            DebugExtension.DrawRay(playerEyeLevelTransform.position, playerEyeLevelTransform.forward * eyeDistance, Color.red);
        }
    }

}
