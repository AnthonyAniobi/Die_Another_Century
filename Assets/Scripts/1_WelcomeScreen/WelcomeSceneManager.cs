using UnityEngine;

public class WelcomeSceneManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject gameStartPanel;

    void Start()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void StartGame()
    {
        if (gameStartPanel != null)
            gameStartPanel.SetActive(true);
    }

    public void PlayerOptions()
    {
        NavigationManager.Instance.NavigateToScene(GameScene.PlayerOptionsScene);
    }

    public void Settings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void StartGameWorld(GameWorld world)
    {
        GameManager.Instance.SetSelectedGameWorld(world);
        NavigationManager.Instance.NavigateToScene(GameScene.GameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
