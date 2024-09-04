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
        if (collision.gameObject.CompareTag("player1"))
        {
            inventory.inventoryItemList.Add(item);
            inventory.onItemChange.Invoke();
            Debug.Log("item picked up");
            Destroy(gameObject);
        }
    }

    public StatItem GetItem()
    {
        return item;
    }
}

