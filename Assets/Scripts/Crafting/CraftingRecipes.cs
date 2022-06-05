using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CraftingRecepieType
{
    AxeRecipe,
    PickaxeRecipe
}
public class CraftingRecipes: MonoBehaviour
{
    public static CraftingRecipes instance = null;


    private List<Recipe> recipes = new List<Recipe>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        CreateRecipes();
    }

    public void CreateRecipes()
    {
        recipes.Add(new Recipe(AxeIngredientsList(), CraftingRecepieType.AxeRecipe,new Item(ItemType.Axe, 1)));
        recipes.Add(new Recipe(PickaxeIngredientsList(), CraftingRecepieType.PickaxeRecipe, new Item(ItemType.Pickaxe, 1)));

    }

    private List<Item> AxeIngredientsList()
    {
        List<Item> ingredients = new List<Item>();
        ingredients.Add(new Item(ItemType.Wood, 3));
        ingredients.Add(new Item(ItemType.Stone, 2));
        return ingredients;
    }

    private List<Item> PickaxeIngredientsList()
    {
        List<Item> ingredients = new List<Item>();
        ingredients.Add(new Item(ItemType.Wood, 2));
        ingredients.Add(new Item(ItemType.Stone, 5));
        return ingredients;
    }

    public List<Item> GetRecipie(CraftingRecepieType recipieType)
    {
        switch (recipieType)
        {
            case CraftingRecepieType.AxeRecipe:
                return GetRecipeByType(CraftingRecepieType.AxeRecipe).GetIngredients();
            default:
                return null;
        }
    }

    public List<Recipe> GetAllRecipes()
    {
        return recipes;
    }
    public Item GetCraftedItem(CraftingRecepieType recepieType)
    {
        return GetRecipeByType(recepieType).GetOutputItem();
    }
    private Recipe GetRecipeByType(CraftingRecepieType type)
    {
        foreach (Recipe item in recipes)
        {
            if (item.IsRecipeType(type))
            {
                return item;
            }
        }
        return null;
    }
}
