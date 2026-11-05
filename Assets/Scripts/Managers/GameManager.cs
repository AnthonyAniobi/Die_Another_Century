using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayerData;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    internal PlayerType selectedPlayerType;
    internal GameWorld selectedGameWorld;
    internal int currentLevelIndex = 0;


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

    public void SetSelectedPlayer(PlayerType playerType)
    {
        selectedPlayerType = playerType;
    }

    public void SetSelectedGameWorld(GameWorld world)
    {
        selectedGameWorld = world;
        currentLevelIndex = 0; // Reset level index when changing game world
    }

    
}

public enum GameWorld
{
    Village,
    Forest,
    Desert,
    Mountain,
}
