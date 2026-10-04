using System.Collections.Generic;
using UnityEngine;

public class GameLevelData
{
    public List<CharacterPosition> characterPositions; 

    public List<Vector3> enemySpawnPositions; // positions where enemies can spawn

    public float enemySpawnInterval; // time interval between enemy spawns

    public Vector3 enemySearchAreaCenter; // where enemy will search for the player

    public float enemySearchAreaRadius; // radius of the search area

    static public readonly Dictionary<int, GameLevelData> levels = new Dictionary<int, GameLevelData>
    {
        {
            1,
            new GameLevelData
            {
                characterPositions = new List<CharacterPosition>
                {
                    new CharacterPosition { position = new Vector3(-16f, 1f, -38f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.NPC , isOnPatrol = true},
                    new CharacterPosition { position = new Vector3(11f, 1f, 44f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy ,isOnPatrol = true},  // correct
                    new CharacterPosition { position = new Vector3(11f, 1f, 9f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy ,isOnPatrol = true}, // correct
                    new CharacterPosition { position = new Vector3(-3f, 1f, -22f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy ,isOnPatrol = true}, // correct
                    new CharacterPosition { position = new Vector3(-13f, 1f, -4.1f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy, isOnPatrol = true },// correct
                },
                enemySpawnPositions = new List<Vector3>
                {
                    new Vector3(58f, 1f, 26f),
                },
                enemySpawnInterval = 8.0f,
                enemySearchAreaCenter = new Vector3(9.6f, 1f, 33.8f),
                enemySearchAreaRadius = 15.0f
            }
        },
        {
            2,
            new GameLevelData
            {
                characterPositions = new List<CharacterPosition>
                {
                    new CharacterPosition { position = new Vector3(-16f, 1f, -38f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.NPC , isOnPatrol = true},
                    new CharacterPosition { position = new Vector3(29.4f, 1f, -40f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.NPC, isOnPatrol = true},
                    new CharacterPosition { position = new Vector3(11f, 1f, 44f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy,isOnPatrol = true },  // correct
                    new CharacterPosition { position = new Vector3(11f, 1f, 9f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy,isOnPatrol = true }, // correct
                    new CharacterPosition { position = new Vector3(-3f, 1f, -22f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy ,isOnPatrol = true}, // correct
                    new CharacterPosition { position = new Vector3(-13f, 1f, -4.1f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy ,isOnPatrol = true},// correct
                    new CharacterPosition { position = new Vector3(-6.7f, 1f, 52.1f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy ,isOnPatrol = true},// correct
                    new CharacterPosition { position = new Vector3(-15.4f, 1f, 9.1f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy ,isOnPatrol = true},// correct
                },
                enemySpawnPositions = new List<Vector3>
                {
                    new Vector3(-5f, 0f, -5f),
                    new Vector3(5f, 0f, -5f),
                    new Vector3(-10f, 0f, 10f)
                },
                enemySpawnInterval = 1.5f,
                enemySearchAreaCenter = new Vector3(0f, 0f, 0f),
                enemySearchAreaRadius = 20.0f
            }
        },
        {
            3,
            new GameLevelData
            {
                characterPositions = new List<CharacterPosition>
                {
                    new CharacterPosition { position = new Vector3(-16f, 1f, -38f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.NPC , isOnPatrol = true},
                    new CharacterPosition { position = new Vector3(29.4f, 1f, -40f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.NPC, isOnPatrol = true},
                    new CharacterPosition { position = new Vector3(16.87f, 12.76f, -46.5f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.NPC, isOnPatrol = true},
                    new CharacterPosition { position = new Vector3(11f, 1f, 44f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy,isOnPatrol = true },  // correct
                    new CharacterPosition { position = new Vector3(11f, 1f, 9f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy,isOnPatrol = true }, // correct
                    new CharacterPosition { position = new Vector3(-3f, 1f, -22f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy ,isOnPatrol = true}, // correct
                    new CharacterPosition { position = new Vector3(-13f, 1f, -4.1f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy ,isOnPatrol = true},// correct
                    new CharacterPosition { position = new Vector3(-6.7f, 1f, 52.1f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy ,isOnPatrol = true},// correct
                    new CharacterPosition { position = new Vector3(-15.4f, 1f, 9.1f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy ,isOnPatrol = true},// correct
                    new CharacterPosition { position = new Vector3(30.9f, 1f, 43.9f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy ,isOnPatrol = false},// correct
                    new CharacterPosition { position = new Vector3(-2f, 12.76f, -46.5f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy ,isOnPatrol = false},// correct
                },
                enemySpawnPositions = new List<Vector3>
                {
                    new Vector3(-5f, 0f, -5f),
                    new Vector3(5f, 0f, -5f),
                    new Vector3(-10f, 0f, 10f)
                },
                enemySpawnInterval = 1.5f,
                enemySearchAreaCenter = new Vector3(0f, 0f, 0f),
                enemySearchAreaRadius = 20.0f
            }
        }
    };

}


