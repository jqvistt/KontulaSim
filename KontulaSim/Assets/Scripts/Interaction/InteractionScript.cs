using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour {

private bool inReach = false; // Bool for checking whether player is in reach to interact
private bool looted = false; //Bool for checking whether object is looted or not

public AudioSource audioSource;// 
public AudioClip lootSound;// Sound to be played when a player loots the container

public Sprite activeSprite; //Activated sprite, Shows whether the player is in reach or not.
public Sprite defSprite; //Unactivated sprite, the default sprite for the gameObject.
public Sprite lootedSprite; // Looted sprite

public float cooldown = 10f; // Determines interaction cooldown, can be set in the inspector

public GameObject Player;

    void start()
    {
        audioSource = gameObject.GetComponent<AudioSource>(); //Gets the audio source of the object for later use.
    }

    void Update () {

        if(looted == false)
        {

        if(inReach){

            this.GetComponent<SpriteRenderer>().sprite = activeSprite;   

        }
        else{

            this.GetComponent<SpriteRenderer>().sprite = defSprite;

        }

        if(inReach && Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<LootContainer>().InstantiateLoot(transform.position);
            audioSource.PlayOneShot(lootSound, 1);

            looted = true;
            this.GetComponent<SpriteRenderer>().sprite = lootedSprite;
            StartCoroutine(LootCooldown()); // Starts looting cooldown
        }

        }

        //Interaction cooldown script
        IEnumerator LootCooldown()
    {
        yield return new WaitForSeconds(cooldown);
            GetComponent<SpriteRenderer>().sprite = defSprite;
        looted = false;
    }

    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Player"){

            inReach = true;

        }

    }

    void OnTriggerExit2D(Collider2D other) {

        if (other.tag == "Player") {

            inReach = false;

        }

    }

}
