using System.Collections.Generic;
using UnityEngine;

public class SafeHouseManager : MonoBehaviour
{
    [SerializeField] private GameObject popupUI;
    bool isPlayerInSafeHouse = false;

    void Start()
    {
        popupUI.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInSafeHouse)
        {
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            popupUI.SetActive(true);
            other.GetComponent<PlayerAimController>().SetInSafeHouse(true);
            isPlayerInSafeHouse = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            popupUI.SetActive(false);
            isPlayerInSafeHouse = false;
            other.GetComponent<PlayerAimController>().SetInSafeHouse(false);
        }
    }
}
