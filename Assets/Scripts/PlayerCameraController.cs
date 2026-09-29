using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera firstPersonCamera;
    [SerializeField] private CinemachineCamera thirdPersonCamera;

    private bool isFirstPerson = true;

    void Start()
    {
        firstPersonCamera.gameObject.SetActive(isFirstPerson);
        thirdPersonCamera.gameObject.SetActive(!isFirstPerson);
    }

    // Update is called once per frame
    void Update()
    {
        InputAction switchCameraAction = InputSystem.actions.FindAction("SwitchCamera");

        if (switchCameraAction.triggered)
        {
            isFirstPerson = !isFirstPerson;
            firstPersonCamera.gameObject.SetActive(isFirstPerson);
            thirdPersonCamera.gameObject.SetActive(!isFirstPerson);
        }
    }
}
