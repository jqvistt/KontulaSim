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

    [SerializeField] private GameObject triggerArea;
    [SerializeField] private GameObject injectHover;
    [SerializeField] private GameObject playerHotbar;

    public Camera mainCam;

    public bool isCursorOverTrigger = false;

    public bool isConsuming = false;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    public AudioClip stepSound;
    public AudioClip needleAudio;
    public AudioClip blinkSound;
    public AudioClip tripSound;
    private AudioSource audioSource;

    public MouseItemData mouseItemData;

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

        //Debug.Log("Current Animation: " + GetCurrentAnimationName());

        if (mouseItemData != null)
        {
            string itemName = mouseItemData.GetAssignedItemName();
            if (!string.IsNullOrEmpty(itemName))
            {
                if (itemName == "needle_data")
                {
                    // Check if the cursor is over the trigger area
                    if (isCursorOverTrigger)
                    {
                        injectHover.SetActive(true);
                        mouseItemData.ItemSprite.enabled = false;
                        mouseItemData.ItemCount.enabled = false;
                    }
                    else
                    {
                        injectHover.SetActive(false);
                        mouseItemData.ItemSprite.enabled = true;
                        mouseItemData.ItemCount.enabled = true; 
                    }

                    // Check if the right mouse button is pressed
                    if (Input.GetMouseButtonDown(1) && isCursorOverTrigger || Input.GetMouseButtonDown(0) && isCursorOverTrigger)
                    {
                        UseItemFromMouseItemData();
                        controlsDisabled = true;
                        animator.SetTrigger("isInjecting");
                
                        injectHover.SetActive(false); // Disable the hover
                    }
                }
            }
        }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null && hit.collider.gameObject == triggerArea)
        {
            // Cursor is hovering over the trigger area
            isCursorOverTrigger = true;
        }
        else
        {
            // Cursor is not over the trigger area
            isCursorOverTrigger = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) && !controlsDisabled)
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

    private string GetCurrentAnimationName()
    {
        if (animator.GetCurrentAnimatorClipInfo(0).Length > 0)
        {
            return animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        }
        return "No Animation";
    }

    private void FixedUpdate()
    {
        if (controlsDisabled) return;

        if (!isDashing)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }

    }
    private void UseItemFromMouseItemData()
    {
        // Assuming that you have a way to reference the InventorySlot of the mouse
        InventorySlot mouseInventorySlot = mouseItemData.AssignedInventorySlot;

        if (mouseInventorySlot.ItemData != null)
        {
            if (mouseInventorySlot.StackSize > 0)
            {
                mouseInventorySlot.RemoveFromStack(1); // Decrease stack size

                if (mouseInventorySlot.StackSize <= 0)
                {
                    mouseItemData.ClearSlot(); // Clear the slot if stack size becomes zero or less
                }
            }
        }
    }

    private void PlayStepSound()
    {
        float randomPitch = Random.Range(minPitch, maxPitch);
        audioSource.pitch = randomPitch;
        audioSource.PlayOneShot(stepSound);
    }

    private void PlayNeedleSound()
    {
        audioSource.PlayOneShot(needleAudio);
        audioSource.pitch = 1;
    }

    private void PlayBlinkSound()
    {
        audioSource.PlayOneShot(blinkSound);
        audioSource.pitch = 1;
    }

    private void PlayTripSound()
    {
        audioSource.PlayOneShot(tripSound);
        audioSource.pitch = 1;
    }

    public void OnInjectAnimationStart()
    {
        mainCam.orthographicSize = 3;
        playerHotbar.SetActive(false);
        controlsDisabled = true; // Disable controls when the animation starts
        injectHover.SetActive(false);
        Input.ResetInputAxes();
        isConsuming = true;
    }

    public void OnInjectAnimationEnd()
    {
        mainCam.orthographicSize = 6;
        playerHotbar.SetActive(true);
        controlsDisabled = false; // Disable controls when the animation starts
        mouseItemData.ItemSprite.enabled = true;
        mouseItemData.ItemCount.enabled = true;
        Input.ResetInputAxes();
        isConsuming = false;
    }
}
