using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAimController : MonoBehaviour
{
    private const int ActiveCameraPriority = 10;
    private const int InactiveCameraPriority = 0;

    [Header("Cameras")]
    public CinemachineCamera thirdPersonCamera;
    public CinemachineCamera firstPersonCamera;

    [Header("Aim UI")]
    public Image aimImage;

    public Sprite AimImageSprite;
    public Sprite selectImageSprite;

    private bool isFirstPerson = false;

    private bool isAiming = false;
    private bool isInSafeHouse = false;

    

    void Start()
    {
        SetCameraPriorities();
        // Ensure the aim image is initially inactive
        if (aimImage != null)
        {
            aimImage.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (CustomInput.Instance.cameraSwitch)
        {
            ToggleCamera();
        }
    }

    public void SetInSafeHouse(bool inSafeHouse)
    {
        isInSafeHouse = inSafeHouse;
        if (isInSafeHouse)
        {
            aimImage.sprite = selectImageSprite;
            aimImage.gameObject.SetActive(true);
        }else
        {
            aimImage.sprite = AimImageSprite;
            aimImage.gameObject.SetActive(isAiming);
        }
    }

    public void SetWeaponAim(bool isAiming)
    {
        this.isAiming = isAiming;
        aimImage.gameObject.SetActive(isAiming);
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
