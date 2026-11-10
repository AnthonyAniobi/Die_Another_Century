using UnityEngine;
using PlayerData;

public class PlayerType : MonoBehaviour
{
    [Header("Player Type")]
    public PlayerTypeEnum playerType;
    
    [Header("Player Type Objects")]
    public GameObject swat;
    public GameObject specialForces;
    public GameObject warSoldier;
    public GameObject gasMaskSoldier;
    public GameObject futuristicTrooper;

    [Header("Avatars")]
    public Avatar swatAvatar;
    public Avatar specialForcesAvatar;
    public Avatar warSoldierAvatar;
    public Avatar gasMaskSoldierAvatar;
    public Avatar futuristicTrooperAvatar;

    private Animator animator;
    internal Sprite sprite;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        InitializePlayerType(playerType);
    }
    void InitializePlayerType(PlayerTypeEnum type)
    {
        playerType = type;
        ActivatePlayerType(type);
        animator.avatar = GetAvatarForPlayerType(type);
        GetComponent<PlayerAttackController>().Initialize(type);
        sprite = GetSpriteForPlayerType(type);
    }

    void ActivatePlayerType(PlayerTypeEnum type)
    {
        swat.SetActive(false);
        specialForces.SetActive(false);
        warSoldier.SetActive(false);
        gasMaskSoldier.SetActive(false);
        futuristicTrooper.SetActive(false);

        switch (type)
        {
            case PlayerTypeEnum.Swat:
                swat.SetActive(true);
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
            case PlayerTypeEnum.FuturisticSoldier:
                futuristicTrooper.SetActive(true);
                break;
            default:
                Debug.LogError("Unknown player type: " + type);
                break;
        }
    }

    Avatar GetAvatarForPlayerType(PlayerTypeEnum type)
    {
        switch (type)
        {
            case PlayerTypeEnum.Swat:
                return swatAvatar;
            case PlayerTypeEnum.SpecialForces:
                return specialForcesAvatar;
            case PlayerTypeEnum.WarSoldier:
                return warSoldierAvatar;
            case PlayerTypeEnum.GasMaskSoldier:
                return gasMaskSoldierAvatar;
            case PlayerTypeEnum.FuturisticSoldier:
                return futuristicTrooperAvatar;
            default:
                Debug.LogError("Unknown player type: " + type);
                return null;
        }
    }
}
