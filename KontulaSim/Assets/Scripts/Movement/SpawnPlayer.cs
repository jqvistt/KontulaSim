using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public Vector2 SpawnPoint;
    
    void Awake()
    {
        transform.position = SpawnPoint;
    }

}
