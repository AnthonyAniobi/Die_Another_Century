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
    RPG = 6
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
            case WeaponType.RPG:
                return "RPG";
            default:
                return "Unknown Weapon";
        }
    }

    public static int GetWeaponDamage(this WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.punch:
                return 10;
            case WeaponType.knife:
                return 25;
            case WeaponType.Pistol:
                return 15;
            case WeaponType.Rifle:
                return 20;
            case WeaponType.Shotgun:
                return 30;
            case WeaponType.Grenade:
                return 50;
            case WeaponType.RPG:
                return 100;
            default:
                return 0;
        }
    }
}