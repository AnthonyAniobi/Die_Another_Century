using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    static public GameLevelManager instance { get; private set; }

    [SerializeField] private int currentLevel = 1; // the current level of the game
    [SerializeField] private List<GameObject> enemyPrefabs; // list of enemy prefabs to spawn
    [SerializeField] private List<GameObject> npcPrefabs; // list of NPC prefabs to spawn

    public Vector2 patrolCenter;
    public float patrolRadius = 10f;
    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        
    }

    void StartGameLevel(int levelNumber)
    {
        GameLevelData levelData = GameLevelData.levels[levelNumber];
        // Use the levelData to set up the game level
        patrolCenter = new Vector2(levelData.enemySearchAreaCenter.x, levelData.enemySearchAreaCenter.z);
        patrolRadius = levelData.enemySearchAreaRadius;
        SpawnInitialCharacters(levelData.characterPositions);
    }

    private void SpawnInitialCharacters(List<CharacterPosition> characterPositions)
    {
        foreach (CharacterPosition characterPosition in characterPositions)
        {
            if (characterPosition.characterType == CharacterPosition.CharacterType.Enemy)
            {
                // Instantiate enemy prefab at characterPosition.position
                // Example: Instantiate(enemyPrefab, characterPosition.position, Quaternion.identity);
            }
            else if (characterPosition.characterType == CharacterPosition.CharacterType.NPC)
            {
                // Instantiate NPC prefab at characterPosition.position
                // Example: Instantiate(npcPrefab, characterPosition.position, Quaternion.identity);
            }
        }
    }
}
