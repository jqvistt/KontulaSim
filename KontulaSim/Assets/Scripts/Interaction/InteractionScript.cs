using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour {
public bool inReach = false; // Bool for checking whether player is in reach to interact
public bool looted = false; //Bool for checking whether object is looted or not
public AudioSource audioSource;
public AudioClip lootSound;
public AudioClip emptySound;
public Sprite activeSprite; //Activated sprite
public Sprite defSprite; //Unactivated sprite
public float cooldown = 10f; // Determines interaction cooldown

public GameObject Player;
public GameObject E_Popup;

    void start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
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
            this.GetComponent<SpriteRenderer>().sprite = defSprite;
            StartCoroutine(LootCooldown()); // Starts looting cooldown
        }

        }

        //Interaction cooldown script
        IEnumerator LootCooldown()
    {
        yield return new WaitForSeconds(cooldown);
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
