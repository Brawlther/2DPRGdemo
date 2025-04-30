using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrefabsManager : MonoSingleton<PrefabsManager>
{
    private Dictionary<string, GameObject> prefabDic;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private GameObject dashEffectPrefab;
    [SerializeField] private GameObject explosionEffectPrefab;

    protected override void Awake()
    {
        base.Awake();
        prefabDic = new Dictionary<string, GameObject>
        {
            { "fireball", fireballPrefab },
            { "dasheffect", dashEffectPrefab },
            { "explosioneffect", explosionEffectPrefab }
        };
    }

    public GameObject GetPrefabByName(string prefabName){
        return prefabDic[prefabName];
    }
}
