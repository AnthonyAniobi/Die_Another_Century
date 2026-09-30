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
                    new CharacterPosition { position = new Vector3(0f, 0f, 5f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.NPC },
                    new CharacterPosition { position = new Vector3(10f, 0f, 10f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy }
                },
                enemySpawnPositions = new List<Vector3>
                {
                    new Vector3(-5f, 0f, -5f),
                    new Vector3(5f, 0f, -5f)
                },
                enemySpawnInterval = 2.0f,
                enemySearchAreaCenter = new Vector3(0f, 0f, 0f),
                enemySearchAreaRadius = 15.0f
            }
        },
        {
            2,
            new GameLevelData
            {
                characterPositions = new List<CharacterPosition>
                {
                    new CharacterPosition { position = new Vector3(0f, 0f, 5f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.NPC },
                    new CharacterPosition { position = new Vector3(10f, 0f, 10f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy },
                    new CharacterPosition { position = new Vector3(-10f, 0f, -10f), rotation = Quaternion.identity, characterType = CharacterPosition.CharacterType.Enemy }
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


