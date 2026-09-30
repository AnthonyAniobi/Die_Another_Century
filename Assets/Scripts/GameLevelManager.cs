using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    static public GameLevelManager instance { get; private set; }

    [SerializeField] private int currentLevel = 1; // the current level of the game
    [SerializeField] private List<GameObject> enemyPrefabs; // list of enemy prefabs to spawn
    [SerializeField] private List<GameObject> npcPrefabs; // list of NPC prefabs to spawn

    [SerializeField] private TextMeshProUGUI rescuedCountText;
    [SerializeField] private GameObject rescueProgressLabel;
    [SerializeField] private TextMeshProUGUI playersHealthText;

    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI infoText;

    
    public bool RescueInProgress {get => currentRescuedNPC != null;}
    private float rescuedCount = 0;
    private float totalNPCsToRescue = 0;
    private int maxHealth = 3;
    private int currentHealth;
    private Transform currentRescuedNPC = null;
    private bool canSpawnEnemies = true;

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

    void Awake()
    {
        ResetGameLevel();
    }

    void Update()
    {
        SpawnEnemiesAtIntervals();
    }


    public void SetPlayerIsRescuingNPC(Transform npcTransform)
    {
        currentRescuedNPC = npcTransform;
        UpdateUI();
    }

    public void SetNPCRescued()
    {
        if(currentRescuedNPC != null)
        {
            // You can add any additional logic here, such as updating the score or triggering an event.
            Debug.Log("NPC rescued: " + currentRescuedNPC.name);
            currentRescuedNPC = null; // Reset the reference after rescue
            rescuedCount++;
            UpdateUI();
            ShowInfoPanel("NPC Rescued! Total Rescued: " + rescuedCount + "/" + totalNPCsToRescue);
        }
    }

    private void SpawnInitialCharacters(List<CharacterPosition> characterPositions)
    {
        
        foreach (CharacterPosition characterPosition in characterPositions)
        {
            if (characterPosition.characterType == CharacterPosition.CharacterType.Enemy)
            {
                // Instantiate enemy prefab at characterPosition.position
                // Example: Instantiate(enemyPrefab, characterPosition.position, Quaternion.identity);
                int randomIndex = Random.Range(0, enemyPrefabs.Count);
                GameObject enemyPrefab = enemyPrefabs[randomIndex];
                Vector3 enemyPatrolCenter = characterPosition.position;
                // all preset enemy prefabs will be idle in a single spot
                enemyPrefab.GetComponent<EnemyController>().startingState = EnemyController.EnemyStartingState.Idle;
                enemyPrefab.GetComponent<EnemyController>().patrolCenter = enemyPatrolCenter;
                Instantiate(enemyPrefab, characterPosition.position, characterPosition.rotation);
            }
            else if (characterPosition.characterType == CharacterPosition.CharacterType.NPC)
            {
                // Instantiate NPC prefab at characterPosition.position
                // Example: Instantiate(npcPrefab, characterPosition.position, Quaternion.identity);
                int randomIndex = Random.Range(0, npcPrefabs.Count);
                GameObject npcPrefab = npcPrefabs[randomIndex];
                Instantiate(npcPrefab, characterPosition.position, characterPosition.rotation);
                // Increment the total NPCs to rescue count
                totalNPCsToRescue++;
            }
        }
    }

    private void SpawnEnemiesAtIntervals()
    {
        if(RescueInProgress && canSpawnEnemies)
        {
            GameLevelData levelData = GameLevelData.levels[currentLevel];
            Vector3 randomSpawnPosition = levelData.enemySpawnPositions[Random.Range(0, levelData.enemySpawnPositions.Count)];
            int randomIndex = Random.Range(0, enemyPrefabs.Count);
            GameObject enemyPrefab = enemyPrefabs[randomIndex];
            // set the center where the enemy will get to before they start patrolling
            Vector3 patrolCenter = levelData.enemySearchAreaCenter;
            float patrolRadius = levelData.enemySearchAreaRadius;
            // get a random point within the patrol radius for the enemy to patrol to
            float randomx = Random.Range(patrolCenter.x - patrolRadius, patrolCenter.x + patrolRadius);
            float randomz = Random.Range(patrolCenter.z - patrolRadius, patrolCenter.z + patrolRadius);
            Vector3 randomPatrolPoint = new Vector3(randomx, randomSpawnPosition.y, randomz);
            enemyPrefab.GetComponent<EnemyController>().patrolCenter = new Vector2(randomPatrolPoint.x, randomPatrolPoint.z);
            enemyPrefab.GetComponent<EnemyController>().startingState = EnemyController.EnemyStartingState.Patrol;
            Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
            canSpawnEnemies = false; // stop from spawning more enemies until the next interval
            Invoke("EnableEnemySpawning", levelData.enemySpawnInterval);
        }
    }

    private void EnableEnemySpawning()
    {
        canSpawnEnemies = true;
    }

    public void ResetGameLevel()
    {
        // Reset the game level to its initial state
        rescuedCount = 0;
        currentRescuedNPC = null;
        infoPanel.SetActive(false);
        currentHealth = maxHealth;
        GameLevelData levelData = GameLevelData.levels[currentLevel];
        // Use the levelData to set up the game level
        // SpawnInitialCharacters(levelData.characterPositions);
        UpdateUI();
    }

    private void UpdateUI()
    {
        rescuedCountText.text = $"{rescuedCount} / {totalNPCsToRescue}";
        playersHealthText.text = $"{currentHealth} / {maxHealth}";
        rescueProgressLabel.SetActive(RescueInProgress);
    }

    public void ShowInfoPanel(string message)
    {
        infoText.text = message;
        infoPanel.SetActive(true);
        StartCoroutine(HideInfoPanelAfterDelay(3f)); // Hide after 3 seconds
    }

    IEnumerator HideInfoPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        infoPanel.SetActive(false);
    }
}
