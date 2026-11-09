using UnityEngine;

public class NpcType : MonoBehaviour
{
    public NpcTypeEnum npcType;
    [SerializeField] private GameObject workerObject;
    [SerializeField] private GameObject womanObject;
    [SerializeField] private Avatar workerAvatar;
    [SerializeField] private Avatar womanAvatar;

    void Start()
    {
        
    }

    public void Initialize()
    {
        Animator animator = GetComponent<Animator>();
        animator.avatar = npcType == NpcTypeEnum.Worker ? workerAvatar : womanAvatar;
        
    }
}
