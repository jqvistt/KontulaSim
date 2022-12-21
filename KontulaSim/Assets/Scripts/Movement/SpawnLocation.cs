using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnLocation : MonoBehaviour
{

    public string PreviousSceneName;
    public Vector3 SpawnPoint;
    private void Start()
    {
        SetSpawnLocation();
    }

    private void SetSpawnLocation()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if(currentSceneName == PreviousSceneName) 
        { 
            transform.position = SpawnPoint;
        }
    }
}
