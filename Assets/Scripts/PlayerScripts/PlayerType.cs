using UnityEngine;
using PlayerData;

public class PlayerType : MonoBehaviour
{
    public PlayerTypeEnum playerType;
    public GameObject sampleObject;
    public GameObject spawnPoint;

    [Header("Player Type Prefabs")]
    public GameObject swatPrefab;
    public GameObject specialForcesPrefab;
    public GameObject warSoldierPrefab;
    public GameObject gasMaskSoldierPrefab;
    public GameObject futuristicTrooperPrefab;
    
    void Start()
    {
        // InitializePlayerType(playerType);
    }
    void InitializePlayerType(PlayerTypeEnum type)
    {
        playerType = type;
        GameObject playerPrefab = GetPlayerPrefabByType(playerType);
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

    GameObject GetPlayerPrefabByType(PlayerTypeEnum type)
    {
        switch (type)
        {
            case PlayerTypeEnum.Swat:
                return swatPrefab;
            case PlayerTypeEnum.SpecialForces:
                return specialForcesPrefab;
            case PlayerTypeEnum.WarSoldier:
                return warSoldierPrefab;
            case PlayerTypeEnum.GasMaskSoldier:
                return gasMaskSoldierPrefab;
            case PlayerTypeEnum.FuturisticSoldier:
                return futuristicTrooperPrefab;
            default:
                return null;
        }
    }
}
