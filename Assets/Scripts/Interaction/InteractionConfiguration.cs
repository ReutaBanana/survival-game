using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionConfiguration : MonoBehaviour
{
    public static InteractionConfiguration instance = null;

    [Header("Prefabs")]
    public GameObject woodPrefab;
    public GameObject stonePrefab;
    public GameObject firePrefab;



    [Header("HitConfiguration")]
    public int woodHitCount;
    public int stoneHitCount;

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
