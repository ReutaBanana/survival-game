using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InstaniateObjects : MonoBehaviour
{
    [SerializeField]private GameObject testObject;
    [SerializeField]private GameObject testObjectWaiting;
    [SerializeField] private Camera mainCamera;

    private GameObject objectWaitingPrefab;

    private bool hasInstaniate;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Build()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            Instantiate(testObject, hit.point, Quaternion.identity);
        }
    }
    public void CreateWaitingObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(!hasInstaniate)
            {
                objectWaitingPrefab = Instantiate(testObjectWaiting, hit.point, Quaternion.identity);
                hasInstaniate = true;
            }
            else
            {
                objectWaitingPrefab.transform.position = hit.point;

            }

        }
    }
}
