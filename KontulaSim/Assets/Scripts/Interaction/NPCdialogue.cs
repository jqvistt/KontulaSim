using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCdialogue : MonoBehaviour
{
    public GameObject dialogueUI;
    public GameObject interactPopupSprite;

    public bool inReach = false;

    private void Start()
    {
        inReach = false;
        interactPopupSprite.SetActive(false);

        dialogueUI = DialogueUIManager.Instance.dialogueUI;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inReach = true;
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
        if (!inReach)
        {
            interactPopupSprite.SetActive(false);
        } else if (inReach)
        {
            interactPopupSprite.SetActive(true);
        }

        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
        else if (!inReach)
        {
            EndDialogue();
            HideInteractPopup();
        }
    }

    public void ShowInteractPopup()
    {
        interactPopupSprite.SetActive(true); // show the interact popup sprite
    }

    public void HideInteractPopup()
    {
        interactPopupSprite.SetActive(false); // hide the interact popup sprite
    }

    public void StartDialogue()
    {
        StartCoroutine(DialogueCoroutine());
    }

    private IEnumerator DialogueCoroutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueUI.SetActive(true);
                HideInteractPopup();
                yield break; // End the coroutine
            }
            yield return null;
        }
    }

    public void EndDialogue()
    {
        if (dialogueUI != null)
        {
            dialogueUI.SetActive(false); // hide the dialogue UI
        }
    }

    public void UpdateDialogue(int responseIndex)
    {

        switch (responseIndex)
        {
            case 0:
                //handle response 1
                break;
            case 1:
                //handle response 2
                break;
        }

    }

}
