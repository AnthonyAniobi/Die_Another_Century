using Unity.Cinemachine;
using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    private const int ActiveCameraPriority = 10;
    private const int InactiveCameraPriority = 0;

    public CinemachineCamera thirdPersonCamera;
    public CinemachineCamera firstPersonCamera;
    private bool isFirstPerson = false;

    void Start()
    {
        SetCameraPriorities();
    }

    void Update()
    {
        if (CustomInput.Instance.cameraSwitch)
        {
            ToggleCamera();
        }
    }

    void ToggleCamera()
    {
        isFirstPerson = !isFirstPerson;
        SetCameraPriorities();
    }

    void SetCameraPriorities()
    {
        if (firstPersonCamera == null || thirdPersonCamera == null)
        {
            Debug.LogError("Both first-person and third-person cameras must be assigned.", this);
            return;
        }

        firstPersonCamera.gameObject.SetActive(true);
        thirdPersonCamera.gameObject.SetActive(true);
        firstPersonCamera.Priority = isFirstPerson ? ActiveCameraPriority : InactiveCameraPriority;
        thirdPersonCamera.Priority = isFirstPerson ? InactiveCameraPriority : ActiveCameraPriority;
    }
}
