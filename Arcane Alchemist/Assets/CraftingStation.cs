using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingStation : MonoBehaviour
{
    [SerializeField] private bool playerHasEntered;
    [SerializeField] public bool craftingActivated;
    public InventoryUi inventoryUi;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerHasEntered = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerHasEntered = true;
        }
        if (playerHasEntered == true&& Input.GetKeyDown(KeyCode.E)&&craftingActivated== false&&inventoryUi.InventoryOpen==false)
        {
            craftingActivated = true;
            inventoryUi.inventoryParent.SetActive(true);
            inventoryUi.craftingTab.SetActive(true);
        }
        else if(playerHasEntered == true && Input.GetKeyDown(KeyCode.E) && craftingActivated == true)
        {
            craftingActivated = false;
            inventoryUi.inventoryParent.SetActive(false);
            inventoryUi.craftingTab.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerHasEntered = false;
            inventoryUi.inventoryParent.SetActive(false);
            inventoryUi.inventoryTab.SetActive(false);
            inventoryUi.craftingTab.SetActive(false);
        }
    }
}
