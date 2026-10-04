using UnityEngine;

public class NPCVictimController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float eyeDistance = 8f; // distance for the NPC eye (raycast)
    [SerializeField] private float distanceFromPlayer = 3f; // distance where the NPC will stop moving towards the player when being rescued
    [SerializeField] private NPCState currentState = NPCState.Idle;
    private Animator animator;
    private bool isBeingRescued = false;

    private Transform playerTransform;

    private BotCharacterMove movementController;



    internal enum NPCState
    {
        Idle,
        Running,
        Dead
    }
    
    
    void Start()
    {
        movementController = GetComponent<BotCharacterMove>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // if player transform is not null the player is being rescued,
        // so rotate the NPC to face the player and move towards the player
        if(playerTransform != null && isBeingRescued)
        {
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 1);
        }

        LookForPlayer();
        // if player is in range do nothing
        if(IsPlayerInRange())
        {
            movementController.StopMovement();
            UpdateNPCState(NPCState.Idle);
        }
        else if(playerTransform != null && isBeingRescued)
        {


            UpdateNPCState(NPCState.Running);
            // move towards the player
            movementController.SetMoveDestination(
                playerTransform.position, 
                moveSpeed, 
                distanceFromPlayer,
                distanceFromPlayer,
                ()=>{
                    // when the NPC reaches the player, stop moving and play the rescue animation
                    movementController.StopMovement();
                    UpdateNPCState(NPCState.Idle);
                }
            );
            animator.SetBool("MOVING", true);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, eyeDistance);
    }

    private void LookForPlayer()
    {
        
        if(Physics.CheckSphere(transform.position, eyeDistance, LayerMask.GetMask("Player")))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, eyeDistance, LayerMask.GetMask("Player"));
            if(hitColliders.Length > 0)
            {
                playerTransform = hitColliders[0].transform;
                isBeingRescued = true;
            }
        }else
        {
            if(isBeingRescued)
            {
                // GameLevelManager.ShowInfoPanel("You have stopped rescuing the NPC. Move closer to continue rescuing.");
            }
            playerTransform = null;
            isBeingRescued = false;
        }
    }

    private void DeactivateNPC()
    {
        Destroy(gameObject);
    }

    private void UpdateNPCState(NPCState newState)
    {
        switch (newState)
        {
            case NPCState.Idle:
                animator.SetBool("MOVING", false);
                currentState = NPCState.Idle;
                break;
            case NPCState.Running:
                animator.SetBool("MOVING", true);
                currentState = NPCState.Running;
                break;
            case NPCState.Dead:
                animator.SetBool("DIE", true);
                currentState = NPCState.Dead;
                break;
        }
    }


    private bool IsPlayerInRange()
    {
        if(playerTransform == null)
            return false;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        return distanceToPlayer <= distanceFromPlayer;
    }
}
