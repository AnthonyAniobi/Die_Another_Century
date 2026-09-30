using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLevelManager : MonoBehaviour
{
    static public GameLevelManager instance { get; private set; }

    [SerializeField] private int currentLevel = 1; // the current level of the game
    [SerializeField] private List<GameObject> enemyPrefabs; // list of enemy prefabs to spawn
    [SerializeField] private List<GameObject> npcPrefabs; // list of NPC prefabs to spawn
    [SerializeField] private GameObject playerPrefab; // Reference to the player prefab

    [SerializeField] private TextMeshProUGUI rescuedCountText;
    [SerializeField] private GameObject rescueProgressLabel;
    [SerializeField] private TextMeshProUGUI playersHealthText;

    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI infoText;

    // Game Over and Intro Panels
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameIntroPanel;
    [SerializeField] private bool gameEnded = false;
    public bool gameStarted = false;
    [SerializeField] private TextMeshProUGUI gameOverTitleText;
    [SerializeField] private TextMeshProUGUI gameOverMessageText;

    [Header("UI Buttons")]
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button startButton;

    private Vector3 playerStartPosition = new Vector3(23.9f, 2.1f, 58.7f); // Set the player's starting position

    
    

    
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

    void OnEnable()
    {
        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestartButtonPressed);
        if (menuButton != null)
            menuButton.onClick.AddListener(OnMenuButtonPressed);
        if (startButton != null)
            startButton.onClick.AddListener(StartGame);
    }

    void OnDestroy()
    {
        if (restartButton != null)
            restartButton.onClick.RemoveListener(OnRestartButtonPressed);
        if (menuButton != null)
            menuButton.onClick.RemoveListener(OnMenuButtonPressed);
        if (startButton != null)
            startButton.onClick.RemoveListener(StartGame);
    }

    void Awake()
    {
        Time.timeScale = 1f;
        gameIntroPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        removeAllEnemiesAndNPCs();
        infoPanel.SetActive(false);
    }

    void Update()
    {
        if (!gameStarted)
        {
            return;
        }

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
            
            if (rescuedCount >= totalNPCsToRescue)
            {
                EndGame(true, "All civilians rescued!");
            }
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

    // call this to start any game afreash
    public void ResetGameLevel()
    {
        Time.timeScale = 1f;
        // let reset game remove all existing enemies and npcs in the scene
       removeAllEnemiesAndNPCs();
        // show intro panel
        gameIntroPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameEnded = false;
        gameStarted = false;
        // Reset the game level to its initial state
        rescuedCount = 0;
        totalNPCsToRescue = 0;
        currentRescuedNPC = null;
        infoPanel.SetActive(false);
        currentHealth = maxHealth;
        UpdateUI();
        // Spawn the player at the starting position
        if (playerPrefab != null)
        {
            Instantiate(playerPrefab, playerStartPosition, Quaternion.identity);
        }
    }

    private void removeAllEnemiesAndNPCs()
    {
        // Remove all enemies
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }

        // Remove all NPCs
        foreach (var npc in GameObject.FindGameObjectsWithTag("NPC"))
        {
            Destroy(npc);
        }

        // Remove the player if it exists
        GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");
        if (existingPlayer != null)
        {
            Destroy(existingPlayer);
        }
    }

   
    public void EndGame(bool playerWon, string message)
    {
        Cursor.lockState = CursorLockMode.None;
        ShowInfoPanel(message);
        gameOverPanel.SetActive(true);
        gameEnded = true;
        gameStarted = false;
        if (playerWon)
        {
            gameOverTitleText.text = "You Win!";
            gameOverMessageText.text = message;
        }
        else
        {
            gameOverTitleText.text = "Game Over";
            gameOverMessageText.text = message;
        }
        Time.timeScale = 0f;
        
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

    /// <summary>
    /// buttion actions these are actions referenced in the ui button
    /// </summary>
    public void OnRestartButtonPressed(){
        // remove player and set the player back to the start position
        ResetGameLevel();

    }

    public void OnMenuButtonPressed()
    {
         Time.timeScale = 1f;
       SceneManager.LoadScene("WelcomeScene");
    }
     // call this when player presses the start button in the intro panel
    public void StartGame() 
    {
        if (gameStarted)
        {
            return;
        }

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        gameIntroPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameEnded = false;
        gameStarted = true;
        rescuedCount = 0;
        totalNPCsToRescue = 0;
        currentRescuedNPC = null;
        currentHealth = maxHealth;
        GameLevelData levelData = GameLevelData.levels[currentLevel];
        SpawnInitialCharacters(levelData.characterPositions);

        // Spawn the player at the starting position
        if (playerPrefab != null)
        {
            Instantiate(playerPrefab, playerStartPosition, Quaternion.identity);
        }

        UpdateUI();
    }

}
