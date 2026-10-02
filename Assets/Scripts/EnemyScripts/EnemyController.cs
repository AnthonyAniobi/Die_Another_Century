using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector2 patrolCenter;
    [SerializeField] private float searchAreaRadius = 20f; // radius of where the enemy will walk arround to search for the player
    [SerializeField] private Transform playerEyeLevelTransform; // reference to the player's eye level transform
    [SerializeField] private Transform weaponDamagePoint;
    [SerializeField] private float weaponDamageRadius = 0.5f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackDamageInterval = 0.5f;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float attackRange = 5f;

    private Animator animator;
    
    public Transform playerTransform;
    private bool isAttacking;
    private float nextDamageTime;
    private BotCharacterMove movementController;


    
    


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        movementController = GetComponent<BotCharacterMove>();
    }
    


    // Update is called once per frame
    void Update()
    {
        if(playerTransform != null)
        {
            movementController.SetMoveDestination(
                playerTransform.position, 
                runSpeed, 
                attackRange,
                attackRange, // use the same threshold for obstacle maneuvering
                () => {
                    UpdateEnemyState(EnemyState.Attack);
                    isAttacking = true;
                }
                );
        }
        
    }



    public void StopAttack()
    {
        isAttacking = false;
        UpdateEnemyState(EnemyState.Idle);
        
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
        // Implement logic to pursue the player
        // For example, you can set the enemy's destination to the player's position
        // and change the state to Chase or Attack based on distance.
        UpdateEnemyState(EnemyState.Chase);
        if(this.playerTransform == null)
        {
            this.playerTransform = playerTransform;
        }
        
    }

    

    private void UpdateEnemyState(EnemyState newState)
    {
        switch (newState)
        {
            case EnemyState.Idle:
                animator.SetBool("WALKING", false);
                animator.SetBool("RUNNING", false);
                animator.SetBool("ATTACKING", false);
                animator.SetBool("DEAD", false);
                break;
            case EnemyState.Patrol:
                animator.SetBool("WALKING", true);
                animator.SetBool("RUNNING", false);
                animator.SetBool("ATTACKING", false);
                animator.SetBool("DEAD", false);
                break;
            case EnemyState.Chase:
                animator.SetBool("WALKING", false);
                animator.SetBool("RUNNING", true);
                animator.SetBool("ATTACKING", false);
                animator.SetBool("DEAD", false);
                break;
            case EnemyState.Attack:
                animator.SetBool("WALKING", false);
                animator.SetBool("RUNNING", false);
                animator.SetBool("ATTACKING", true);
                animator.SetBool("DEAD", false);
                break;
            case EnemyState.Hit:
                animator.SetTrigger("Hit");
                break;
            case EnemyState.Dead:
                animator.SetBool("DEAD", true);
                break;
        }
    }

}
