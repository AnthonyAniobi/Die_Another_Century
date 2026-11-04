using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameInputManager : MonoBehaviour
{
    static public GameInputManager instance {get; private set;}

    // enable screen input for mobile devices
    [SerializeField] private bool screenInputEnabled = false;
    [SerializeField] private Joystick movementJoystick;
    [SerializeField] private Joystick cameraJoystick;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button cameraInvertButton;
    [SerializeField] private Button sprintButton;

    private bool attackButtonPressed;
    private bool cameraInvertButtonPressed;
    private bool sprintButtonPressed;

    private InputActionMap inputActions;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            inputActions = InputSystem.actions.FindActionMap("Player");
            inputActions.Enable();
        }
    }

    void OnEnable()
    {
        if (screenInputEnabled)
        {
            if(movementJoystick == null || cameraJoystick == null || attackButton == null || cameraInvertButton == null)
            {
                Debug.LogError("Screen input is enabled, but one or more UI elements are not assigned in the inspector.");
                screenInputEnabled = false; // Disable screen input to prevent null reference errors
            }
        }
        if(!screenInputEnabled)
        {
            InputSystem.EnableDevice(Keyboard.current);
            InputSystem.EnableDevice(Mouse.current);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            attackButton.onClick.AddListener(OnAttackButtonClicked);
            cameraInvertButton.onClick.AddListener(OnCameraInvertButtonClicked);
            sprintButton.onClick.AddListener(OnSprintButtonClicked);
        }
    }

    void OnDisable()
    {
        if (attackButton != null)
        {
            attackButton.onClick.RemoveListener(OnAttackButtonClicked);
        }

        if (cameraInvertButton != null)
        {
            cameraInvertButton.onClick.RemoveListener(OnCameraInvertButtonClicked);
        }

        if (sprintButton != null)
        {
            sprintButton.onClick.RemoveListener(OnSprintButtonClicked);
        }

        attackButtonPressed = false;
        cameraInvertButtonPressed = false;
        sprintButtonPressed = false;
    }

    private void OnAttackButtonClicked()
    {
        attackButtonPressed = true;
    }

    private void OnCameraInvertButtonClicked()
    {
        cameraInvertButtonPressed = true;
    }

    private void OnSprintButtonClicked()
    {
        sprintButtonPressed = true;
    }

    public Vector2 GetMovementInput()
    {
        if(screenInputEnabled && movementJoystick != null)
        {
            return new Vector2(movementJoystick.Horizontal, movementJoystick.Vertical);
        }
        else
        {
            return inputActions.FindAction("Move").ReadValue<Vector2>();
        }
    }

    

    public Vector2 GetLookInput()
    {
        if(screenInputEnabled && cameraJoystick != null)
        {
            return new Vector2(cameraJoystick.Horizontal, cameraJoystick.Vertical);
        }
        else
        {
            return inputActions.FindAction("Look").ReadValue<Vector2>();
        }
    }

    public bool IsSprinting()
    {
        if(screenInputEnabled)
        {
            return false; // Sprinting is not supported on screen input
        }
        else
        {
            InputAction sprint = InputSystem.actions.FindAction("Sprint");
            return sprint.ReadValue<float>() > 0.5f;
        }
    }

    public bool IsAttackButtonPressed()
    {
        if(screenInputEnabled && attackButton != null)
        {
            bool pressed = attackButtonPressed;
            attackButtonPressed = false;
            return pressed;
        }
        else
        {
            return inputActions.FindAction("Attack").triggered;
        }
    }

    public bool IsSprintButtonPressed()
    {
        if(screenInputEnabled && sprintButton != null)
        {
            bool pressed = sprintButtonPressed;
            sprintButtonPressed = false;
            return pressed;
        }
        else
        {
            return inputActions.FindAction("Sprint").IsPressed();
        }
    }

    public bool IsCameraInvertButtonPressed()
    {
        if(screenInputEnabled && cameraInvertButton != null)
        {
            bool pressed = cameraInvertButtonPressed;
            cameraInvertButtonPressed = false;
            return pressed;
        }
        else
        {
            return inputActions.FindAction("SwitchCamera").triggered;
        }
    }

    public bool IsJumpButtonPressed()
    {
        if(screenInputEnabled)
        {
            return false; // Jumping is not supported on screen input
        }
        else
        {
            return inputActions.FindAction("Jump").triggered;
        }
    }
}
