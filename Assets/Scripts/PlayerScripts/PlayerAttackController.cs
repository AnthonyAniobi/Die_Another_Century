using System.Collections.Generic;
using UnityEngine;
using PlayerData;
public class PlayerAttackController : MonoBehaviour
{
    [Header("Player Attack Controller")]
    [Space(10)]
    [Header("Aim UI")]
    [SerializeField] private GameObject aimUI;

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
    private GameObject weapon;
    public List<int> weaponTypes = new List<int> { 0, 1, 2, 3, 4, 5, 6 }; // List of weapon types as integers
    private bool isAttacking = false;
    void Start()
    {
        animator = GetComponent<Animator>();
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
            Attack();
        }
    }

    public void Initialize(PlayerTypeEnum playerType, WeaponType startingWeapon = WeaponType.punch)
    {
        currentPlayerType = playerType;
        currentWeaponType = startingWeapon;
        SetWeaponAnimation(startingWeapon);
        EquipWeapon(startingWeapon);
        aimUI.SetActive(false);
    }

    public void ChangeWeapon(WeaponType newWeaponType)
    {
        currentWeaponType = newWeaponType;
        SetWeaponAnimation(newWeaponType);
        EquipWeapon(newWeaponType);
        isAttacking = false; // Reset the attacking state when changing weapons
        EndAttack();
    }


    private void SetWeaponAnimation(WeaponType weaponType)
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
        animator.SetInteger("WeaponType", (int)weaponType);
        animator.SetTrigger("Equip");
    }

    private void EquipWeapon(WeaponType weaponType)
    {
        // checks for aim ui for the weapon type
        if(IsAimedWeapon)
        {
            aimUI.SetActive(true);
        }
        else
        {
            aimUI.SetActive(false);
        }
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
        if (weaponType == WeaponType.punch)
        {
            weapon = null;
            return; // No weapon to equip for punch
        }
        
        // then add the new weapon to the correct arm based on the player type
        GameObject weaponPrefab = WeaponManager.Instance.GetWeaponPrefab(weaponType);
        if (weaponPrefab != null)
        {
            Debug.Log("Weapon Controller found for: " + weaponType.GetWeaponName());
            Transform parentTransform;
            Vector3 weaponOffset =  weaponType.GetWeaponOffset(currentPlayerType);
            Vector3 weaponRotation =  weaponType.GetWeaponRotation(currentPlayerType);
            switch (currentPlayerType)
            {
                case PlayerTypeEnum.WarSoldier:
                    parentTransform = weaponType.isGrenade()? 
                        leftArmWarSoldier.transform : 
                        rightArmWarSoldier.transform;
                    weapon = Instantiate(
                        weaponPrefab,
                        weaponOffset,
                        Quaternion.Euler(weaponRotation),
                        parentTransform
                    );
                    weapon.transform.localPosition = weaponOffset;
                    weapon.transform.localRotation = Quaternion.Euler(weaponRotation);
                    break;
                case PlayerTypeEnum.Swat:
                    parentTransform = weaponType.isGrenade()? 
                        leftArmSwat.transform : 
                        rightArmSwat.transform;
                    weapon = Instantiate(
                        weaponPrefab,
                        weaponOffset,
                        Quaternion.Euler(weaponRotation),
                        parentTransform
                    );
                    weapon.transform.localPosition = weaponOffset;
                    weapon.transform.localRotation = Quaternion.Euler(weaponRotation);
                    break;
                case PlayerTypeEnum.GasMaskSoldier:
                    parentTransform = weaponType.isGrenade()? 
                        leftArmGasMaskSoldier.transform : 
                        rightArmGasMaskSoldier.transform;
                    weapon = Instantiate(
                        weaponPrefab,
                        weaponOffset,
                        Quaternion.Euler(weaponRotation),
                        parentTransform
                    );
                    weapon.transform.localPosition = weaponOffset;
                    weapon.transform.localRotation = Quaternion.Euler(weaponRotation);
                    break;
                case PlayerTypeEnum.SpecialForces:
                    parentTransform = weaponType.isGrenade()? 
                        leftArmSpecialForces.transform : 
                        rightArmSpecialForces.transform;
                    weapon = Instantiate(
                        weaponPrefab,
                        weaponOffset,
                        Quaternion.Euler(weaponRotation),
                        parentTransform
                    );
                    weapon.transform.localPosition = weaponOffset;
                    weapon.transform.localRotation = Quaternion.Euler(weaponRotation);
                    break;
                case PlayerTypeEnum.FuturisticSoldier:
                    parentTransform = weaponType.isGrenade()? 
                        leftArmFuturistic.transform : 
                        rightArmFuturistic.transform;
                    weapon = Instantiate(
                        weaponPrefab,
                        weaponOffset,
                        Quaternion.Euler(weaponRotation),
                        parentTransform
                    );
                    weapon.transform.localPosition = weaponOffset;
                    weapon.transform.localRotation = Quaternion.Euler(weaponRotation);
                    break;
                default:
                    break;
            }
        }
    }

    private void Attack()
    {
        if(currentWeaponType == WeaponType.punch) return;
        if(isAttacking) return; // Prevent starting a new attack if already attacking
        isAttacking = true;
        if(IsAimedWeapon){ // only face camera for aimed weapons
            gameObject.GetComponent<PlayerController>().FaceCamera();
        }
        animator.SetTrigger("Attack");
        weapon.GetComponent<WeaponController>().StartAttack(IsAimedWeapon);
        EndAttack();
    }

    private void EndAttack()
    {
        float delay = currentWeaponType.GetWeaponDelay();
        System.Collections.IEnumerator _EndAttack()
        {
            yield return new WaitForSeconds(delay);
            isAttacking = false;
            weapon.GetComponent<WeaponController>().StopAttack();
        }
        StartCoroutine(_EndAttack());
    }

    private bool IsAimedWeapon=> currentWeaponType == WeaponType.Pistol || currentWeaponType == WeaponType.Rifle || currentWeaponType == WeaponType.Shotgun || currentWeaponType == WeaponType.Grenade || currentWeaponType == WeaponType.RPG;
}
