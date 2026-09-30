using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector2 patrolCenter;
    [SerializeField] public EnemyStartingState startingState = EnemyStartingState.Idle; // the initial state of the enemy
    [SerializeField] private float searchAreaRadius = 20f; // radius of where the enemy will walk arround to search for the player
    [SerializeField] private float walkSpeed = 2f; // speed at which the enemy walks
    [SerializeField] private float chaseSpeed = 4f; // speed at which the enemy chases the player
    [SerializeField] private Transform playerEyeLevelTransform; // reference to the player's eye level transform
    [SerializeField] private float eyeDistance = 8f; // distance for the enemy eye (raycast)
    [SerializeField] private float attackDistance = 40.0f; // distance at which the enemy will attack the player
    [SerializeField] private float turnSpeed = 5f; // speed at which the enemy turns in patrol
    
    private bool isInPatrolPoint = false; // whether the enemy is currently in a patrol point
    Transform playerTransform;
    private CharacterController characterController;
    private float gravity = -9.81f;


    public enum EnemyStartingState
    {
        Idle, Patrol
    }


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if(startingState == EnemyStartingState.Patrol)
        {
            isInPatrolPoint = true;
        }
        else
        {
            isInPatrolPoint = false;
        }
    }
    


    // Update is called once per frame
    void Update()
    {

        CheckIfPlayerIsInView();
        Vector3 moveDirection = Vector3.zero;
        
        if(playerTransform != null)
        {
            // if enemy has detected the player, chase the player
            // Move towards the player
            Vector3 distanceToPlayer = playerTransform.position - transform.position;
            float distanceMagnitude = distanceToPlayer.magnitude;

            if(distanceMagnitude < attackDistance)
            {
                // Attack the player
                // Implement your attack logic here
            }
            else
            {
                // Chase the player
                float step = chaseSpeed * Time.deltaTime;
                moveDirection = distanceToPlayer.normalized * step;
            }
            
        }else if(!isInPatrolPoint)
        {
            if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), patrolCenter) > 0.5f)
            {
                // if enemy has not gotten to the patrol point, move towards the patrol point
                Vector3 directionToPatrolPoint = new Vector3(patrolCenter.x, transform.position.y, patrolCenter.y) - transform.position;
                directionToPatrolPoint = directionToPatrolPoint.normalized;
                float step = walkSpeed * Time.deltaTime;
                moveDirection = directionToPatrolPoint.normalized * step;
                isInPatrolPoint = false;
            }else 
            {
                isInPatrolPoint = true;
            }
            
        }else
        {
            // if enemy has gotten to the patrol point, go round in a circle arround the patrol point
            float angle = Time.time * walkSpeed; // Adjust the speed of rotation by multiplying with walkSpeed
            float x = Mathf.Cos(angle) * searchAreaRadius;
            float z = Mathf.Sin(angle) * searchAreaRadius;
            Vector3 patrolPoint = new Vector3(patrolCenter.x + x, transform.position.y, patrolCenter.y + z);
            Vector3 directionToPatrolPoint = patrolPoint - transform.position;
            directionToPatrolPoint = directionToPatrolPoint.normalized;
            float step = walkSpeed * Time.deltaTime;
            moveDirection = directionToPatrolPoint.normalized * step;
            gameObject.transform.localRotation = Quaternion.Euler(0f, angle * Mathf.Rad2Deg, 0f);
        }
        moveDirection.y = gravity;

        if(characterController.isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = -2f; // small negative value to keep the NPC grounded
        }

        characterController.Move(moveDirection);
    }

    void OnTriggerEnter(Collider other) // This method is called when the enemy's collider enters a trigger collider
    {
        if (other.CompareTag("Player") && playerTransform == null)
        {
            playerTransform = other.transform;
        }
    }

    private void CheckIfPlayerIsInView()
    {
        // use a raycast to check if the player is in view of the enemy
        // if the player is sighted set the player transform and continue to chase the player until
        // you catch up to the player
        if(playerTransform == null)
        {
            Ray ray = new Ray(playerEyeLevelTransform.position, playerEyeLevelTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, eyeDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Player sighted by enemy!");
                    playerTransform = hit.transform;
                }
            }
        }
        
    }
}
