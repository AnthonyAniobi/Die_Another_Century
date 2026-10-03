using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector2 patrolCenter;
    [SerializeField] private float patrolLength = 20f; // radius of where the enemy will walk arround to search for the player
    [SerializeField] private Transform playerEyeLevelTransform; // reference to the player's eye level transform
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float attackRange = 5f;

    private Animator animator;
    
    public Transform playerTransform;
    private BotCharacterMove movementController;
    [SerializeField] internal EnemyState currentState = EnemyState.Idle;



    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        movementController = GetComponent<BotCharacterMove>();
        if(currentState == EnemyState.Patrol)
        {
            StartPatrolling();
        }
    }
    
    // void Update()
    // {
    // }


    public enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Hit,
        Dead
    }

    public void PursuePlayer(Transform playerTransform)
    {
        if(currentState == EnemyState.Chase){
            /// if player is sighted rotate to face the player
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 1);
        }
        // pursue the player current position
        // if the player is in attack range, stop and attack
        if(IsPlayerInAttackRange(playerTransform))
        {
            movementController.StopMovement();
            PerformAttack();
        }
        else
        {
            UpdateEnemyState(EnemyState.Chase);
            movementController.SetMoveDestination(
                new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z),
                runSpeed, 
                attackRange,
                attackRange, // use the same threshold for obstacle maneuvering
                () => {
                    // UpdateEnemyState(EnemyState.Idle);
                    // callback when the enemy reaches the players position
                    if (IsPlayerInAttackRange(playerTransform))
                    {
                        movementController.StopMovement();
                        PerformAttack();
                    }
                    else
                    {
                        UpdateEnemyState(EnemyState.Idle); // continue pursuing the player if not in range
                    }
                }
            );
        }
        
        
    }

    private bool IsPlayerInAttackRange(Transform playerTransform)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        return distanceToPlayer <= attackRange;
    }
    
    private void PerformAttack()
    {
        // if (canAttack)
        // {
            UpdateEnemyState(EnemyState.Attack);
            // Invoke("EndAttack", attackInterval); // Schedule the end of the attack after the interval
        // }
    }

    private void UpdateEnemyState(EnemyState newState)
    {

        switch (newState)
        {
            case EnemyState.Idle:
                animator.SetBool("WALKING", false);
                animator.SetBool("RUNNING", false);
                animator.SetBool("ATTACKING", false);
                animator.SetBool("DIE", false);
                currentState = EnemyState.Idle;
                break;
            case EnemyState.Patrol:
                animator.SetBool("WALKING", true);
                animator.SetBool("RUNNING", false);
                animator.SetBool("ATTACKING", false);
                animator.SetBool("DIE", false);
                currentState = EnemyState.Patrol;
                break;
            case EnemyState.Chase:
                animator.SetBool("WALKING", false);
                animator.SetBool("RUNNING", true);
                animator.SetBool("ATTACKING", false);
                animator.SetBool("DIE", false);
                currentState = EnemyState.Chase;
                break;
            case EnemyState.Attack:
                animator.SetBool("WALKING", false);
                animator.SetBool("RUNNING", false);
                animator.SetBool("ATTACKING", true);
                animator.SetBool("DIE", false);
                currentState = EnemyState.Attack;
                break;
            case EnemyState.Hit:
                animator.SetTrigger("Hit");
                currentState = EnemyState.Hit;
                break;
            case EnemyState.Dead:
                animator.SetBool("DIE", true);
                currentState = EnemyState.Dead;
                break;
        }
    }


    private void StartPatrolling()
    {
        UpdateEnemyState(EnemyState.Patrol);
        // turn 90 degrees to the right and move forward with the patrol length and walk speed
        Vector3 patrolDirection = Quaternion.Euler(0, 90, 0) * transform.forward;
        Vector3 patrolDestination = transform.position + patrolDirection * patrolLength;
        // rotate the enemy to face the patrol destination
        Vector3 directionToPatrol = (patrolDestination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPatrol);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 1);
        
        movementController.SetMoveDestination(patrolDestination, walkSpeed, attackRange,
                attackRange, () => {
            // Once the enemy reaches the patrol point, pick a new one
            // movementController.StopMovement();
            StartPatrolling();
        });
    }
}
