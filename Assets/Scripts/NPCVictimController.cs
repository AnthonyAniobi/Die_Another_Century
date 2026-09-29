using UnityEngine;

public class NPCVictimController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private bool isBeingRescued = false;
    private bool isRescued = false;

    private Transform playerTransform;
    private CharacterController characterController;
    
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
            characterController.Move(transform.position - playerTransform.position);
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
