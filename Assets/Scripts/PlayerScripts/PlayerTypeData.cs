using System.Collections.Generic;
using UnityEngine;

namespace PlayerData
{

    public enum PlayerTypeEnum
    {
        WarSoldier,
        Swat,
        GasMaskSoldier,
        SpecialForces,
        FuturisticSoldier,
    }

    public static class PlayerTypeDataExtensions
    {
        public static GameObject GetGameObject(this PlayerTypeEnum playerType)
        {
            switch (playerType)
            {
                case PlayerTypeEnum.WarSoldier:
                    return Resources.Load<GameObject>("Prefabs/Players/OldSoldier");
                case PlayerTypeEnum.Swat:
                    return Resources.Load<GameObject>("Prefabs/Players/Swat");
                case PlayerTypeEnum.GasMaskSoldier:
                    return Resources.Load<GameObject>("Prefabs/Players/SoldierGasMask");
                case PlayerTypeEnum.SpecialForces:
                    return Resources.Load<GameObject>("Prefabs/Players/SwatSoldier");
                case PlayerTypeEnum.FuturisticSoldier:
                    return Resources.Load<GameObject>("Prefabs/Players/Trooper");
                default:
                    return null;
            }
        }

        public static string GetDisplayName(this PlayerTypeEnum playerType)
        {
            switch (playerType)
            {
                case PlayerTypeEnum.WarSoldier:
                    return "War Soldier";
                case PlayerTypeEnum.Swat:
                    return "SWAT";
                case PlayerTypeEnum.GasMaskSoldier:
                    return "Gas Mask Soldier";
                case PlayerTypeEnum.SpecialForces:
                    return "Special Forces";
                case PlayerTypeEnum.FuturisticSoldier:
                    return "Futuristic Soldier";
                default:
                    return "Unknown";
            }
        }

        public static int GetHealth(this PlayerTypeEnum playerType)
        {
            switch (playerType)
            {
                case PlayerTypeEnum.WarSoldier:
                    return 100;
                case PlayerTypeEnum.Swat:
                    return 120;
                case PlayerTypeEnum.GasMaskSoldier:
                    return 90;
                case PlayerTypeEnum.SpecialForces:
                    return 110;
                case PlayerTypeEnum.FuturisticSoldier:
                    return 130;
                default:
                    return 100;
            }
        }

        public static float GetSpeed(this PlayerTypeEnum playerType)
        {
            switch (playerType)
            {
                case PlayerTypeEnum.WarSoldier:
                    return 5f;
                case PlayerTypeEnum.Swat:
                    return 4.5f;
                case PlayerTypeEnum.GasMaskSoldier:
                    return 5.5f;
                case PlayerTypeEnum.SpecialForces:
                    return 6f;
                case PlayerTypeEnum.FuturisticSoldier:
                    return 6.5f;
                default:
                    return 5f;
            }
        }

        public static float GetResistance(this PlayerTypeEnum playerType)
        {
            switch (playerType)
            {
                case PlayerTypeEnum.WarSoldier:
                    return 0.8f;
                case PlayerTypeEnum.Swat:
                    return 0.9f;
                case PlayerTypeEnum.GasMaskSoldier:
                    return 0.7f;
                case PlayerTypeEnum.SpecialForces:
                    return 0.85f;
                case PlayerTypeEnum.FuturisticSoldier:
                    return 0.95f;
                default:
                    return 0.8f;
            }
        }
    
        public static Sprite GetSpriteForPlayerType(this PlayerTypeEnum playerType)
        {
            switch (playerType)
            {
                case PlayerTypeEnum.WarSoldier:
                    return Resources.Load<Sprite>("Sprites/Avatars/WarSoldier");
                case PlayerTypeEnum.Swat:
                    return Resources.Load<Sprite>("Sprites/Avatars/Swat");
                case PlayerTypeEnum.GasMaskSoldier:
                    return Resources.Load<Sprite>("Sprites/Avatars/GasMask");
                case PlayerTypeEnum.SpecialForces:
                    return Resources.Load<Sprite>("Sprites/Avatars/SpecialForces");
                case PlayerTypeEnum.FuturisticSoldier:
                    return Resources.Load<Sprite>("Sprites/Avatars/FuturisticTrooper");
                default:
                    return null;
            }
        }
        
        // public static int GetPlayerWeaponCapacity(this PlayerTypeEnum playerType, WeaponType weaponType)
        // {
        //     switch (playerType)
        //     {
        //         case PlayerTypeEnum.WarSoldier:
        //             ;
        //         case PlayerTypeEnum.Swat:
        //             return new List<WeaponData>
        //             {
        //                 new WeaponData(WeaponType.Shotgun, 32),
        //                 new WeaponData(WeaponType.Pistol, 48)
        //             };
        //         case PlayerTypeEnum.GasMaskSoldier:
        //             return new List<WeaponData>
        //             {
        //                 new WeaponData(WeaponType.RPG, 150),
        //                 new WeaponData(WeaponType.knife, 1)
        //             };
        //         case PlayerTypeEnum.SpecialForces:
        //             return new List<WeaponData>
        //             {
        //                 new WeaponData(WeaponType.RPG, 1),
        //                 new WeaponData(WeaponType.Pistol, 48)
        //             };
        //         case PlayerTypeEnum.FuturisticSoldier:
        //             return new List<WeaponData>
        //             {
        //                 new WeaponData(WeaponType.Rifle, 120),
        //                 new WeaponData(WeaponType.RPG, 150)
        //             };
        //         default:
        //             return new List<WeaponData>();
        //     }
        // }
    }
}