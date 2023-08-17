using UnityEngine;

public class DialogueUIManager : MonoBehaviour
{
    private static DialogueUIManager instance;

    public static DialogueUIManager Instance
    {
        get { return instance; }
    }

    public GameObject dialogueUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject alive between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
}