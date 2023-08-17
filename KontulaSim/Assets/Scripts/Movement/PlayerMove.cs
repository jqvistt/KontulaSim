using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : MonoBehaviour
{

    [SerializeField] private int speed = 2;
    [SerializeField] private float dashSpeed = 20000;
    [SerializeField] private float dashDuration = 0.5f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    public AudioClip stepSound;
    private AudioSource audioSource;
    public bool shiftPressed = false;
    private bool isDashing = false;
    private float dashTimer = 0;

    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    [SerializeField] private bool controlsDisabled = false;


    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    private void OnMovement(InputValue value)
    {
        if (controlsDisabled) return;

        movement = value.Get<Vector2>();

        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);

            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            controlsDisabled = true;
            animator.SetTrigger("isInjecting");
        }

        /*if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Inject"))
        {
            controlsDisabled = false;
        }*/

        if (Input.GetKey(KeyCode.LeftShift))
        {
            shiftPressed = true;
        }
        else
        {
            shiftPressed = false;
        }

        if (shiftPressed)
        {
            speed = 3;
            animator.speed = 1.5f;
        }
        else
        {
            speed = 2;
            animator.speed = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            isDashing = true;
            dashTimer = dashDuration;
        }

        if (isDashing)
        {
            rb.velocity = movement.normalized * dashSpeed;
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
            {
                isDashing = false;
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        if (controlsDisabled) return;

        if (!isDashing)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }

    }

    private void PlayStepSound()
    {
        float randomPitch = Random.Range(minPitch, maxPitch);
        audioSource.pitch = randomPitch;
        audioSource.PlayOneShot(stepSound);
    }
}
