using UnityEngine;
using PlayerData;

public class PlayerType : MonoBehaviour
{
    public PlayerTypeEnum playerType;
    public GameObject sampleObject;
    public GameObject spawnPoint;
    

    void InitializePlayerType(PlayerTypeEnum type)
    {
        playerType = type;
        GameObject playerPrefab = playerType.GetGameObject();
        if (playerPrefab != null)
        {
            sampleObject.SetActive(false);
            Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation, transform);
        }
        else
        {
            Debug.LogError("Player prefab not found for type: " + playerType);
        }
    }

    
}
