using UnityEngine;

public class NPCVictimController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private bool isBeingRescued = false;
    private bool isRescued = false;

    private Transform playerTransform;
    private CharacterController characterController;

    private float gravity = -9.81f;

    // manage how the npc moves towards the player when being rescued, and how it behaves when it reaches the safe area. The NPC will move towards the player when the player is nearby and will stop moving once it reaches the safe area.
    private float safeAreaRadius = 2f; // radius around the player where the NPC will stop moving
    private float walkRadius = 5f; // radius around the player where the npc walks towards the player

    private float runRadius = 10f; // radius around the player where the npc runs towards the player

    private float lostRadius = 15f; // radius around the player where the npc will stop moving towards the player because the player is too far
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform != null && isBeingRescued)
        {
            // Move towards the player
            float step = moveSpeed * Time.deltaTime; // Adjust speed as needed

            Vector3 distanceToPlayer = playerTransform.position - transform.position;
            float distanceMagnitude = distanceToPlayer.magnitude;
            
            Vector3 moveDirection = distanceToPlayer.normalized ;
            moveDirection.y = gravity;

            if(characterController.isGrounded && moveDirection.y < 0)
            {
                moveDirection.y = -2f; // small negative value to keep the NPC grounded
            }

            characterController.Move(moveDirection * step);
        }
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
