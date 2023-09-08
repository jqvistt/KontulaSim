using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer parentSpriteRenderer;

    private void Start()
    {
        // Get the SpriteRenderer component on the current GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the SpriteRenderer component on the parent GameObject
        parentSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on this GameObject.");
            enabled = false; // Disable the script if there is no SpriteRenderer.
        }

        if (parentSpriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on the parent GameObject.");
            enabled = false; // Disable the script if there is no SpriteRenderer on the parent.
        }
    }

    private void LateUpdate()
    {
        // Sync the sprite of the current GameObject with the parent's sprite
        spriteRenderer.sprite = parentSpriteRenderer.sprite;
    }
}
