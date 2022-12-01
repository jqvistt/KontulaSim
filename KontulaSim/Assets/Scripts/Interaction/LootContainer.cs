using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class LootContainer : MonoBehaviour
{
    public List<Loot> lootList = new List<Loot>();


    Loot GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101); // 1-100
        List<Loot> possibleItems = new List<Loot>();
        foreach (Loot item in lootList)
        {
            if(randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }
        if(possibleItems.Count > 0)
        {
            Loot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
            
        }
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        


        Loot droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItem.dropPrefab, spawnPosition + Vector3.up * 1, Quaternion.identity);

            float dropForce = 2.5f;
            Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(0f, 0.5f));
            lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
        }
    }
}
