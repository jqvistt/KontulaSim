using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : MonoBehaviour
{

    [SerializeField]private int speed = 2;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    public VectorValue startingPosition;

    private void Awake(){

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    private void Start()
    {
        transform.position = startingPosition.initialValue;
    }

    private void OnMovement(InputValue value){

        movement = value.Get<Vector2>();

    if(movement.x != 0 || movement.y != 0)
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

    private void FixedUpdate(){

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Shift Pressed");

            speed = 3;
            animator.speed = 2;

        }
        else 
        {

            speed = 2;
            animator.speed = 1;

        }
               

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

    }
}
