using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private Animator animator;
    public WeaponType currentWeaponType;

    public List<int> weaponTypes = new List<int> { 0, 1, 2}; // List of weapon types as integers
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
        }else if (CustomInput.Instance.prevWeapon)
        {
            int currentIndex = weaponTypes.IndexOf((int)currentWeaponType);
            int prevIndex = (currentIndex - 1 + weaponTypes.Count) % weaponTypes.Count;
            ChangeWeapon((WeaponType)weaponTypes[prevIndex]);
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
        animator.SetBool(weaponType.GetWeaponName(), true);
    }
}
