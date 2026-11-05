using UnityEngine;
using UnityEngine.UI;


public class GameJoystickControls : MonoBehaviour
{
    [SerializeField] private Joystick movementJoystick;
    [SerializeField] private Joystick cameraJoystick;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button reloadButton;
    [SerializeField] private Button cameraInvertButton;
    [SerializeField] private Button sprintButton;
    [SerializeField] private Button jumpButton;
    
    
    void Update()
    {
        GameInputManager.Instance.movementInput = new Vector2(movementJoystick.Horizontal, movementJoystick.Vertical);
        GameInputManager.Instance.lookInput = new Vector2(cameraJoystick.Horizontal, cameraJoystick.Vertical);
    }

    void OnSprintPressed()
    {
        GameInputManager.Instance.sprint = true;
    }
    void OnSprintReleased()
    {
        GameInputManager.Instance.sprint = false;
    }
    void OnAttackPressed()
    {
        GameInputManager.Instance.attackContinuous = true;
        GameInputManager.Instance.attackSingle = true; // Trigger a single attack
    }
    void OnAttackReleased()
    {
        GameInputManager.Instance.attackContinuous = false;
        GameInputManager.Instance.attackSingle = false; // Reset single attack
    }

    void OnReloadPressed()
    {
        GameInputManager.Instance.reload = true;
    }
    void OnReloadReleased()
    {
        GameInputManager.Instance.reload = false;
    }

    void OnCameraInvertPressed()
    {
        GameInputManager.Instance.cameraInvert = true;
    }
    void OnCameraInvertReleased()
    {
        GameInputManager.Instance.cameraInvert = false;
    }

    void OnJumpPressed()
    {
        GameInputManager.Instance.jump = true;
    }
    void OnJumpReleased()
    {
        GameInputManager.Instance.jump = false;
    }
}