using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BotCharacterMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float destinationThreshold = 0.1f; // Distance threshold to consider the bot has reached the destination
    public Vector3 moveDestination = Vector3.zero;
    private float gravity = -9.81f;
    [SerializeField] private float width = 0.5f; // Width of the box for obstacle detection
    [SerializeField] private CharacterController characterController;
    [SerializeField] private List<LayerMask> obstacleLayers;
    private Animator animator;
    void Start()
    {
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check for obstacles in the move direction
        
        Vector3 moveDirection = Vector3.zero;
        float distanceToDestination = Vector3.Distance(transform.position, moveDestination);
        if(distanceToDestination > destinationThreshold)
        {
            animator?.SetBool("Moving", true);

            // Move towards the player
            Vector3 direction = (moveDestination - transform.position).normalized;
            float step = moveSpeed * Time.deltaTime;
            moveDirection = direction * step;

            // check for obstacles in the move direction
            // if(Physics.BoxCast(transform.position, new Vector3(width, 0.5f, width), direction, out RaycastHit hitInfo, Quaternion.identity, step, LayerMask.GetMask("Obstacle")))
            // {
            //     // If an obstacle is detected in move direction, change direction to
            //     // nearer moveDirection to the right or left of the obstacle
            //     Debug.Log("Obstacle detected, changing direction.");

            //     Vector3 rightDirection = Quaternion.Euler(0, 90, 0) * direction;
            //     Vector3 leftDirection = Quaternion.Euler(0, -90, 0) * direction;

            //     // Choose the direction that is furthest from the obstacle
            //     Vector3 newDirection = (rightDirection - hitInfo.point).sqrMagnitude > (leftDirection - hitInfo.point).sqrMagnitude ? rightDirection : leftDirection;
            //     moveDirection = newDirection;
            // }
        }
        else
        {
            animator?.SetBool("Moving", false);
        }
        moveDirection.y = gravity;
        if(characterController.isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = -2f; // small negative value to keep the NPC grounded
        }
        characterController.Move(moveDirection);
    }
}
