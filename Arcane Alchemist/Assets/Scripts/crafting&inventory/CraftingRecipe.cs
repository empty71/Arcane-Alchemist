using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CraftingRecipe/BaseRecipe")]
public class CraftingRecipe : Item
{
    public Item result;
    public Ingredient[] ingredients;

    private bool CanCraft()
    {
        //ask inv obj, if there is enough resources
        foreach (Ingredient ingredient in ingredients)
        {
            bool containsCurrentIngredient = Inventory.instance.ContainsItem(ingredient.item, ingredient.amount);
           
            if (!containsCurrentIngredient)
            {
                return false;
            }
        }
        return true;
    }

    private void RemoveIngredientsFromInventory()
    {
        foreach (Ingredient ingredient in ingredients)
        {
            Inventory.instance.RemoveItems(ingredient.item, ingredient.amount);
        }
    }
    
    public override void Use()
    {
        if (CanCraft())
        {
            //remove items
            RemoveIngredientsFromInventory();

            //add item to inv
            Inventory.instance.AddItem(result);
            Debug.Log("u crafted: " + result.name);
        }
        else
        {
            Debug.Log("not enough ingriedents to craft: " + result.name);
        }
    }

    [System.Serializable]
    public class Ingredient 
    {
        public Item item;
        public int amount;
    }

}
