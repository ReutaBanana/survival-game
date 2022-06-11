using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingObjects : MonoBehaviour
{
    private PlayerInventory playerInventory;
    [SerializeField] GameObject axeWorld;
    private GameObject instaniate;
    private bool hasInstanated = false;
    private Item currentTool;
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasInstanated)
        {
            foreach (Item tool in playerInventory.GetInventory().GetTools())
            {
                instaniate = Instantiate(GetGameObjectByType(tool.type));

                instaniate.transform.parent = GameObject.Find("Hand_R").GetComponent<Transform>();
                instaniate.transform.localPosition = (new Vector3(18.3f, 0, -14.5f));
                instaniate.transform.localEulerAngles =(new Vector3(-125.92f, -95.33398f, 64.375f));
                instaniate.transform.localScale = (new Vector3(100, 50, 100));
                hasInstanated = true;
                currentTool = tool;
                break;
            }
        }

        List<Item> inventoryTools = playerInventory.GetInventory().GetTools();
        if(!inventoryTools.Contains(currentTool))
        {
            Destroy(instaniate);
            hasInstanated = false;
        }
       
    }
    private GameObject GetGameObjectByType(ItemType type)
    {
        switch (type)
        {
            default:
                return null;
            case ItemType.Axe:
                return axeWorld;
            case ItemType.Pickaxe:
                return axeWorld;
        }
    }
}
