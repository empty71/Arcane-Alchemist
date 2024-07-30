using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion
    public List<Item> itemList = new List<Item>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Inventory.instance.AddItem(itemList[Random.Range(0, itemList.Count)]);
        }
    }

    public void OnStatItemUse(StatItemType itemType,int amount)
    {
        Debug.Log("consuming: " + itemType + "add amount: " + amount);
    }
}
