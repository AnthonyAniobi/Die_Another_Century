using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool Patroll = true; // whether the enemy is patrolling or idle / chasing
    
    [Tooltip("Distance to walk before turning")]
    [SerializeField] private float patroltime = 2f; // time to walk before turning 
    [Tooltip("Distance to walk when hit by player")]
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

    public void Initialize(bool isPatrolling, float patrolTime = 2f)
    {
        this.Patroll = isPatrolling; // set wether the enemy is patrolling by default or not
        this.patroltime = patrolTime;
    }
    void Start()
    {
        chaseTimer = patroltime;
    }

    void Update()
    {
        if(IsPlayerInAttackRange())
        {
            StopMoving();
            PerformAttack();
        }
        else if(chaseTimer > 0)
        {
            chaseTimer -= Time.deltaTime;
        }
        else
        {
            // if the enemy has been chasing for too long, reset to patrol
            StopMoving();
            if (isPatrolling) // if the enemy is patrolling change direcion
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
        if (IsPlayerInAttackRange())
        {
            StopMoving();
            PerformAttack();
            return;
        }
        moveDirection = new Vector2(
            playerPosition.x - transform.position.x, 
            playerPosition.z - transform.position.z).normalized;
        sprinting = true;
        chaseTimer = patroltime; // reset the chase timer if the player is still in view
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
        GetComponent<EnemyAttack>().Attack();
    }
    private void StopMoving()
    {
        moveDirection = Vector2.zero;
        sprinting = false;
        chaseTimer = patroltime;
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
        moveDirection = patrolAngle switch
        {
            0 => Vector2.up,    // North
            1 => Vector2.right, // East
            2 => Vector2.down,  // South
            3 => Vector2.left,  // West
            _ => Vector2.zero
        };
        sprinting = false;
        chaseTimer = patroltime; // reset the chase timer if the player is still in view
        
    }
}
