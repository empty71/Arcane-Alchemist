using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject); // Ensures there's only one instance of GameManager
    }
    #endregion

    public List<Item> itemList = new List<Item>();
    public List<Item> craftingRecipes = new List<Item>();

    public Transform canvas;
    public GameObject itemInfoPrefab;
    private GameObject currentItemInfo = null;

    public Transform weaponItemTransform;
    public Inventory iv;

    public float moveX = 180f;
    public float moveY = 100f;

    public bool holdingItem;

    [Header("offset items")]
    public float offsetX;
    public float offsety;
    public float offsetz;

   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Inventory.instance.AddItem(itemList[Random.Range(0, itemList.Count)]);
        }
    }



    public void DisplayItemInfo(string itemName, string itemDescription, Vector2 buttonPos)
    {

        if (currentItemInfo != null)
        {
            Destroy(currentItemInfo.gameObject);
        }

        buttonPos.x -= moveX;
        buttonPos.y += moveY;

        currentItemInfo = Instantiate(itemInfoPrefab, buttonPos, Quaternion.identity, canvas);
        currentItemInfo.GetComponent<ItemInfo>().SetUp(itemName, itemDescription);
    }

    public void DestroyItemInfo()
    {
        if (currentItemInfo != null)
        {
            Destroy(currentItemInfo.gameObject);
        }
    }

    public void InstantiateItemByID(int id)
    {
        StatItem itemToInstantiate = null;

        // Check itemList for the item with the given ID
        foreach (Item item in iv.inventoryItemList)
        {
            StatItem statItem = item as StatItem;
            if (statItem != null && statItem.ID == id)
            {
                itemToInstantiate = statItem;
                break;
            }
        }

        // Check craftingRecipes for the item with the given ID if not found in itemList
        if (itemToInstantiate == null)
        {
            foreach (Item item in craftingRecipes)
            {
                StatItem statItem = item as StatItem;
                if (statItem != null && statItem.ID == id)
                {
                    itemToInstantiate = statItem;
                    break;
                }
            }
        }

        // Instantiate the item if found
        if (itemToInstantiate != null)
        {
            // Check if there's already an item under weaponItemTransform
            if (weaponItemTransform.childCount > 0)
            {
                Transform oldItemTransform = weaponItemTransform.GetChild(0);

                // Retrieve the old item's StatItem before destroying it
                StatItem oldStatItem = oldItemTransform.GetComponent<PickUp>()?.GetItem();

                // Add the old item back to the inventory if it exists
                if (oldStatItem != null)
                {
                    iv.AddItem(oldStatItem);
                    iv.onItemChange.Invoke();
                }

                // Destroy the old item
                Destroy(oldItemTransform.gameObject);
            }

            // Instantiate the new item
            GameObject newItem = Instantiate(itemToInstantiate.statItemPrefab, weaponItemTransform);

            // Check if the new item has a PickUp component
            PickUp pickUpComponent = newItem.GetComponent<PickUp>();
            if (pickUpComponent == null)
            {
                // Add the PickUp component if it doesn't exist
                pickUpComponent = newItem.AddComponent<PickUp>();
            }

            // Assign the StatItem to the PickUp component
            pickUpComponent.item = itemToInstantiate;

            holdingItem = true;

            Debug.Log("Instantiated item with ID: " + id);
        }
        else
        {
            Debug.LogWarning("Item with ID " + id + " not found in itemList or craftingRecipes.");
        }
    }



}



