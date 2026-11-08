using System.Collections.Generic;
using UnityEngine;

// manages the weapons in the game including switching between them, and handling their data
public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Prefabs")]
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private GameObject pistolPrefab;
    [SerializeField] private GameObject riflePrefab;
    [SerializeField] private GameObject shotgunPrefab;
    [SerializeField] private GameObject grenadePrefab;
    [SerializeField] private GameObject rpgLauncherPrefab;

    [Header("Weapon Projectile Prefabs")]
    [SerializeField] private GameObject pistolProjectilePrefab;
    [SerializeField] private GameObject rifleProjectilePrefab;
    [SerializeField] private GameObject shotgunProjectilePrefab;
    [SerializeField] private GameObject rpgProjectilePrefab;



    private Dictionary<WeaponType, int> ammoCounts = new Dictionary<WeaponType, int>();

    public static WeaponManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // Initialize ammo counts for each weapon type
        ammoCounts[WeaponType.punch] = 0; // Punch has no ammo
        ammoCounts[WeaponType.knife] = 0; // Knife has no ammo
        ammoCounts[WeaponType.Pistol] = 15; // Example initial ammo count for Pistol
        ammoCounts[WeaponType.Rifle] = 30; // Example initial ammo count for Rifle
        ammoCounts[WeaponType.Shotgun] = 8; // Example initial ammo count for Shotgun
        ammoCounts[WeaponType.Grenade] = 5; // Example initial ammo count for Grenade
        ammoCounts[WeaponType.RPG] = 3; // Example initial ammo count for RPG
    }

    public GameObject GetWeaponPrefab(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.punch:
                return null; // Punch doesn't have a prefab
            case WeaponType.knife:
                return knifePrefab;
            case WeaponType.Pistol:
                return pistolPrefab;
            case WeaponType.Rifle:
                return riflePrefab;
            case WeaponType.Shotgun:
                return shotgunPrefab;
            case WeaponType.Grenade:
                return grenadePrefab;
            case WeaponType.RPG:
                return rpgLauncherPrefab;
            default:
                return null;
        }
    }
}
