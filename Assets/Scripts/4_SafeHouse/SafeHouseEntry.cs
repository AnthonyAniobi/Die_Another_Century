using System.Collections.Generic;
using UnityEngine;

public class SafeHouseEntry : MonoBehaviour
{
    // safe house manager controls the safe house where player can rest
    // and upgrade their abilities. It is responsible for managing the 
    // safe house environment, player interactions, and any relevant game mechanics related to the safe house.

    private Animator doorAnimator;
    
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
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

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("SafeHouseManager: OnTriggerEnter called.");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the safe house.");
            doorAnimator.SetTrigger("OpenDoor");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("CloseDoor");
        }
    }
}
