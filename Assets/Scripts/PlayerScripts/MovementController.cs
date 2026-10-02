using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Animator animator;
    
    private CharacterController characterController;
    private Vector3 velocity = Vector3.zero;
    private Button jumpButton; // Reference to the Jump button

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    void Update()
    {
        if(GameLevelManager.instance != null && !GameLevelManager.instance.gameStarted)
        {
            animator?.SetBool("MOVING", false);
            return; // Do not process movement if the game hasn't started
        }

        Vector2 moveVector = Vector2.zero;
        bool jumpPressed = false;

        if(GameLevelManager.instance.moveJoystick != null){
            moveVector = new Vector2(
                GameLevelManager.instance.moveJoystick.Horizontal, 
                GameLevelManager.instance.moveJoystick.Vertical
            );
        }
        else
        {
            InputAction moveInput = InputSystem.actions.FindAction("Move");  
            moveVector = moveInput.ReadValue<Vector2>();
        }

        if (jumpButton != null)
        {
            jumpPressed = jumpButton.onClick != null; // Check if the jump button is pressed
        }
        else
        {
            InputAction jumpAction = InputSystem.actions.FindAction("Jump");
            jumpPressed = jumpAction.triggered;
        }


        animator?.SetBool("MOVING", moveVector.sqrMagnitude > 0f);

        Vector3 movement = transform.right * moveVector.x + transform.forward * moveVector.y;
        movement = movement.normalized * moveSpeed;

        // Vector3 moveDirection = new Vector3(moveVector.x, 0f, moveVector.y).normalized * moveSpeed;

        if(jumpPressed && characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(2f * Mathf.Abs(gravity) * jumpHeight); // jump height of 1.5 units
        }

        if(characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // small negative value to keep the player grounded
        }

        velocity.y += gravity * Time.deltaTime;

        movement.y = velocity.y;

        characterController.Move(movement * Time.deltaTime);
    }
}
