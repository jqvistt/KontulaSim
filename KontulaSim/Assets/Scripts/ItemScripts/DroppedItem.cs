using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip hitGround;
    private SpriteRenderer sprite;
    public int inAir;
    public int grounded;

    public Rigidbody2D rb;
    void Start()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        StartCoroutine(FreezeDrop());   
    }

    IEnumerator FreezeDrop()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sortingOrder = inAir;
        yield return new WaitForSeconds(0.6f);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        audioSource.PlayOneShot(hitGround, 1);
        sprite.sortingOrder = grounded;
    }

}


