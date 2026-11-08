using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Player Right Arm")]
    [SerializeField] private GameObject rightArmSwat;
    [SerializeField] private GameObject rightArmSpecialForces;
    [SerializeField] private GameObject rightArmWarSoldier;
    [SerializeField] private GameObject rightArmGasMaskSoldier;
    [SerializeField] private GameObject rightArmFuturistic;
    
    //
    private Animator animator;
    public WeaponType currentWeaponType;

    public List<int> weaponTypes = new List<int> { 0, 1, 2, 3, 4, 5, 6 }; // List of weapon types as integers
    void Start()
    {
        animator = GetComponent<Animator>();
        ChangeWeapon(currentWeaponType);
    }

    void Update()
    {
        if (CustomInput.Instance.nextWeapon)
        {
            int currentIndex = weaponTypes.IndexOf((int)currentWeaponType);
            int nextIndex = (currentIndex + 1) % weaponTypes.Count;
            ChangeWeapon((WeaponType)weaponTypes[nextIndex]);
            Debug.Log("Next Weapon: " + ((WeaponType)weaponTypes[nextIndex]).GetWeaponName());
        }else if (CustomInput.Instance.prevWeapon)
        {
            int currentIndex = weaponTypes.IndexOf((int)currentWeaponType);
            int prevIndex = (currentIndex - 1 + weaponTypes.Count) % weaponTypes.Count;
            ChangeWeapon((WeaponType)weaponTypes[prevIndex]);
            Debug.Log("Previous Weapon: " + ((WeaponType)weaponTypes[prevIndex]).GetWeaponName());
        }
        //attack
        if (CustomInput.Instance.attackSingle)
        {
            animator.SetTrigger("Attack");
        }
    }

    
    public void ChangeWeapon(WeaponType newWeaponType)
    {
        currentWeaponType = newWeaponType;
        SetWeaponAnimation(newWeaponType);
    }


    private void SetWeaponAnimation(WeaponType weaponType)
    {
        animator.SetInteger("WeaponType", (int)weaponType);
        animator.SetTrigger("Equip");
    }
}
