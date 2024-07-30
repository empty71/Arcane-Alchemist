using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    private Item_oldCrafting CurrentItem;
    public Image customCursor;

    public Slot_oldCrafting[] craftingSlots;

    public List<Item_oldCrafting> itemList;
    public string[] recipes;
    public Item_oldCrafting[] recipeResults;
    public Slot_oldCrafting resultSlot;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (CurrentItem != null)
            {
                customCursor.gameObject.SetActive(false);
                Slot_oldCrafting nearestSlot = null;
                float shortestDistance = float.MaxValue;

                foreach (Slot_oldCrafting slot in craftingSlots)
                {
                    float dist = Vector2.Distance(Input.mousePosition, slot.transform.position);

                    if (dist < shortestDistance)
                    {
                        shortestDistance = dist;
                        nearestSlot = slot;
                    }
                }
                nearestSlot.gameObject.SetActive(true);
                nearestSlot.GetComponent<Image>().sprite = CurrentItem.GetComponent<Image>().sprite;
                nearestSlot.item = CurrentItem;
                itemList[nearestSlot.index] = CurrentItem;

                CurrentItem = null;

                CheckForCreatedRecipes();

            }
        }
    }

    void CheckForCreatedRecipes()
    {
        resultSlot.gameObject.SetActive(false);
        resultSlot.item = null;

        string currentRecipeString = "";
        foreach (Item_oldCrafting item in itemList)
        {
            if(item != null)
            {
                currentRecipeString += item.itemName;
            }
            else
            {
                currentRecipeString += "null";
            }
        }
        for(int i = 0; i<recipes.Length; i++)
        {
            if(recipes[i] == currentRecipeString)
            {
                resultSlot.gameObject.SetActive(true);
                resultSlot.GetComponent<Image>().sprite = recipeResults[i].GetComponent<Image>().sprite;
                resultSlot.item = recipeResults[i];
            }
        }
    }

    public void onClickSlot(Slot_oldCrafting slot)
    {
        slot.item = null;
        itemList[slot.index] = null;
        slot.gameObject.SetActive(false);
        CheckForCreatedRecipes();
    }
    public void OnmouseDownItem(Item_oldCrafting item)
    {
        if(CurrentItem == null)
        {
            CurrentItem = item;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = CurrentItem.GetComponent<Image>().sprite;
        }
    
    }
}
