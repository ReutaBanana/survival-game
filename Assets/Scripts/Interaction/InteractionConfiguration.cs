using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionConfiguration : MonoBehaviour
{
    public static InteractionConfiguration instance = null;

    [Header("Prefabs")]
    [SerializeField] public GameObject woodPrefab;


    [Header("HitConfiguration")]
    [SerializeField] public int woodHitCount;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if(instance!=this)
        { Destroy(gameObject);
        }
    }
    
}
