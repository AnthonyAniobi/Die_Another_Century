using UnityEngine;
using UnityEngine.UI;

public class WelcomeSceneManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject gameStartPanel;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private GameObject keyboardControlCheckbox;
    [SerializeField] private GameObject touchpadControlCheckbox;

    void Start()
    {
        if (settingsPanel != null){
            settingsPanel.SetActive(false);
        }
        if (gameStartPanel != null){
            gameStartPanel.SetActive(false);
        }
        if (volumeSlider != null)
        {
            volumeSlider.value = BackgroundMusic.Instance != null ? 
                BackgroundMusic.Instance.GetComponent<AudioSource>().volume : 
                1.0f;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
        
        SetControlScheme(true); // Default to keyboard control scheme
    }

    void OnDestroy()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
        }
    }

    public void StartGame()
    {
        if (gameStartPanel != null){
            gameStartPanel.SetActive(true);
            settingsPanel.SetActive(false);
        }
    }

    public void PlayerOptions()
    {
        NavigationManager.Instance.NavigateToScene(GameScene.PlayerOptionsScene);
    }

    public void Settings()
    {
        if (settingsPanel != null){
            settingsPanel.SetActive(true);
            gameStartPanel.SetActive(false);}
    }

    public void StartGameWorld(int world)
    {
        GameManager.Instance.SetSelectedGameWorld((GameWorld)world);
        NavigationManager.Instance.NavigateToScene(GameScene.GameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // settings panel methods
    public void SetControlScheme(bool isKeyboard)
    {
        if (isKeyboard)
        {
            keyboardControlCheckbox.SetActive(true);
            touchpadControlCheckbox.SetActive(false);
        }
        else
        {
            keyboardControlCheckbox.SetActive(false);
            touchpadControlCheckbox.SetActive(true);
        }
    }

    private void OnVolumeChanged(float value)
    {
        if (BackgroundMusic.Instance != null)
        {
            BackgroundMusic.Instance.SetVolume(value);
        }
    }
}
