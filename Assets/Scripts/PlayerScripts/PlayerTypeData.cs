using UnityEngine;

namespace PlayerData
{
    public class PlayerTypeData
    {
        
    }



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
    }
}