using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetainPlayerData : MonoBehaviour
{

    private static RetainPlayerData playerInstance;
    void Awake()
    {
        GameObject.DontDestroyOnLoad(this);

        if(playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            Object.Destroy(gameObject);
        }
    }
}
