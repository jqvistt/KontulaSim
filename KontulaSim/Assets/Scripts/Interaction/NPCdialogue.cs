using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCdialogue : MonoBehaviour
{
    public GameObject dialogueUI; // reference to the dialogue UI object
    public GameObject interactPopupSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartDialogue();
            ShowInteractPopup();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
                dialogueUI.SetActive(true); // show the dialogue UI
                HideInteractPopup();
                break;
            }
            yield return null;
        }
    }

    public void EndDialogue()
    {
        dialogueUI.SetActive(false); // hide the dialogue UI
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
