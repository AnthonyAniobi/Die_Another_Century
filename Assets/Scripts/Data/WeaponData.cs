using UnityEngine;

public class WeaponData
{
    public WeaponType type;
    public int totalAmmo;


    public WeaponData(WeaponType type, int totalAmmo)
    {
        this.type = type;
        this.totalAmmo = totalAmmo;
    }
}
public enum WeaponType
{
    punch = 0,
    knife = 1,
    Pistol = 2,
    Rifle = 3,
    Shotgun = 4,
    Grenade = 5,
    SMG = 6
}

public static class WeaponDataExtensions
{
    public static string GetWeaponName(this WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.punch:
                return "Punch";
            case WeaponType.knife:
                return "Knife";
            case WeaponType.Pistol:
                return "Pistol";
            case WeaponType.Rifle:
                return "Rifle";
            case WeaponType.Shotgun:
                return "Shotgun";
            case WeaponType.Grenade:
                return "Grenade";
            case WeaponType.SMG:
                return "SMG";
            default:
                return "Unknown Weapon";
        }
    }
}