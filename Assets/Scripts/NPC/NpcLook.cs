using UnityEngine;

public class NpcLook : MonoBehaviour
{
    [SerializeField] private float lookRadius = 5f; // The radius of the area the NPC will look around
    [SerializeField] private float lookDistance = 2f; // The speed at which the NPC will look around
    [SerializeField] private GameObject workerEyeTransform;
    [SerializeField] private GameObject womanEyeTransform;

    private Transform npcEyeTransform;
    void Initialize(NpcTypeEnum npcType)
    {
        switch (npcType)
        {
            case NpcTypeEnum.Worker:
                npcEyeTransform = workerEyeTransform.transform;
                break;
            case NpcTypeEnum.Woman:
                npcEyeTransform = womanEyeTransform.transform;
                break;
            default:
                Debug.LogWarning("Unknown NPC type: " + npcType);
                break;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Physics.SphereCast(
            npcEyeTransform.position, 
            lookRadius,
            npcEyeTransform.forward,
            out RaycastHit hit,
            lookDistance
        ))
        {
            if(hit.collider.CompareTag("Player"))
            {
                // Debug.Log("Player sighted by enemy lookout!");
                gameObject.GetComponent<EnemyController>().SetPlayerTransform(hit.transform.position);
            } 
            DebugExtension.DrawSphereCast(npcEyeTransform.position, lookRadius, npcEyeTransform.forward, hit.distance, Color.green);
        }
        else
        {
            DebugExtension.DrawSphereCast(npcEyeTransform.position, lookRadius, npcEyeTransform.forward, lookDistance, Color.red);
        }
    }
}
