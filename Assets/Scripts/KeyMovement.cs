using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class KeyMovement : MonoBehaviour
{
    // move with arrow keys for debugging, etc.
    public float moveSpeed = 4f;
    private Rigidbody2D rb;
    public float xDirection = 0;
    public float yDirection = 0;
    private Animator animator;
    private bool facingRight = true;

    void Update()
    {
        ProcessInput();
        Animate();
    }

    void FixedUpdate()
    {
        Run();
    }


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.gravityScale = 0;
    }

    private void Run()
    {
        rb.velocity = new Vector2(xDirection * moveSpeed, yDirection * moveSpeed);
        animator.SetBool("Walk",  (Mathf.Abs(rb.velocity.x) > moveSpeed/2 || 
                                    Mathf.Abs(rb.velocity.y) > moveSpeed/2));
    }

    private void ProcessInput()
    {
        xDirection = Input.GetAxis("Horizontal");
        yDirection = Input.GetAxis("Vertical");
    }

    
    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Animate()
    {
        if (xDirection >0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (xDirection <0 && facingRight)
        {
            FlipCharacter();
        }
    }


}
