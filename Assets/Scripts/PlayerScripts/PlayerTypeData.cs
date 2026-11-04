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
                    return Resources.Load<GameObject>("PlayerPrefabs/WarSoldier");
                case PlayerType.Swat:
                    return Resources.Load<GameObject>("PlayerPrefabs/Swat");
                case PlayerType.GasMaskSoldier:
                    return Resources.Load<GameObject>("PlayerPrefabs/GasMaskSoldier");
                case PlayerType.SpecialForces:
                    return Resources.Load<GameObject>("PlayerPrefabs/SpecialForces");
                case PlayerType.FuturisticSoldier:
                    return Resources.Load<GameObject>("PlayerPrefabs/FuturisticSoldier");
                default:
                    return null;
            }
        }
    }
}