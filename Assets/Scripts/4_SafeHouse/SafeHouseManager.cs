using System.Collections.Generic;
using UnityEngine;

public class SafeHouseManager : MonoBehaviour
{
    // safe house manager controls the safe house where player can rest
    // and upgrade their abilities. It is responsible for managing the 
    // safe house environment, player interactions, and any relevant game mechanics related to the safe house.

    
    void Start()
    {
        // Initialize(transform.position);
    }

    void Initialize(
        Vector3 position,
        Vector3 rotation,
        Dictionary<WeaponType, int> weaponInventory
    )
    {
        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
