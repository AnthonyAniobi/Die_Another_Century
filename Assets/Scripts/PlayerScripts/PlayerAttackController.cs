using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private Animator animator;
    public WeaponType currentWeaponType;
    void Start()
    {
        animator = GetComponent<Animator>();
        ChangeWeapon(currentWeaponType);
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
