using UnityEngine;
using PlayerData;

public class WeaponData
{
    
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


    public static bool isGrenade(this WeaponType weaponType)
    {
        return weaponType == WeaponType.Grenade;
    }

    public static Vector3 GetWeaponOffset(this WeaponType weaponType, PlayerTypeEnum playerType)
    {

        switch (weaponType)
        {
            case WeaponType.knife:
                if(playerType == PlayerTypeEnum.FuturisticSoldier)
                {
                    return new Vector3(0.114f, -0.044f, -0.029f);
                }
                return new Vector3(0, 0, 0);
            case WeaponType.Pistol:
                if(playerType == PlayerTypeEnum.FuturisticSoldier)
                {
                    return new Vector3(0.131f, -0.035f, 0.08f);
                }
                return new Vector3(0, 0, 0);
            case WeaponType.Rifle:
                if(playerType == PlayerTypeEnum.FuturisticSoldier)
                {
                    return new Vector3(0.192f, -0.043f, 0.131f);
                }
                return new Vector3(0, 0, 0);
            case WeaponType.Shotgun:
                if(playerType == PlayerTypeEnum.FuturisticSoldier)
                {
                    return new Vector3(0.228f, -0.065f, 0.149f);
                }
                return new Vector3(0, 0, 0);
            case WeaponType.Grenade:
                if(playerType == PlayerTypeEnum.FuturisticSoldier)
                {
                    return new Vector3(-0.095f, 0.033f, -0.023f);
                }
                return new Vector3(0, 0, 0);
            case WeaponType.RPG:
                if(playerType == PlayerTypeEnum.FuturisticSoldier)
                {
                    return new Vector3(0.173f, -0.009f, 0.178f);
                }
                return new Vector3(0, 0, 0);
            default:
                return Vector3.zero;
        }
    }

    public static Vector3 GetWeaponRotation(this WeaponType weaponType, PlayerTypeEnum playerType)
    {

        switch (weaponType)
        {
            case WeaponType.knife:
                if(playerType == PlayerTypeEnum.FuturisticSoldier)
                {
                    return new Vector3(65.13f, 17.381f, 50.675f);
                }
                return new Vector3(0, 0, 0);
            case WeaponType.Pistol:
                if(playerType == PlayerTypeEnum.FuturisticSoldier)
                {
                    return new Vector3(172.31f, 107.3f, 78.6f);
                }
                return new Vector3(0, 0, 0);
            case WeaponType.Rifle:
                if(playerType == PlayerTypeEnum.FuturisticSoldier)
                {
                    return new Vector3(-7.249f, -101.236f, -93.808f);
                }
                return new Vector3(0, 0, 0);
            case WeaponType.Shotgun:
                if(playerType == PlayerTypeEnum.FuturisticSoldier)
                {
                    return new Vector3(-7.581f, -108.401f, 258.008f);
                }
                return new Vector3(0, 0, 0);
            case WeaponType.Grenade:
                if(playerType == PlayerTypeEnum.FuturisticSoldier)
                {
                    return new Vector3(-90f, 0f, 0f);
                }
                return new Vector3(0, 0, 0);
            case WeaponType.RPG:
                if(playerType == PlayerTypeEnum.FuturisticSoldier)
                {
                    return new Vector3(186.116f, 83.131f, 94.868f);
                }
                return new Vector3(0, 0, 0);
            default:
                return Vector3.zero;
        }
    }

}