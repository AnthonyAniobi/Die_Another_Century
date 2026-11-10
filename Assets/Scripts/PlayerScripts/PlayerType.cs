using UnityEngine;
using PlayerData;
using TMPro;
using UnityEngine.UI;

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
    
    [Header("Player Type Data")]
    public TextMeshProUGUI playerNameText;
    public Image image;

    private Animator animator;
    internal Sprite sprite;
    private string playerName;
    
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
        sprite = type.GetSpriteForPlayerType();
        playerName = type.GetDisplayName();
        playerNameText.text = playerName;
        image.sprite = sprite;
        GetComponent<PlayerAttackController>().Initialize(type);
        GetComponent<PlayerHealth>().Initialize(type.GetHealth());
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
