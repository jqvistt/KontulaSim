using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoofScript : MonoBehaviour
{
    private bool covered = false;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            covered = true;

        }

    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            covered = false;

        }

    }

    private void Update()
    {
        if (covered)
        {
            gameObject.GetComponent<Tilemap>().color = new Color(1f, 1f, 1f, 0.3f);
        }
        else
        {
            gameObject.GetComponent<Tilemap>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

}

