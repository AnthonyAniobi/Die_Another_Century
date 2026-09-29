using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Transform currentRescuedNPC;
    private int rescuedCount = 0;
    [SerializeField] private TextMeshProUGUI rescuedCountText;
    [SerializeField] private TextMeshProUGUI playersHealthText;


    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void StartGame()
    {
        // Initialize game
    }

    public void SetPlayerIsRescuingNPC(Transform npcTransform)
    {
        currentRescuedNPC = npcTransform;
    }

    public void SetNPCRescued()
    {
        if(currentRescuedNPC != null)
        {
            // You can add any additional logic here, such as updating the score or triggering an event.
            Debug.Log("NPC rescued: " + currentRescuedNPC.name);
            currentRescuedNPC = null; // Reset the reference after rescue
            rescuedCount++;
        }
    }
}
