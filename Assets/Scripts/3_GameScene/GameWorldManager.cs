using UnityEngine;

public class GameWorldManager : MonoBehaviour
{
    [Header("Game World Settings")]
    public GameObject playerPrefab;
    public Vector3 playerSpawnPosition;

    //
    private GameObject playerInstance;

    void Start()
    {
        // InitializePlayer();
    }

    private void InitializePlayer()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("GameWorldManager: playerPrefab is not assigned.");
            return;
        }

        playerInstance = Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity);
        PlayerType playerType = playerInstance.GetComponentInChildren<PlayerType>(true);
        if (playerType == null)
        {
            Debug.LogError("GameWorldManager: the instantiated player prefab has no PlayerType component.");
            return;
        }

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameWorldManager: GameManager.Instance is not available.");
            return;
        }

        playerType.InitializePlayerType(GameManager.Instance.selectedPlayerType);
    }
}
