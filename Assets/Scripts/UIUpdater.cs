using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIUpdater : MonoBehaviour
{

    [SerializeField] private PlayerStatus _playerStatus;
    [SerializeField] private Slider hungerUI;
    // Start is called before the first frame update
    void Start()
    {
        _playerStatus.onStatusChange += ChangeUI;
    }

    private void ChangeUI(int amount)
    {
        hungerUI.value = amount;
    }

 
}
