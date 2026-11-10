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
        Initialize(npcType);
    }

    public void Initialize(NpcTypeEnum npcType)
    {
        SetNpcType(npcType);
        GetComponent<NpcLook>().Initialize(npcType);
    }

    private void SetNpcType(NpcTypeEnum type)
    {
        npcType = type;
        switch (npcType)
        {
            case NpcTypeEnum.Worker:
                workerObject.SetActive(true);
                womanObject.SetActive(false);
                GetComponent<Animator>().avatar = workerAvatar;
                break;
            case NpcTypeEnum.Woman:
                workerObject.SetActive(false);
                womanObject.SetActive(true);
                GetComponent<Animator>().avatar = womanAvatar;
                break;
        }
    }
}
