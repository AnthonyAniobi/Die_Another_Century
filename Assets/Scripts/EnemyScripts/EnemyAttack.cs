using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] private BoxCollider ninjaSwordCollider; // Reference to the ninja's attack collider
    [SerializeField] private BoxCollider bruteAxeCollider; // Reference to the brute's attack collider
    [SerializeField] private LayerMask[] enemyAttackVictim = new LayerMask[2]; // Layer mask to filter the player layer
    
    // private variables
    private BoxCollider activeAttackCollider; // Reference to the currently active attack collider
    private bool canAttack = true; // Flag to control attack availability
    private float attackInterval = 1f; // Interval between attacks
    private Animator animator; // Reference to the Animator component

    private string animationTriggerName = "Attack";

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void Initialize(EnemyTypeEnum enemyType)
    {
        switch (enemyType)
        {
            case EnemyTypeEnum.Brute:
                activeAttackCollider = bruteAxeCollider;
                animationTriggerName = "Attack1"; // Set the animation trigger name for Brute
                break;
            case EnemyTypeEnum.Ninja:
                activeAttackCollider = ninjaSwordCollider;
                animationTriggerName = "Attack2"; // Set the animation trigger name for Ninja
                break;
            default:
                Debug.LogWarning("Unknown enemy type: " + enemyType);
                break;
        }
        activeAttackCollider.enabled = false; // Disable the attack collider initially
    }

    
     // reference of the attack point

    void Update()
    {
        if (CustomInput.Instance.attackSingle && canAttack)
        {
            Attack();
        } 
    }

    private void Attack()
    {
        Invoke(nameof(ResetDamage), attackInterval); // Reset the canDamage flag after the interval
        canAttack = false; // Prevent further attacks until the interval has passed
        activeAttackCollider.enabled = true;
        animator.SetTrigger("Attack");
    }

    private void ResetDamage()
    {
        canAttack = true; // Allow damage again after the interval
        activeAttackCollider.enabled = false; // Disable the attack collider after the interval
    }
    
}
