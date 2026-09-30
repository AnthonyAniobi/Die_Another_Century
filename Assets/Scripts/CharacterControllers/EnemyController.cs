using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private EnemyStartingState startingState = EnemyStartingState.Idle; // the initial state of the enemy
    [SerializeField] private float searchAreaRadius = 10f; // radius of where the enemy will walk arround to search for the player
    [SerializeField] private float walkSpeed = 2f; // speed at which the enemy walks
    [SerializeField] private float chaseSpeed = 4f; // speed at which the enemy chases the player
    
    Transform playerTransform;
    private CharacterController characterController;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private Transform playerEyeLevelTransform; // reference to the player's eye level transform
    [SerializeField] private float eyeDistance = 1.5f; // distance for the enemy eye (raycast)
    private float gravity = -9.81f;


    public enum EnemyStartingState
    {
        Idle, Patrol
    }


    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    


    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = Vector3.zero;
        if(playerTransform != null)
        {
            // Move towards the player
            Vector3 distanceToPlayer = playerTransform.position - transform.position;
            // float distanceMagnitude = distanceToPlayer.magnitude;           

            float step = moveSpeed * Time.deltaTime;
            
            // if(distanceMagnitude < 0.5f){
            moveDirection = distanceToPlayer.normalized * step;
            // }
            
        }
        moveDirection.y = gravity;

        if(characterController.isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = -2f; // small negative value to keep the NPC grounded
        }

        characterController.Move(moveDirection);
    }
}
