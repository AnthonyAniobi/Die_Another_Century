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

    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public Vector2 GetMovementInput()
    {
        if(screenInputEnabled && movementJoystick != null)
        {
            return new Vector2(movementJoystick.Horizontal, movementJoystick.Vertical);
        }
        else
        {
            InputAction move = InputSystem.actions.FindAction("Move");
            return move.ReadValue<Vector2>();
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
            InputAction look = InputSystem.actions.FindAction("Look");
            return look.ReadValue<Vector2>();
        }
    }

    public bool IsAttackButtonPressed()
    {
        if(screenInputEnabled && attackButton != null)
        {
            return attackButton.onClick != null; // Check if the attack button is pressed
        }
        else
        {
            InputAction attack = InputSystem.actions.FindAction("Attack");
            return attack.triggered;
        }
    }

    public bool IsCameraInvertButtonPressed()
    {
        if(screenInputEnabled && cameraInvertButton != null)
        {
            return cameraInvertButton.onClick != null; // Check if the camera invert button is pressed
        }
        else
        {
            InputAction invertCamera = InputSystem.actions.FindAction("SwitchCamera");
            return invertCamera.triggered;
        }
    }
}
