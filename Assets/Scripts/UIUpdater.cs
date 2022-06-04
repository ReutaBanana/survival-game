using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UIUpdater : MonoBehaviour
{

    [SerializeField] private PlayerStatus _playerStatus;
    [SerializeField] private Slider hungerUI;

    [Header("Interactions")]
    [SerializeField] private GameObject _treeInteractionPopup;
    [SerializeField] private GameObject _stoneCollectInteractionPopup;
    [SerializeField] private GameObject _stoneDigInteractionPopup;

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
            case interactionType.ChopTree:
                TogglePopup(activeInteraction,_treeInteractionPopup);
                break;
            case interactionType.StoneCollect:
                TogglePopup(activeInteraction, _stoneCollectInteractionPopup);
                break;
            case interactionType.StoneDig:
                TogglePopup(activeInteraction, _stoneDigInteractionPopup);
                break;
            default:
                break;
        }
    }

    private void TogglePopup(bool activeInteraction,GameObject interactionType)
    {
        if (activeInteraction)
            interactionType.SetActive(true);

        else
            interactionType.SetActive(false);
    }
}
