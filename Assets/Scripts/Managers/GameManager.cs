using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayerData;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Button startGameButton;
    private PlayerType selectedPlayerType;


    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void OnEnable()
    {
        if (startGameButton != null)
            startGameButton.onClick.AddListener(StartGame);
    }

    void OnDestroy()
    {
        if (startGameButton != null)
            startGameButton.onClick.RemoveListener(StartGame);
    }


    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void SetSelectedPlayer(PlayerType playerType)
    {
        selectedPlayerType = playerType;
    }

    
}

enum GameWorld
{
    Village,
    Forest,
    Desert,
    Mountain,
}
