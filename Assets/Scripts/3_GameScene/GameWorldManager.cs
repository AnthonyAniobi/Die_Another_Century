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
        playerInstance = Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity);
        playerInstance.GetComponent<PlayerType>().InitializePlayerType(
            GameManager.Instance.selectedPlayerType
        ); // Initialize the player with the Swat type
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
