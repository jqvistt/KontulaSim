using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot System/Loot Item")]
public class Loot : ScriptableObject
{

    public GameObject dropPrefab;
    public int dropChance;
    public string lootName;

    public Loot(GameObject dropPrefab, int dropChance, string lootName)
    {
        this.dropPrefab = dropPrefab;
        this.dropChance = dropChance;
        this.lootName = lootName;
    }
}
