using UnityEngine;

public class EnemyLookout : MonoBehaviour
{
    [SerializeField] private float eyeDistance = 30f; // Distance the enemy can see
    [SerializeField] private Transform playerEyeLevelTransform; // Reference to the player's eye level
    [SerializeField] private float viewRadius = 2f; // Radius of the lookout area
    // Update is called once per frame
    void Update()
    {
        if(Physics.SphereCast(
            playerEyeLevelTransform.position, 
            viewRadius,
            playerEyeLevelTransform.forward,
            out RaycastHit hit,
            eyeDistance
        ))
        {
            if(hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player sighted by enemy lookout!");
            } 
            DebugExtension.DrawSphereCast(playerEyeLevelTransform.position + playerEyeLevelTransform.forward * hit.distance, viewRadius, playerEyeLevelTransform.forward, hit.distance, Color.green);
        }
        else
        {
            DebugExtension.DrawSphereCast(playerEyeLevelTransform.position + playerEyeLevelTransform.forward * hit.distance, viewRadius, playerEyeLevelTransform.forward, eyeDistance, Color.red);
        }
    }
}
