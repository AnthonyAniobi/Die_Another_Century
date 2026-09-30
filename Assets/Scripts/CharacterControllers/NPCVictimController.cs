using UnityEngine;

public class NPCVictimController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private bool isBeingRescued = false;
    private bool isRescued = false;

    private Transform playerTransform;
    private CharacterController characterController;

    private float gravity = -9.81f;

    // private float lostRadius = 15f; // radius around the player where the npc will stop moving towards the player because the player is too far
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = Vector3.zero;
        if(playerTransform != null && isBeingRescued)
        {
            // Move towards the player
            Vector3 distanceToPlayer = playerTransform.position - transform.position;
            // float distanceMagnitude = distanceToPlayer.magnitude;           

            float step = moveSpeed * Time.deltaTime;
            
            // if(distanceMagnitude < 0.5f){
            moveDirection = distanceToPlayer.normalized * step;
            // }

            
            
        }
        moveDirection.y = gravity;

        if(characterController.isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = -2f; // small negative value to keep the NPC grounded
        }

        characterController.Move(moveDirection);
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isBeingRescued)
        {
            isBeingRescued = true;
            playerTransform = other.transform;
        }

        if(other.CompareTag("SafeArea") && isBeingRescued)
        {
            isBeingRescued = false;
            isRescued = true;
            GameLevelManager.instance.SetNPCRescued();
            playerTransform = null;
            // gameObject.SetActive(false);

            Invoke("DeactivateNPC", 2f); // Deactivate after 2 seconds
        }
    }

    private void DeactivateNPC()
    {
        Destroy(gameObject);
        
    }

}
