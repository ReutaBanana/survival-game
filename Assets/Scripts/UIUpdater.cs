using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIUpdater : MonoBehaviour
{

    [SerializeField] private PlayerStatus _playerStatus;
    [SerializeField] private Slider hungerUI;

    [Header("Interactions")]
    [SerializeField] private GameObject _treeInteractionPopup;
    // Start is called before the first frame update
    void Start()
    {
        _playerStatus.onStatusChange += ChangeUI;
    }

    private void ChangeUI(int amount)
    {
        hungerUI.value = amount;
    }

    public void InteractionScreenPopup(interactionType type, bool activeInteraction)
    {
        switch (type)
        {
            case interactionType.Tree:
                if (activeInteraction)
                    _treeInteractionPopup.SetActive(true);
                else
                    _treeInteractionPopup.SetActive(false);
                break;
            case interactionType.Rock:
                break;
            default:
                break;
        }
    }
}
