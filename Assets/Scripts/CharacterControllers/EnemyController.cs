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
    [SerializeField] private Animator animator;
    [SerializeField] private Transform weaponDamagePoint;
    [SerializeField] private float weaponDamageRadius = 0.5f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackDamageInterval = 0.5f;
    
    
    private bool isInPatrolPoint = false; // whether the enemy is currently in a patrol point
    Transform playerTransform;
    private CharacterController characterController;
    private float gravity = -9.81f;
    private bool isAttacking;
    private bool canStartAttack = true;
    private float nextDamageTime;


    public enum EnemyStartingState
    {
        Idle, Patrol
    }
    


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        
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
                if (canStartAttack)
                {
                    isAttacking = true;
                    canStartAttack = false;
                    nextDamageTime = 0f;
                }
            }
            else
            {
                isAttacking = false;
                canStartAttack = true;
                // Chase the player
                float step = chaseSpeed * Time.deltaTime;
                moveDirection = distanceToPlayer.normalized * step;
            }
            
        }else if(!isInPatrolPoint)
        {
                isAttacking = false;
                canStartAttack = true;
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
            isAttacking = false;
            canStartAttack = true;
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

        animator?.SetBool("Moving", new Vector3(moveDirection.x, 0f, moveDirection.z).sqrMagnitude > 0f);
        animator?.SetBool("Attacking", isAttacking);
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
        if (playerEyeLevelTransform == null)
        {
            playerEyeLevelTransform = transform;
        }

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

    


    public void StopAttack()
    {
        isAttacking = false;
        animator?.SetBool("Attacking", false);
    }
    

    public void HitPlayer()
    {
        if (!isAttacking || weaponDamagePoint == null || playerTransform == null)
        {
            return;
        }

        if (Time.time < nextDamageTime)
        {
            return;
        }

        HealthController playerHealth = playerTransform.GetComponentInParent<HealthController>();
        if (playerHealth == null)
        {
            return;
        }

        Collider[] hitColliders = Physics.OverlapSphere(weaponDamagePoint.position, weaponDamageRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.GetComponentInParent<HealthController>() == playerHealth)
            {
                playerHealth.TakeDamage(attackDamage);
                nextDamageTime = Time.time + attackDamageInterval;
                return;
            }
        }
    }
}
