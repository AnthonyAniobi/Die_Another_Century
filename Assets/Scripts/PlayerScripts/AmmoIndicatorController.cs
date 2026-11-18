using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoIndicatorController : MonoBehaviour
{
    [SerializeField] private GameObject countObject;
    [SerializeField] private Image indicatorImage;
    [SerializeField] private TextMeshProUGUI ammoCountText;

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
            indicatorImage.color = Color.yellow; // Change to the desired selected color
        }
        else
        {
            indicatorImage.color = Color.white; // Change to the default color
        }
    }
}
