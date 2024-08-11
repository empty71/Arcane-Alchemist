using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//public class GameManager : MonoBehaviour
//{
//    #region singleton
//    public static GameManager instance;
//    private void Awake()
//    {
//        if (instance == null)
//            instance = this;
//    }
//    #endregion

//    public List<Item> itemList = new List<Item>();
//    public List<Item> craftingRecipes = new List<Item>();

//    public Transform canvas;
//    public GameObject itemInfoPrefab;
//    private GameObject currentItemInfo = null;
//    public StatItem St;


//    public Transform weaponItemTransform;


//    public float moveX = 180f;
//    public float moveY = 100f;

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.X))
//        {
//            Inventory.instance.AddItem(itemList[Random.Range(0, itemList.Count)]);
//        }
//    }

//    public void OnStatItemUse(StatItemType itemType, int amount)
//    {
//        Debug.Log("Consuming " + itemType + " Add amount: " + amount);
//        if (itemType == StatItemType.WeaponItem)
//        {

//            Instantiate(St.statItemPrefab, weaponItemTransform);
//        }
//    }

//    public void DisplayItemInfo(string itemName, string itemDescription, Vector2 buttonPos)
//    {
//        if (currentItemInfo != null)
//        {
//            Destroy(currentItemInfo.gameObject);
//        }

//        buttonPos.x -= moveX;
//        buttonPos.y += moveY;

//        currentItemInfo = Instantiate(itemInfoPrefab, buttonPos, Quaternion.identity, canvas);
//        currentItemInfo.GetComponent<ItemInfo>().SetUp(itemName, itemDescription);
//    }

//    public void DestroyItemInfo()
//    {
//        if (currentItemInfo != null)
//        {
//            Destroy(currentItemInfo.gameObject);
//        }
//    }

//}
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

    public float moveX = 180f;
    public float moveY = 100f;

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
        foreach (Item item in itemList)
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
            Instantiate(itemToInstantiate.statItemPrefab, weaponItemTransform);
            Debug.Log("Instantiated item with ID: " + id);
        }
        else
        {
            Debug.LogWarning("Item with ID " + id + " not found in itemList or craftingRecipes.");
        }
    }
}
