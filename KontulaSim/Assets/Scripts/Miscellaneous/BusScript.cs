using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusScript : MonoBehaviour
{

    public float speed = 10.0f;
    public float timeUntilStop = 3f;
    public float timeUntilStart = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
        StartCoroutine(StopBus());
    }

    private IEnumerator StopBus()
    {
        yield return new WaitForSeconds(timeUntilStop);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(timeUntilStart);
        rb.velocity = new Vector2(speed, 0);
    }

}
