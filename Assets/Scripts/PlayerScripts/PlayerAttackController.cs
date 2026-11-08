using System.Collections.Generic;
using UnityEngine;
using PlayerData;
public class PlayerAttackController : MonoBehaviour
{
    [Header("Player Right Arm")]
    [SerializeField] private GameObject rightArmSwat;
    [SerializeField] private GameObject rightArmSpecialForces;
    [SerializeField] private GameObject rightArmWarSoldier;
    [SerializeField] private GameObject rightArmGasMaskSoldier;
    [SerializeField] private GameObject rightArmFuturistic;

    [Header("Player Left Arm")]
    [SerializeField] private GameObject leftArmSwat;
    [SerializeField] private GameObject leftArmSpecialForces;
    [SerializeField] private GameObject leftArmWarSoldier;
    [SerializeField] private GameObject leftArmGasMaskSoldier;
    [SerializeField] private GameObject leftArmFuturistic;

    //
    private Animator animator;
    public WeaponType currentWeaponType;
    private PlayerTypeEnum currentPlayerType;

    public List<int> weaponTypes = new List<int> { 0, 1, 2, 3, 4, 5, 6 }; // List of weapon types as integers
    void Start()
    {
        animator = GetComponent<Animator>();
        // ChangeWeapon(currentWeaponType);
    }

    void Update()
    {
        if (CustomInput.Instance.nextWeapon)
        {
            int currentIndex = weaponTypes.IndexOf((int)currentWeaponType);
            int nextIndex = (currentIndex + 1) % weaponTypes.Count;
            ChangeWeapon((WeaponType)weaponTypes[nextIndex]);
        }else if (CustomInput.Instance.prevWeapon)
        {
            int currentIndex = weaponTypes.IndexOf((int)currentWeaponType);
            int prevIndex = (currentIndex - 1 + weaponTypes.Count) % weaponTypes.Count;
            ChangeWeapon((WeaponType)weaponTypes[prevIndex]);
        }
        //attack
        if (CustomInput.Instance.attackSingle)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void Initialize(PlayerTypeEnum playerType, WeaponType startingWeapon = WeaponType.punch)
    {
        currentPlayerType = playerType;
        currentWeaponType = startingWeapon;
        SetWeaponAnimation(startingWeapon);
        EquipWeapon(startingWeapon);
    }

    
    public void ChangeWeapon(WeaponType newWeaponType)
    {
        currentWeaponType = newWeaponType;
        SetWeaponAnimation(newWeaponType);
        EquipWeapon(newWeaponType);
    }


    private void SetWeaponAnimation(WeaponType weaponType)
    {
        animator.SetInteger("WeaponType", (int)weaponType);
        animator.SetTrigger("Equip");
    }

    private void EquipWeapon(WeaponType weaponType)
    {
        // look at all the arm and remove the children
        rightArmWarSoldier.RemoveAllChildren();
        leftArmWarSoldier.RemoveAllChildren();
        rightArmSwat.RemoveAllChildren();
        leftArmSwat.RemoveAllChildren();
        rightArmGasMaskSoldier.RemoveAllChildren();
        leftArmGasMaskSoldier.RemoveAllChildren();
        rightArmSpecialForces.RemoveAllChildren();
        leftArmSpecialForces.RemoveAllChildren();
        rightArmFuturistic.RemoveAllChildren();
        leftArmFuturistic.RemoveAllChildren();
        // then add the new weapon to the correct arm based on the player type
        GameObject weaponPrefab = WeaponManager.Instance.GetWeaponPrefab(weaponType);
        WeaponController weaponController;
        if (weaponPrefab != null && weaponPrefab.TryGetComponent<WeaponController>(out weaponController))
        {
            Debug.Log("Weapon Controller found for: " + weaponType.GetWeaponName());
            Transform parentTransform;
            switch (currentPlayerType)
            {
                case PlayerTypeEnum.WarSoldier:
                    parentTransform = weaponType.isGrenade()? 
                        leftArmWarSoldier.transform : 
                        rightArmWarSoldier.transform;
                    Instantiate(
                        weaponPrefab,
                        weaponController.weaponPosition,
                        Quaternion.Euler(weaponController.weaponRotation),
                        parentTransform
                    );
                    break;
                case PlayerTypeEnum.Swat:
                    parentTransform = weaponType.isGrenade()? 
                        leftArmSwat.transform : 
                        rightArmSwat.transform;
                    Instantiate(
                        weaponPrefab,
                        weaponController.weaponPosition,
                        Quaternion.Euler(weaponController.weaponRotation),
                        parentTransform
                    );
                    break;
                case PlayerTypeEnum.GasMaskSoldier:
                    parentTransform = weaponType.isGrenade()? 
                        leftArmGasMaskSoldier.transform : 
                        rightArmGasMaskSoldier.transform;
                    Instantiate(
                        weaponPrefab,
                        weaponController.weaponPosition,
                        Quaternion.Euler(weaponController.weaponRotation),
                        parentTransform
                    );
                    break;
                case PlayerTypeEnum.SpecialForces:
                    parentTransform = weaponType.isGrenade()? 
                        leftArmSpecialForces.transform : 
                        rightArmSpecialForces.transform;
                    Instantiate(
                        weaponPrefab,
                        weaponController.weaponPosition,
                        Quaternion.Euler(weaponController.weaponRotation),
                        parentTransform
                    );
                    break;
                case PlayerTypeEnum.FuturisticSoldier:
                    parentTransform = weaponType.isGrenade()? 
                        leftArmFuturistic.transform : 
                        rightArmFuturistic.transform;
                    Instantiate(
                        weaponPrefab,
                        weaponController.weaponPosition,
                        Quaternion.Euler(weaponController.weaponRotation),
                        parentTransform
                    );
                    Debug.Log("Equipped Futuristic Soldier Weapon: " + weaponType.GetWeaponName());
                    break;
                default:
                    break;
            }
        }
    }
}
