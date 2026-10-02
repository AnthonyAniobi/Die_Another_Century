using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseAimController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float minRotation = -90f;
    [SerializeField] private float maxRotation = 90f;
    [SerializeField] private CinemachineCamera thirdPersonCamera;
    [SerializeField] private CinemachineCamera firstPersonCamera;

    private float xRotation = 0f;
    private float yRotation = 0f;
    private Quaternion initialFirstPersonCameraLocalRotation;
    private Quaternion initialThirdPersonCameraLocalRotation;

    void Start()
    {
        if (firstPersonCamera != null)
        {
            initialFirstPersonCameraLocalRotation = firstPersonCamera.transform.localRotation;
        }
        if (thirdPersonCamera != null)
        {
            initialThirdPersonCameraLocalRotation = thirdPersonCamera.transform.localRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameLevelManager.instance != null && !GameLevelManager.instance.gameStarted)
        {
            return; // Do not process movement if the game hasn't started
        }
        Vector2 lookVector = GameInputManager.instance.GetLookInput();
        xRotation += lookVector.y * mouseSensitivity * Time.deltaTime;
        yRotation += lookVector.x * mouseSensitivity * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, minRotation, maxRotation);

        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);

        if (firstPersonCamera != null)
        {
            firstPersonCamera.transform.localRotation = initialFirstPersonCameraLocalRotation * Quaternion.Euler(-xRotation, 0f, 0f);
        }

        if (thirdPersonCamera != null)
        {
            thirdPersonCamera.transform.localRotation = initialThirdPersonCameraLocalRotation * Quaternion.Euler(-xRotation, 0f, 0f);
        }

    }
}
