using UnityEngine;

public class EnemyType : MonoBehaviour
{
    public EnemyTypeEnum enemyType;

    [SerializeField] private GameObject bruteObject;
    [SerializeField] private GameObject ninjaObject;
    [SerializeField] private Avatar bruteAvatar;
    [SerializeField] private Avatar ninjaAvatar;

    void Start()
    {
       Initialize(enemyType); 
    }

    public void Initialize(EnemyTypeEnum type)
    {
        enemyType = type;
        SetAvatar();
        SetActiveEnemyObject();
        GetComponent<EnemyController>().Initialize(true);
        GetComponent<EnemyHealth>().Initialize(enemyType.GetMaxHealth());
        GetComponent<EnemyLookout>().Initialize(enemyType);
        GetComponent<EnemyAttack>().Initialize(enemyType);
    }


    private void SetActiveEnemyObject()
    {
        switch (enemyType)
        {
            case EnemyTypeEnum.Brute:
                bruteObject.SetActive(true);
                ninjaObject.SetActive(false);
                // Destroy(ninjaObject); // Destroy the ninja object to free up resources
                break;
            case EnemyTypeEnum.Ninja:
                bruteObject.SetActive(false);
                ninjaObject.SetActive(true);
                // Destroy(bruteObject); // Destroy the brute object to free up resources
                break;
            default:
                Debug.LogWarning("Unknown enemy type: " + enemyType);
                break;
        }
    }

    private void SetAvatar()
    {
        Animator animator = GetComponent<Animator>();
        switch (enemyType)
        {
            case EnemyTypeEnum.Brute:
                animator.avatar = bruteAvatar;
                break;
            case EnemyTypeEnum.Ninja:
                animator.avatar = ninjaAvatar;
                break;
            default:
                Debug.LogWarning("Unknown enemy type: " + enemyType);
                break;
        }
    }
}
