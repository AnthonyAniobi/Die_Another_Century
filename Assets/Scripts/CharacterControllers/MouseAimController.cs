using UnityEngine;
using UnityEngine.InputSystem;

public class MouseAimController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float minRotation = -90f;
    [SerializeField] private float maxRotation = 90f;

    private float xRotation = 0f;
    private float yRotation = 0f;



    

    // Update is called once per frame
    void Update()
    {
        if(GameLevelManager.instance != null && !GameLevelManager.instance.gameStarted)
        {
            return; // Do not process movement if the game hasn't started
        }
        InputAction mouseInput = InputSystem.actions.FindAction("Look");
        xRotation += mouseInput.ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;
        yRotation += mouseInput.ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, minRotation, maxRotation);

        gameObject.transform.localRotation = Quaternion.Euler(-xRotation, yRotation, 0f);
        
    }
}
