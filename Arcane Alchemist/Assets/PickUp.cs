using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public StatItem item;

    public Inventory inventory;
    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        
        inventory.inventoryItemList.Add(item);
        Debug.Log("item picked up");
        Destroy(gameObject);
    }
}
