using UnityEngine;

public class WelcomeSceneManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject gameStartPanel;

    void Start()
    {
        if (settingsPanel != null){
            settingsPanel.SetActive(false);
        }
        if (gameStartPanel != null){
            gameStartPanel.SetActive(false);
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
}
