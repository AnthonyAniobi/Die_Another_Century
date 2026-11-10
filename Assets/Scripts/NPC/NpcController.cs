using UnityEngine;

public class NpcController : MonoBehaviour
{
    [SerializeField] private float playerDiscoveryDistance = 2.0f; // The range at which the NPC will detect the player
    [SerializeField] private float npcRunDistance = 1.5f; // The distance from the player at which the NPC will start running towards the player
    
    [SerializeField] private float npcLaggingDistance = 0.2f; // The distance the NPC will lag behind the player when following
    [SerializeField] private float npcFollowTime = 0.5f; // The time it takes for the NPC to follow a player after they are detected
    
    bool isRescuing = false;
    bool isRescued = false;
    // internal variables
    internal Vector2 moveDirection = Vector2.zero; // The direction the NPC is moving in
    internal bool sprinting = false; // Whether the NPC is sprinting or not
    internal NpcStateEnum npcState = NpcStateEnum.InCaptivity; // The current state of the NPC
    float followTimer = 0.0f;
    Vector3 playerPosition = Vector3.zero; // The position of the player the NPC is following
    //
    void Start()
    {
        followTimer = npcFollowTime; // Initialize the follow timer
    }

    void Update()
    {
        if(IsPlayerInCloseRange())
        {
            StopMoving();
            npcState = NpcStateEnum.Rescuing;
        }
        else if(followTimer > 0)
        {
            followTimer -= Time.deltaTime;
        }
        else
        {
            // if the enemy has been chasing for too long, reset to patrol
            StopMoving();
            npcState = NpcStateEnum.Lost;
        }
    }
    
    public void SetPlayerTransform(Vector3 player)
    {
        playerPosition = new Vector3(player.x, player.y, player.z);
        moveDirection = new Vector2(
            playerPosition.x - transform.position.x, 
            playerPosition.z - transform.position.z).normalized;
        sprinting = true;
        followTimer = npcFollowTime; // reset the follow timer if the player is still in view
        npcState = NpcStateEnum.Rescuing;
    }

    private bool IsPlayerInCloseRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
        return distanceToPlayer <= npcLaggingDistance;
    }
    
    private void StopMoving()
    {
        moveDirection = Vector2.zero;
        sprinting = false;
        followTimer = npcFollowTime; // reset the follow timer when stopping movement
    }
}
