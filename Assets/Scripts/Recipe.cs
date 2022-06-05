using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe 
{
    List<Item> ingredients;
    BuildingRecipeType buildingType;
    CraftingRecepieType craftingType;
    Item recipeOutput;

    public Recipe(List<Item> ingredients, CraftingRecepieType craftingType, Item output)
    {
        this.ingredients = ingredients;
        this.craftingType = craftingType;
        recipeOutput = output;

    }

    public Recipe(List<Item> ingredients, BuildingRecipeType buildingType, Item output)
    {
        this.ingredients = ingredients;
        this.buildingType = buildingType;
        recipeOutput = output;

    }

    public List<Item> GetIngredients ()
    {
        return ingredients;
    }

    public BuildingRecipeType GetBuildingRecipeType()
    {
        return buildingType;
    }
    public CraftingRecepieType GetCraftingRecipeType()
    {
        return craftingType;
    }

    public bool IsRecipeType(BuildingRecipeType type)
    {
        return type == buildingType;
    }

    public bool IsRecipeType(CraftingRecepieType type)
    {
        return type == craftingType;
    }
    public Item GetOutputItem()
    {
        return recipeOutput;
    }
}
