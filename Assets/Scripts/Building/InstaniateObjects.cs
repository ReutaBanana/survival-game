using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InstaniateObjects : MonoBehaviour
{
    public PlayerInventory _playerInventory;

    [SerializeField] private Camera mainCamera;

    private GameObject objectWaitingPrefab;

    private bool hasInstaniate;

    public void Build(BuildingRecipeType type)
    {
        List<Item> recipe = BuildingRecipes.instance.GetRecipie(type);

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
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(BuildingAssets.instance.getGameobject(type), hit.point, Quaternion.identity);
            }
        }
      
    }
    public void CreateWaitingObject(BuildingRecipeType type)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(!hasInstaniate)
            {
                objectWaitingPrefab = Instantiate(BuildingAssets.instance.getWaitingGameobject(type), hit.point, Quaternion.identity);
                hasInstaniate = true;
            }
            else
            {
                objectWaitingPrefab.transform.position = hit.point;

            }

        }
    }
    private bool ValidateRecipieItem(Item recipeItem)
    {
        return (_playerInventory.GetInventory().CheckIfExsist(recipeItem));
    }

    public void ClearBuilding()
    {
        Destroy(objectWaitingPrefab);
        hasInstaniate = false;

    }
}
