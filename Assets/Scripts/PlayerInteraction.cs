using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;


    public class PlayerInteraction : MonoBehaviour
    {
        private bool isClicked;
        public event Action<bool> onInventoryClick;
        private StarterAssetsInputs _input;


        private void Awake()
        {
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
        {
            OpenInventory();
        }

        private void OpenInventory()
        {
            if (_input.openInventory)
            {
                if (!isClicked)
                {
                    isClicked = true;
                    onInventoryClick?.Invoke(true);
                   _input.openInventory = false;
                }
                else if (isClicked)
                {
                    isClicked = false;
                    onInventoryClick?.Invoke(false);
                    _input.openInventory = false;
                 }
 
             }

        }

   
}
