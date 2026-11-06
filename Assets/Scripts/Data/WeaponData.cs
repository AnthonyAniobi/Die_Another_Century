using UnityEngine;

public class WeaponData
{
    

    

    public WeaponType type;
    public int totalAmmo;


    public WeaponData(WeaponType type, int totalAmmo)
    {
        this.type = type;
        this.totalAmmo = totalAmmo;
    }
}
public enum WeaponType
{
    dagger,
    Pistol,
    Rifle,
    Shotgun,
    Sniper,
    SMG
}