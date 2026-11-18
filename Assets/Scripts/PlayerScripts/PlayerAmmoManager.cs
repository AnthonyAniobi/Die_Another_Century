using System.Collections.Generic;
using UnityEngine;



public class PlayerAmmoManager : MonoBehaviour
{
    [Header("Ammo Manager")]
    [Space(10)]
    [Header("this script is responsible for managing the player's ammo and reloading.")]
    public Dictionary<WeaponType, int> availableWeapons;

    public void Initialize(Dictionary<WeaponType, int> initialAmmo)
    {
        availableWeapons = new Dictionary<WeaponType, int>(initialAmmo);
        availableWeapons[WeaponType.punch] = 0; // Ensure punch is always available
    }

    public void NextWeapon(WeaponType currentWeapon)
    {
        List<WeaponType> weaponTypes = new List<WeaponType>(availableWeapons.Keys);
        int currentIndex = weaponTypes.IndexOf(currentWeapon);
        int nextIndex = (currentIndex + 1) % weaponTypes.Count;
        WeaponType nextWeapon = weaponTypes[nextIndex];
        GetComponent<PlayerAttackController>().ChangeWeapon(nextWeapon);
    }
    public void PreviousWeapon(WeaponType currentWeapon)
    {
        List<WeaponType> weaponTypes = new List<WeaponType>(availableWeapons.Keys);
        int currentIndex = weaponTypes.IndexOf(currentWeapon);
        int previousIndex = (currentIndex - 1 + weaponTypes.Count) % weaponTypes.Count;
        WeaponType previousWeapon = weaponTypes[previousIndex];
        GetComponent<PlayerAttackController>().ChangeWeapon(previousWeapon);
    }

    public bool DecreaseAmmo(WeaponType weaponType, int amount)
    {
        if (availableWeapons.ContainsKey(weaponType))
        {
            int currentAmmo = availableWeapons[weaponType];
            if (currentAmmo >= amount)
            {
                availableWeapons[weaponType] -= amount;
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
        {
            availableWeapons[weaponType] = ammoAmount;
        }
    }
}
