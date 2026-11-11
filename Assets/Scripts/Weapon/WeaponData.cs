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
                switch (playerType)
                {
                    case PlayerTypeEnum.FuturisticSoldier:
                        return new Vector3(0.114f, -0.044f, -0.029f);
                    case PlayerTypeEnum.GasMaskSoldier:
                        return new Vector3(0.01f, 0.077f, 0.011f);
                    case PlayerTypeEnum.Swat:
                        return new Vector3(-0.018f, 0.09f, 0.05f);
                    default:
                        return new Vector3(0.004f, 0.095f, 0.041f);
                }
            case WeaponType.Pistol:
                switch (playerType)
                {
                    case PlayerTypeEnum.FuturisticSoldier:
                        return new Vector3(0.131f, -0.035f, 0.08f);
                    default:
                        return new Vector3(-0.059931f, 0.1173864f, 0.0251783f);
                }
                
            case WeaponType.Rifle:
                switch (playerType)
                {
                    case PlayerTypeEnum.FuturisticSoldier:
                        return new Vector3(0.192f, -0.043f, 0.131f);
                    case PlayerTypeEnum.Swat:
                        return new Vector3(-0.094f, 0.188f, 0.005f);
                    case PlayerTypeEnum.GasMaskSoldier:
                        return new Vector3(-0.111f, 0.183f, 0.014f);
                    case PlayerTypeEnum.SpecialForces:
                        return new Vector3(-0.097f, 0.159f, 0.034f);
                    default:
                        return new Vector3(-0.081f, 0.192f, 0.011f);
                }
                
            case WeaponType.Shotgun:
                switch (playerType)
                {
                    case PlayerTypeEnum.FuturisticSoldier:
                        return new Vector3(0.228f, -0.065f, 0.149f);
                    case PlayerTypeEnum.SpecialForces:
                        return new Vector3(-0.106f, 0.216f, 0.063f);
                    case PlayerTypeEnum.GasMaskSoldier:
                        return new Vector3(-0.146f, 0.236f, -0.012f);
                    default:
                        return new Vector3(-0.106f, 0.256f, 0.031f);
                }
                
            case WeaponType.Grenade:
                if(playerType == PlayerTypeEnum.FuturisticSoldier)
                {
                    return new Vector3(-0.095f, 0.033f, -0.023f);
                }
                return new Vector3(-0.0017f, 0.0715f, 0.0171f);
            case WeaponType.RPG:
                switch (playerType)
                {
                    case PlayerTypeEnum.FuturisticSoldier:
                        return new Vector3(0.17f, -0.0022f, 0.129f);
                    case PlayerTypeEnum.GasMaskSoldier:
                        return new Vector3(-0.1051114f, 0.1776643f, -0.011333f);
                    case PlayerTypeEnum.Swat:
                        return new Vector3(-0.09f, 0.161f, -0.013f);
                    default:
                        return new Vector3(-0.09f, 0.184f, 0f);
                }
                
            default:
                return Vector3.zero;
        }
    }

    public static Vector3 GetWeaponRotation(this WeaponType weaponType, PlayerTypeEnum playerType)
    {

        switch (weaponType)
        {
            case WeaponType.knife:
                switch(playerType)
                {
                    case PlayerTypeEnum.FuturisticSoldier:
                        return new Vector3(65.13f, 17.381f, 50.675f);
                    case PlayerTypeEnum.GasMaskSoldier:
                        return new Vector3(-9.292f, -3.961f, 104.111f);
                    case PlayerTypeEnum.Swat:
                        return new Vector3(-10.184f, -4.988f, 90.055f);
                    default:
                        return new Vector3(-11.249f, -22.444f, 114.068f);
                }
            case WeaponType.Pistol:
                switch(playerType)
                {
                    case PlayerTypeEnum.FuturisticSoldier:
                        return new Vector3(172.31f, 107.3f, 78.6f);
                    default:
                        return new Vector3(71.853f, -30.852f, 55.9f);
                }
                
            case WeaponType.Rifle:
                switch(playerType)
                {
                    case PlayerTypeEnum.FuturisticSoldier:
                        return new Vector3(-7.249f, -101.236f, -93.808f);
                    case PlayerTypeEnum.Swat:
                        return new Vector3(98.79999f, -31.69501f, 86.34f);
                    case PlayerTypeEnum.GasMaskSoldier:
                        return new Vector3(99.91901f, -33.84698f, 85.189f);
                    case PlayerTypeEnum.SpecialForces:
                        return new Vector3(114.259f, -29.84302f, 86.079f);
                    default:
                        return new Vector3(79.114f, -154.455f, -41.57f);
                }
                
            case WeaponType.Shotgun:
                switch(playerType)
                {
                    case PlayerTypeEnum.FuturisticSoldier:
                        return new Vector3(-7.581f, -108.401f, 258.008f);
                    case PlayerTypeEnum.SpecialForces:
                        return new Vector3(114.249f, -28.94f, 84.235f);
                    case PlayerTypeEnum.GasMaskSoldier:
                        return new Vector3(109.329f, -104.022f, 0.2279968f);
                    default:
                        return new Vector3(97.2219f, -32.32898f, 73f);
                }
                
            case WeaponType.Grenade:
                if(playerType == PlayerTypeEnum.FuturisticSoldier)
                {
                    return new Vector3(-90f, 0f, 0f);
                }
                return new Vector3(26.731f, -14.757f, -68.797f);
            case WeaponType.RPG:
                switch (playerType)
                {
                    case PlayerTypeEnum.FuturisticSoldier:
                        return new Vector3(-7.154f, -93.89f, -86.903f);
                    case PlayerTypeEnum.GasMaskSoldier:
                        return new Vector3(82.635f, -97.412f, 4.182f);
                    case PlayerTypeEnum.Swat:
                        return new Vector3(82.291f, -106.266f, -4.651f);
                    default:
                        return new Vector3(80.49f, -141.075f, -38.06f);
                }
            default:
                return Vector3.zero;
        }
    }

    public static float GetWeaponDelay(this WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.punch:
                return 0.5f;
            case WeaponType.knife:
                return 0.7f;
            case WeaponType.Pistol:
                return 0.3f;
            case WeaponType.Rifle:
                return 0.2f;
            case WeaponType.Shotgun:
                return 1f;
            case WeaponType.Grenade:
                return 1.5f;
            case WeaponType.RPG:
                return 2f;
            default:
                return 0f;
        }
    }

    public static float GetWeaponFireForce(this WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Pistol:
                return 15f;
            case WeaponType.Rifle:
                return 20f;
            case WeaponType.Shotgun:
                return 25f;
            case WeaponType.Grenade:
                return 30f;
            case WeaponType.RPG:
                return 50f;
            default:
                return 0f;
        }
    }
}