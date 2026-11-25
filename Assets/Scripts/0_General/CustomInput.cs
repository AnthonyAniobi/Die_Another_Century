using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CustomInput : MonoBehaviour
{
    static public CustomInput Instance {get; private set;}

    [Header("Input Type")]
    public bool screenInputEnabled = false;
    
    internal Vector2 move;
    internal Vector2 look;
    internal bool attackSingle;
    internal bool attackContinuous;
    internal bool reload;
    internal bool cameraSwitch;
    internal bool sprint;
    internal bool jump;
    internal bool nextWeapon;
    internal bool prevWeapon;
    // dialog inputs
    internal bool dialogNext;
    internal bool dialogPrev;
    
    private InputActionMap inputActions;

    private InputMap inputMap = InputMap.Player;

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
            inputActions = InputSystem.actions.FindActionMap(InputMapToString(inputMap));
            inputActions.Enable();
        }
    }

    void Update()
    {
        if(!screenInputEnabled)
        {
            if(inputMap == InputMap.Player){
                move = inputActions.FindAction("Move").ReadValue<Vector2>();
                look = inputActions.FindAction("Look").ReadValue<Vector2>();
                attackSingle = inputActions.FindAction("Attack").WasPerformedThisFrame();
                attackContinuous = inputActions.FindAction("Attack").IsPressed();
                reload = inputActions.FindAction("Reload").WasPerformedThisFrame();
                cameraSwitch = inputActions.FindAction("SwitchCamera").WasPerformedThisFrame();
                sprint = inputActions.FindAction("Sprint").IsPressed();
                jump = inputActions.FindAction("Jump").WasPerformedThisFrame();
                nextWeapon = inputActions.FindAction("Next").WasPerformedThisFrame();
                prevWeapon = inputActions.FindAction("Previous").WasPerformedThisFrame();
            }else if(inputMap == InputMap.Dialog){
                dialogNext = inputActions.FindAction("Next").WasPerformedThisFrame();
                dialogPrev = inputActions.FindAction("Previous").WasPerformedThisFrame();
            }
        }
    }


    void SetPlayerInputMap()
    {
        inputActions.Disable();
        inputMap = InputMap.Player;
        inputActions = InputSystem.actions.FindActionMap(InputMapToString(inputMap));
        inputActions.Enable();
    }

    void SetDialogInputMap()
    {
        inputActions.Disable();
        inputMap = InputMap.Dialog;
        inputActions = InputSystem.actions.FindActionMap(InputMapToString(inputMap));
        inputActions.Enable();
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

    private enum InputMap
    {
        Player,
        Dialog
    }

    private string InputMapToString(InputMap map)
    {
        switch(map)
        {
            case InputMap.Player:
                return "Player";
            case InputMap.Dialog:
                return "Dialog";
            default:
                return "";
        }
    }

    
}

