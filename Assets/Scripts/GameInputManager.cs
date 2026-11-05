using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameInputManager : MonoBehaviour
{
    static public GameInputManager Instance {get; private set;}

    [Header("Input Type")]
    public bool screenInputEnabled = false;
    
    internal Vector2 movementInput;
    internal Vector2 lookInput;
    internal bool attackSingle;
    internal bool attackContinuous;
    internal bool reload;
    internal bool cameraInvert;
    internal bool sprint;
    internal bool jump;
    

    private InputActionMap inputActions;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            inputActions = InputSystem.actions.FindActionMap("Player");
            inputActions.Enable();
        }
    }

    void Update()
    {
        if(!screenInputEnabled)
        {
            movementInput = inputActions.FindAction("Move").ReadValue<Vector2>();
            lookInput = inputActions.FindAction("Look").ReadValue<Vector2>();
            attackSingle = inputActions.FindAction("Attack").WasPerformedThisFrame();
            attackContinuous = inputActions.FindAction("Attack").IsPressed();
            reload = inputActions.FindAction("Reload").WasPerformedThisFrame();
            cameraInvert = inputActions.FindAction("SwitchCamera").WasPerformedThisFrame();
            sprint = inputActions.FindAction("Sprint").IsPressed();
            jump = inputActions.FindAction("Jump").WasPerformedThisFrame();
        }
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
