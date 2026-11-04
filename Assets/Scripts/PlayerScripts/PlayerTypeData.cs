using UnityEngine;

namespace PlayerData
{
    public class PlayerTypeData
    {
        
    }



    public enum PlayerType
    {
        WarSoldier,
        Swat,
        GasMaskSoldier,
        SpecialForces,
        FuturisticSoldier,
    }

    public static class PlayerTypeDataExtensions
    {
        public static GameObject GetGameObject(this PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.WarSoldier:
                    return Resources.Load<GameObject>("Prefabs/Players/OldSoldier");
                case PlayerType.Swat:
                    return Resources.Load<GameObject>("Prefabs/Players/Swat");
                case PlayerType.GasMaskSoldier:
                    return Resources.Load<GameObject>("Prefabs/Players/SoldierGasMask");
                case PlayerType.SpecialForces:
                    return Resources.Load<GameObject>("Prefabs/Players/SwatSoldier");
                case PlayerType.FuturisticSoldier:
                    return Resources.Load<GameObject>("Prefabs/Players/Trooper");
                default:
                    return null;
            }
        }

        public static string GetDisplayName(this PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.WarSoldier:
                    return "War Soldier";
                case PlayerType.Swat:
                    return "SWAT";
                case PlayerType.GasMaskSoldier:
                    return "Gas Mask Soldier";
                case PlayerType.SpecialForces:
                    return "Special Forces";
                case PlayerType.FuturisticSoldier:
                    return "Futuristic Soldier";
                default:
                    return "Unknown";
            }
        }

        public static int GetHealth(this PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.WarSoldier:
                    return 100;
                case PlayerType.Swat:
                    return 120;
                case PlayerType.GasMaskSoldier:
                    return 90;
                case PlayerType.SpecialForces:
                    return 110;
                case PlayerType.FuturisticSoldier:
                    return 130;
                default:
                    return 100;
            }
        }

        public static float GetSpeed(this PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.WarSoldier:
                    return 5f;
                case PlayerType.Swat:
                    return 4.5f;
                case PlayerType.GasMaskSoldier:
                    return 5.5f;
                case PlayerType.SpecialForces:
                    return 6f;
                case PlayerType.FuturisticSoldier:
                    return 6.5f;
                default:
                    return 5f;
            }
        }

        public static float GetResistance(this PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.WarSoldier:
                    return 0.8f;
                case PlayerType.Swat:
                    return 0.9f;
                case PlayerType.GasMaskSoldier:
                    return 0.7f;
                case PlayerType.SpecialForces:
                    return 0.85f;
                case PlayerType.FuturisticSoldier:
                    return 0.95f;
                default:
                    return 0.8f;
            }
        }
    }
}