using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void NavigateToScene(GameScene scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene((int)scene);        
    }
}


public enum GameScene
{
    WelcomeScene = 0,
    PlayerOptionsScene = 1,
    GameScene = 2,
}