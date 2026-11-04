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
    }
}