using UnityEngine;

public class EnemyData
{
    
}

public enum EnemyTypeEnum
{
    Brute = 0,
    Ninja = 1
}


public static class EnemyTypeExtensions
{
    public static int GetMaxHealth(this EnemyTypeEnum enemyType)
    {
        switch (enemyType)
        {
            case EnemyTypeEnum.Brute:
                return 5; // Brute has more health
            case EnemyTypeEnum.Ninja:
                return 3; // Ninja has less health
            default:
                return 1; // Default health for unknown types
        }
    }

    public static float GetMoveSpeed(this EnemyTypeEnum enemyType)
    {
        switch (enemyType)
        {
            case EnemyTypeEnum.Brute:
                return 2f; // Brute is slower
            case EnemyTypeEnum.Ninja:
                return 4f; // Ninja is faster
            default:
                return 1f; // Default speed for unknown types
        }
    }

    public static float GetAttackDelay(this EnemyTypeEnum enemyType)
    {
        switch (enemyType)
        {
            case EnemyTypeEnum.Brute:
                return 4f; // Brute has a longer attack delay
            case EnemyTypeEnum.Ninja:
                return 1.2f; // Ninja has a shorter attack delay
            default:
                return 1f; // Default attack delay for unknown types
        }
    }
}