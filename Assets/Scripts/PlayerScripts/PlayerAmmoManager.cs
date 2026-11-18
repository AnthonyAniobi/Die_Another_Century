using System.Collections.Generic;
using UnityEngine;



public class PlayerAmmoManager : MonoBehaviour
{
    [Header("Ammo Manager")]
    [Space(10)]
    [Header("this script is responsible for managing the player's ammo and reloading.")]
    public Dictionary<WeaponType, int> availableWeapons;
    
    public GameObject AmmoIndicatorPrefab;
    public Transform AmmoIndicatorRowParent;

    //
    private Dictionary<WeaponType, AmmoIndicatorController> ammoIndicators = new Dictionary<WeaponType, AmmoIndicatorController>();

    public void Initialize(Dictionary<WeaponType, int> initialAmmo)
    {
        availableWeapons = new Dictionary<WeaponType, int>(initialAmmo);
        availableWeapons[WeaponType.None] = 0; // Ensure no weapon is always available
        // Create ammo indicators for each weapon type
        // make sure empty weapon is first in the list so that it is always available
        List<WeaponType> weaponTypes = new List<WeaponType>(availableWeapons.Keys);
        weaponTypes.Sort((a, b) => a == WeaponType.None ? -1 : (b == WeaponType.None ? 1 : 0));
        foreach (var weapon in weaponTypes)
        {
            GameObject indicatorObject = Instantiate(AmmoIndicatorPrefab);
            indicatorObject.transform.SetParent(AmmoIndicatorRowParent, false);
            AmmoIndicatorController indicator = indicatorObject.GetComponent<AmmoIndicatorController>();
            indicator.Initialize(weapon, availableWeapons[weapon]);
            ammoIndicators[weapon] = indicator;
        }
        ammoIndicators[WeaponType.None].SetSelected(true);
    }

    public void NextWeapon(WeaponType currentWeapon)
    {
        List<WeaponType> weaponTypes = new List<WeaponType>(availableWeapons.Keys);
        int currentIndex = weaponTypes.IndexOf(currentWeapon);
        int nextIndex = (currentIndex + 1) % weaponTypes.Count;
        WeaponType nextWeapon = weaponTypes[nextIndex];
        GetComponent<PlayerAttackController>().ChangeWeapon(nextWeapon);
        ammoIndicators[currentWeapon].SetSelected(false);
        ammoIndicators[nextWeapon].SetSelected(true);
    }
    public void PreviousWeapon(WeaponType currentWeapon)
    {
        List<WeaponType> weaponTypes = new List<WeaponType>(availableWeapons.Keys);
        int currentIndex = weaponTypes.IndexOf(currentWeapon);
        int previousIndex = (currentIndex - 1 + weaponTypes.Count) % weaponTypes.Count;
        WeaponType previousWeapon = weaponTypes[previousIndex];
        GetComponent<PlayerAttackController>().ChangeWeapon(previousWeapon);
        ammoIndicators[currentWeapon].SetSelected(false);
        ammoIndicators[previousWeapon].SetSelected(true);
    }

    public bool DecreaseAmmo(WeaponType weaponType, int amount)
    {
        if (availableWeapons.ContainsKey(weaponType))
        {
            if(weaponType == WeaponType.None || weaponType == WeaponType.knife)
            {
                return true; // None and knife don't consume ammo
            }
            int currentAmmo = availableWeapons[weaponType];
            if (currentAmmo >= amount)
            {
                availableWeapons[weaponType] -= amount;
                ammoIndicators[weaponType].UpdateAmmoCount(availableWeapons[weaponType]);
                return true;
            }
            else
            {
                Debug.LogWarning($"Not enough ammo for {weaponType}. Current ammo: {currentAmmo}, required: {amount}");
                return false;
            }
        }
        return false;
    }

    // Update is called once per frame
    public void PickupAmmo(WeaponType weaponType, int ammoAmount)
    {
        if (availableWeapons.ContainsKey(weaponType))
        {
            availableWeapons[weaponType] += ammoAmount;
        }
        else
        { // if weapon not in dictionary add it and crate an indicator for it
            availableWeapons[weaponType] = ammoAmount;
            ammoIndicators[weaponType] = Instantiate(AmmoIndicatorPrefab, AmmoIndicatorRowParent).GetComponent<AmmoIndicatorController>();
            ammoIndicators[weaponType].Initialize(weaponType, availableWeapons[weaponType]);
        }
    }

    public bool HasAmmo(WeaponType weaponType)
    {
        if (availableWeapons.ContainsKey(weaponType))
        {
            return availableWeapons[weaponType] > 0;
        }
        return false;
    }
}
