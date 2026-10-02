using UnityEngine;

public class NPCVictimController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Animator animator;
    private bool isBeingRescued = false;

    private Transform playerTransform;
    
    
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }
    
    void Update()
    {
        if(playerTransform != null && isBeingRescued)
        {
            // Move towards the player
            Vector3 distanceToPlayer = playerTransform.position - transform.position;
        }
        
        // animator?.SetBool("MOVING", false);
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isBeingRescued)
        {
            isBeingRescued = true;
            playerTransform = other.transform;
            GameLevelManager.instance.SetPlayerIsRescuingNPC(transform);
        }

        if(other.CompareTag("SafeArea") && isBeingRescued)
        {
            isBeingRescued = false;
            GameLevelManager.instance.SetNPCRescued();
            playerTransform = null;

            Invoke("DeactivateNPC", 2f); // Deactivate after 2 seconds
        }
    }

    private void DeactivateNPC()
    {
        Destroy(gameObject);
        
    }

}
