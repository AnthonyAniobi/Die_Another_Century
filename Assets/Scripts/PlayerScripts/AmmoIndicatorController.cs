using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoIndicatorController : MonoBehaviour
{
    [Header("Ammo Indicator")]
    [SerializeField] private GameObject countObject;
    [SerializeField] private Image indicatorImage;
    [SerializeField] private TextMeshProUGUI ammoCountText;

    [Header("Sprites for different weapon types")]
    [SerializeField] private Sprite noneSprite;
    [SerializeField] private Sprite knifeSprite;
    [SerializeField] private Sprite pistolSprite;
    [SerializeField] private Sprite rifleSprite;
    [SerializeField] private Sprite shotgunSprite;
    [SerializeField] private Sprite grenadeSprite;
    [SerializeField] private Sprite rpgSprite;

    private WeaponType weaponType;
    private int ammoCount;

    public void Initialize(WeaponType weaponType, int ammoCount)
    {
       if(weaponType == WeaponType.None || weaponType == WeaponType.knife)
       {
            countObject.SetActive(false);
       }
       else
       {
            countObject.SetActive(true);
            this.weaponType = weaponType;
            this.ammoCount = ammoCount;
            UpdateAmmoCount(ammoCount);
       }
       indicatorImage.sprite = GetSpriteForWeaponType(weaponType);
    }

    // Update is called once per frame
    public void UpdateAmmoCount(int newAmmoCount)
    {
        ammoCount = newAmmoCount;
        ammoCountText.text = ammoCount.ToString();
    }

    public void SetSelected(bool isSelected)
    {
        if (isSelected)
        {
            indicatorImage.color = Color.green; // Change to the desired selected color
        }
        else
        {
            indicatorImage.color = Color.white; // Change to the default color
        }
    }

    private Sprite GetSpriteForWeaponType(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.None:
                return noneSprite;
            case WeaponType.knife:
                return knifeSprite;
            case WeaponType.Pistol:
                return pistolSprite;
            case WeaponType.Rifle:
                return rifleSprite;
            case WeaponType.Shotgun:
                return shotgunSprite;
            case WeaponType.Grenade:
                return grenadeSprite;
            case WeaponType.RPG:
                return rpgSprite;
            default:
                Debug.LogWarning($"No sprite defined for weapon type: {weaponType}");
                return null;
        }
    }
}
