using UnityEngine;

public class EnemyType : MonoBehaviour
{
    public EnemyTypeEnum enemyType;

    [SerializeField] private GameObject bruteObject;
    [SerializeField] private GameObject ninjaObject;
    void Start()
    {
       Initialize(enemyType); 
    }

    public void Initialize(EnemyTypeEnum type)
    {
        enemyType = type;
        SetActiveEnemyObject();
        GetComponent<EnemyHealth>().Initialize(enemyType.GetMaxHealth());
    }


    private void SetActiveEnemyObject()
    {
        switch (enemyType)
        {
            case EnemyTypeEnum.Brute:
                bruteObject.SetActive(true);
                ninjaObject.SetActive(false);
                break;
            case EnemyTypeEnum.Ninja:
                bruteObject.SetActive(false);
                ninjaObject.SetActive(true);
                break;
            default:
                Debug.LogWarning("Unknown enemy type: " + enemyType);
                break;
        }
    }
}
