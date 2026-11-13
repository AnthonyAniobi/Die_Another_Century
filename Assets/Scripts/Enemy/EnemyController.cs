using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool Patroll = true; // whether the enemy is patrolling or idle / chasing
    
    [Tooltip("Distance to walk before turning")]
    [SerializeField] private float patroltimeX = 2f; // time to walk horizontally before turning 
    [SerializeField] private float patroltimeZ = 2f; // time to walk vertically before turning
    [Tooltip("Distance to walk when hit by player")]
    [SerializeField] private float chaseTime = 2f;
    [SerializeField] private float hitReactionTime = 1.5f; // time to move to direction of hit
    [SerializeField] private float chaseLostCooldown = 3f; // time to wait before returning to patrol after losing sight of player
    [SerializeField] private float attackRange = 1.5f; // range at which enemy can attack
    
    [Tooltip("Position of the player")]
    public Vector3 playerPosition; // reference to the player's transform if found
    

    // controls for movement
    // this will serve like a realtime input into the movement controller
    // imagine this as a virtual joystick that the enemy AI can control
    internal Vector2 moveDirection = Vector2.zero; // current move direction
    internal bool sprinting = false; // whether the enemy is sprinting

    // timers for movement
    private float chaseTimer = 0f;
    private int patrolAngle = 0; // 0 = North, 1= East, 2= South, 3= West movement direction in square patrol
    private bool isPatrolling = true; // whether the enemy is patrolling or idle / chasing
    private float chaseLostTimer = 0f; // timer for how long the enemy has been chasing the player

    public void Initialize(bool isPatrolling, float patrolTimeX = 2f, float patrolTimeZ = 2f)
    {
        this.Patroll = isPatrolling; // set wether the enemy is patrolling by default or not
        this.isPatrolling = isPatrolling;
        this.patroltimeX = patrolTimeX;
        this.patroltimeZ = patrolTimeZ;
    }
    void Start()
    {
        chaseTimer = chaseTime; // initialize the chase timer to the chase time
    }

    void Update()
    {
        if(IsPlayerInAttackRange())
        {
            PerformAttack();
        }
        else if(chaseTimer > 0)
        {
            chaseTimer -= Time.deltaTime;
        }
        else
        {
            // if the enemy has been chasing for too long, reset to patrol
            StopMoving(false);
            if (Patroll && isPatrolling) // if the enemy is patrolling change direcion
            {
                SwitchPatrolDirection();
            }else if (Patroll)
            {
                // if the enemy is a patrolling enemy and has lost the player
                if (chaseLostTimer > 0)
                {
                    chaseLostTimer -= Time.deltaTime;
                }
                else
                {
                    isPatrolling = true;
                    SwitchPatrolDirection();
                }
            }
        }
    }
    
    public void SetPlayerTransform(Vector3 player)
    {
        playerPosition = new Vector3(player.x, player.y, player.z);
        moveDirection = new Vector2(
            playerPosition.x - transform.position.x, 
            playerPosition.z - transform.position.z).normalized;
        sprinting = true;
        chaseTimer = chaseTime; // reset the chase timer if the player is still in view
        isPatrolling = false; // stop patrolling when chasing the player
        chaseLostTimer = chaseLostCooldown; // reset the chase lost timer for patrolling enemies
    }

    private bool IsPlayerInAttackRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
        return distanceToPlayer <= attackRange;
    }
    
    private void PerformAttack()
    {
        StopMoving();
        GetComponent<EnemyAttack>().Attack();
    }
    private void StopMoving(bool resetChaseTimer = true)
    {
        moveDirection = Vector2.zero;
        playerPosition = Vector3.zero;
        sprinting = false;

        if (resetChaseTimer)
        {
            chaseTimer = chaseTime;
        }
    }

    internal void EnemyHit(Vector3 hitDirection) // if enemy is hit move in hit direction
    {
        moveDirection = new Vector2(hitDirection.x, hitDirection.z).normalized;
        sprinting = true;
        chaseTimer = hitReactionTime; // reset the chase timer if the player is still in view
        isPatrolling = false; // stop patrolling when chasing the player
        chaseLostTimer = chaseLostCooldown; // reset the chase lost timer for patrolling enemies
    }
    
    private void SwitchPatrolDirection()
    {
        // turn 90 degrees to the right and move forward with the patrol length and walk speed
        patrolAngle = (patrolAngle + 1) % 4;
        // moveDirection = patrolAngle 
        switch(patrolAngle)
        {
            case 0:
                moveDirection = Vector2.up;    // North
                chaseTimer = patroltimeZ; // set the chase timer to the patrol time for vertical movement
                break;
            case 1:
                moveDirection = Vector2.right; // East
                chaseTimer = patroltimeX; // set the chase timer to the patrol time for horizontal movement
                break;
            case 2:
                moveDirection = Vector2.down;  // South
                chaseTimer = patroltimeZ; // set the chase timer to the patrol time for vertical movement
                break;
            case 3:
                moveDirection = Vector2.left;  // West
                chaseTimer = patroltimeX; // set the chase timer to the patrol time for horizontal movement
                break;
            default: 
                moveDirection = Vector2.zero;
                Debug.LogWarning("Invalid patrol angle: " + patrolAngle);
                break;
        }
        sprinting = false;
         // reset the chase timer if the player is still in view
        
    }
}
