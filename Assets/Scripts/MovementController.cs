using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -9.81f;
    
    private CharacterController characterController;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        InputAction moveInput = InputSystem.actions.FindAction("Move");  
        InputAction jumpAction = InputSystem.actions.FindAction("Jump"); 

        Vector2 moveVector = moveInput.ReadValue<Vector2>();

        Vector3 moveDirection = new Vector3(moveVector.x, 0f, moveVector.y).normalized * moveSpeed;

        if(jumpAction.triggered && characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(2f * Mathf.Abs(gravity) * jumpHeight); // jump height of 1.5 units
        }

        if(characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // small negative value to keep the player grounded
        }

        velocity.y += gravity * Time.deltaTime;

        moveDirection.y = velocity.y;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
