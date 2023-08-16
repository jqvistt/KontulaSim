using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    void Start()
    {
        transform.position = SpawnPoint.instance.GetSpawnPosition();
    }

}
