using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BotCharacterMove : MonoBehaviour
{
    private float moveSpeed;
    private float destinationThreshold; // Distance threshold to consider the bot has reached the destination
    private Vector3 moveDestination;
    private float objectWidth; // Width of bot for obstacle detection
    private CharacterController characterController;
    private System.Action onDestinationReached; // Callback when destination is reached
    [SerializeField] private List<LayerMask> obstacleLayers;
    private float gravity = -9.81f;
    
    void Start()
    {
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check for obstacles in the move direction
        Vector3 moveVector = Vector3.zero;
        if(moveDestination != null)
        {
            moveVector = MoveTowardsDestination(
                moveDestination, 
                moveSpeed, 
                destinationThreshold
            );
            
        }
        moveVector.y = gravity;
        if(characterController.isGrounded && moveVector.y < 0)
        {
            moveVector.y = -2f; // small negative value to keep the NPC grounded
        }
        characterController.Move(moveVector);

        
    }
    private Vector3 MoveTowardsDestination(Vector3 destination, float speed, float treshold)
    {
        Vector3 moveDirection = Vector3.zero;
        float distanceToDestination = Vector3.Distance(transform.position, moveDestination);
        if(distanceToDestination > treshold)
        {
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
        }else
        {
            StopMoving();
        }
        return moveDirection;
        
    }

    /// <summary>
    /// This method moves the bot towards a specified destination at the given
    /// speed, and stops when it is within the specified threshold distance from the destination.
    /// The width parameter is used for obstacle detection, allowing the bot to navigate around obstacles while
    /// moving towards its destination.
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="speed"></param>
    /// <param name="threshold"></param>
    /// <param name="width"></param>
    /// <param name="onDestinationReachedCallback"></param>
    /// <returns></returns>
    public void SetMoveDestination(Vector3 destination, float speed, float threshold, float width=2f, System.Action onDestinationReachedCallback=null)
    {
        moveDestination = destination;
        moveSpeed = speed;
        destinationThreshold = threshold;
        objectWidth = width;
        onDestinationReached = onDestinationReachedCallback;
    }


    private void StopMoving()
    {
        moveSpeed = 0f;
        onDestinationReached?.Invoke();
    }

}
