using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseAimController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float minRotation = -90f;
    [SerializeField] private float maxRotation = 90f;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private float xRotation = 0f;
    private float yRotation = 0f;
    private Quaternion initialCameraLocalRotation;



    

    void Start()
    {
        if (cinemachineCamera != null)
        {
            initialCameraLocalRotation = cinemachineCamera.transform.localRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameLevelManager.instance != null && !GameLevelManager.instance.gameStarted)
        {
            return; // Do not process movement if the game hasn't started
        }
        Vector2 lookVector = Vector2.zero;
        if(GameLevelManager.instance.lookJoystick != null){
            lookVector = new Vector2(
                GameLevelManager.instance.lookJoystick.Horizontal, 
                GameLevelManager.instance.lookJoystick.Vertical
            );
        }
        else
        {
            InputAction mouseInput = InputSystem.actions.FindAction("Look");
            lookVector = mouseInput.ReadValue<Vector2>();
        }
        xRotation += lookVector.y * mouseSensitivity * Time.deltaTime;
        yRotation += lookVector.x * mouseSensitivity * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, minRotation, maxRotation);

        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);

        if (cinemachineCamera != null)
        {
            cinemachineCamera.transform.localRotation = initialCameraLocalRotation * Quaternion.Euler(-xRotation, 0f, 0f);
        }
        
    }
}
