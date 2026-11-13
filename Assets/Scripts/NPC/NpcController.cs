using UnityEngine;

public class NpcController : MonoBehaviour
{
    [SerializeField] private float playerDiscoveryDistance = 2.0f; // The range at which the NPC will detect the player
    [SerializeField] private float npcRunDistance = 1.5f; // The distance from the player at which the NPC will start running towards the player
    
    [SerializeField] private float npcLaggingDistance = 0.2f; // The distance the NPC will lag behind the player when following
    [SerializeField] private float npcLostDistance = 5.0f; // The distance at which the NPC will stop following the player
    
    bool isRescuing = false;
    bool isRescued = false;
    // internal variables
    internal Vector2 move = Vector2.zero; // The direction the NPC is moving in
    internal bool sprinting = false; // Whether the NPC is sprinting or not
    internal NpcStateEnum npcState = NpcStateEnum.InCaptivity; // The current state of the NPC
    
    
    private Transform playerTransform;

    //
    void Start()
    {
        
    }

    void Update()
    {
        if(playerTransform == null)
        {
            return; // No player to follow
        }

        

        if(IsPlayerInCloseRange())
        {
            StopMoving();
            npcState = NpcStateEnum.Rescuing;
            isRescuing = true;
        }
        else
        {
            if(Vector3.Distance(transform.position, playerTransform.position) > npcLostDistance)
            {
                StopMoving();
                npcState = NpcStateEnum.Lost;
                isRescuing = false;
            }
            else
            {
                move = new Vector2(
                    playerTransform.position.x - transform.position.x, 
                    playerTransform.position.z - transform.position.z).normalized;
                sprinting = Vector3.Distance(transform.position, playerTransform.position) > npcRunDistance;
                npcState = NpcStateEnum.Rescuing;
                isRescuing = true;
            }
        }
    }
    
    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;
        npcState = NpcStateEnum.Rescuing;
    }

    private bool IsPlayerInCloseRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        return distanceToPlayer <= npcLaggingDistance;
    }
    
    private void StopMoving()
    {
        move = Vector2.zero;
        sprinting = false;
    }
}
