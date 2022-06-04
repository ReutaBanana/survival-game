using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingAction : MonoBehaviour
{
    public PlayerInventory _playerInventory;
    private CraftingRecipes craftingRecipes;
    // Start is called before the first frame update
    private void Awake()
    {
        craftingRecipes = CraftingRecipes.instance;
        craftingRecipes = this.GetComponent<CraftingRecipes>();
    }
    public void Craft(CraftingRecepieType recipieType)
    {

        List<Item> recipe = craftingRecipes.GetRecipie(recipieType);
        List<bool> validateResult = new List<bool>();

        foreach (Item recipeItem in recipe)
        {
            validateResult.Add(ValidateRecipieItem(recipeItem));
        }

        if (validateResult.Contains(false))
        {
            Debug.Log("not enough");
        }
        else
        {
            _playerInventory.GetInventory().RemoveItems(recipe);
            _playerInventory.GetInventory().AddItem(craftingRecipes.GetCraftedItem(recipieType));
            Debug.Log("Crafted" + recipieType.ToString());
        }

            
    }

    private bool ValidateRecipieItem(Item recipeItem)
    {
        return (_playerInventory.GetInventory().CheckIfExsist(recipeItem)) ;
    }
}
