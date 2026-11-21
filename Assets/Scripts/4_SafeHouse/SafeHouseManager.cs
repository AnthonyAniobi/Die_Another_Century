using System.Collections.Generic;
using UnityEngine;

public class SafeHouseManager : MonoBehaviour
{
    [SerializeField] private GameObject popupUI;
    bool isPlayerInSafeHouse = false;

    

    void Update()
    {
        if (isPlayerInSafeHouse)
        {
            
        }
    }

    void OnTrigger(Collider other)
    {
        isPlayerInSafeHouse = other.CompareTag("Player");
        if (other.CompareTag("Player"))
        {
            popupUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        isPlayerInSafeHouse = false;
        if (other.CompareTag("Player"))
        {
            popupUI.SetActive(false);
        }
    }
}
