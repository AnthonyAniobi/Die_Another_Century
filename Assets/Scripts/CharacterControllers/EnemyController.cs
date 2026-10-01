using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector2 patrolCenter;
    [SerializeField] public EnemyStartingState startingState = EnemyStartingState.Idle; // the initial state of the enemy
    [SerializeField] private float searchAreaRadius = 20f; // radius of where the enemy will walk arround to search for the player
    [SerializeField] private Transform playerEyeLevelTransform; // reference to the player's eye level transform
    [SerializeField] private float eyeDistance = 8f; // distance for the enemy eye (raycast)
    [SerializeField] private Transform weaponDamagePoint;
    [SerializeField] private float weaponDamageRadius = 0.5f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackDamageInterval = 0.5f;
    
    private Animator animator;
    
    private bool isInPatrolPoint = false; // whether the enemy is currently in a patrol point
    public Transform playerTransform;
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
        animator = GetComponentInChildren<Animator>();
        
    }
    


    // Update is called once per frame
    void Update()
    {

        CheckIfPlayerIsInView();
        Vector3 moveDirection = Vector3.zero;
        
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
                Debug.DrawRay(playerEyeLevelTransform.position, playerEyeLevelTransform.forward * hit.distance, Color.red);
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
