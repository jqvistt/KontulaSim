using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip hitGround;

    public Rigidbody2D rb;
    void Start()
    {
    rb.constraints = RigidbodyConstraints2D.None;
    StartCoroutine(FreezeDrop());
    }

    IEnumerator FreezeDrop()
    {
        yield return new WaitForSeconds(0.5f);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        audioSource.PlayOneShot(hitGround, 1);
    }

}


