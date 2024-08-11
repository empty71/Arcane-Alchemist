using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region singleton

    public static Inventory instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance
        }
    }

    #endregion

    public delegate void OnItemChange();
    public OnItemChange onItemChange = delegate { };

    public List<Item> inventoryItemList = new List<Item>();

    public void AddItem(Item item)
    {
        inventoryItemList.Add(item);
        Debug.Log("Added item: " + item.name + " to inventory.");
        onItemChange.Invoke();
    }

    public void RemoveItem(Item item)
    {
        inventoryItemList.Remove(item);
        Debug.Log("Removed item: " + item.name + " from inventory.");
        onItemChange.Invoke();
    }

    public bool ContainsItem(Item item, int amount)
    {
        int itemCounter = 0;

        foreach (Item i in inventoryItemList)
        {
            if (i == item)
            {
                itemCounter++;
            }
        }

        return itemCounter >= amount;
    }

    public void RemoveItems(Item item, int amount)
    {
        for (int i = 0; i < amount; ++i)
        {
            RemoveItem(item);
        }
    }
}