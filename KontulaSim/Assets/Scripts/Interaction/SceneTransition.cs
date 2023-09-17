using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    private bool inReach = false;

    public Vector2 spawnPos;
    public GameObject Player;

    private void Start()
    {
         
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inReach= true;
            Debug.Log("Player in reach!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inReach = false;
            Debug.Log("Player out of reach!");
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            Debug.Log("RELOADING SCENE!");
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        Player = GameObject.FindGameObjectWithTag("Player");

        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneToLoad);
            Player.transform.position = spawnPos;
        }

    }


}

