using UnityEngine;
using PlayerData;

public class PlayerType : MonoBehaviour
{
    public PlayerTypeEnum playerType;
    public GameObject sampleObject;
    public GameObject spawnPoint;

    [Header("Player Type Objects")]
    public GameObject swat;
    public GameObject specialForces;
    public GameObject warSoldier;
    public GameObject gasMaskSoldier;
    public GameObject futuristicTrooper;
    
    void Start()
    {
        InitializePlayerType(playerType);
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

    void ActivatePlayerType(PlayerTypeEnum type)
    {
        switch (type)
        {
            case PlayerTypeEnum.Swat:
                swat.SetActive(true);
                specialForces.SetActive(false);
                
                break;
            case PlayerTypeEnum.SpecialForces:
                specialForces.SetActive(true);
                break;
            case PlayerTypeEnum.WarSoldier:
                warSoldier.SetActive(true);
                break;
            case PlayerTypeEnum.GasMaskSoldier:
                gasMaskSoldier.SetActive(true);
                break;
            case PlayerTypeEnum.FuturisticTrooper:
                futuristicTrooper.SetActive(true);
                break;
            default:
                Debug.LogError("Unknown player type: " + type);
                break;
        }
    }
}
