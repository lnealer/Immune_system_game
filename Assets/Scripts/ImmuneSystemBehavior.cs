using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImmuneSystemBehavior : MonoBehaviour
{
     // move with arrow keys for debugging, etc.
    public float moveSpeed = 4f;
    private Rigidbody2D rb;
    public float xDirection = 0;
    public float yDirection = 0;
    private Animator animator;
    private bool facingRight = false;
    private float scale;
    private bool sideWalking = false;

    void Start()
    {
        scale = transform.localScale.y;
    }
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

        if (rb.velocity.y != 0 || rb.velocity.x != 0)
        {
            animator.SetBool("Side", rb.velocity.x != 0);
            animator.SetFloat("yDirection", rb.velocity.y);
        }

    }

    private void ProcessInput()
    {
        xDirection = Input.GetAxis("Horizontal");
        yDirection = Input.GetAxis("Vertical");
    }

    
    private void FlipCharacter()
    {
        Debug.Log("flipping");
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

        if (facingRight)
        {
             transform.localScale = new Vector3(-1,1,1)*scale;
        }
        else
        {
            transform.localScale = new Vector3(1,1,1)*scale;
        }
    }

    private void Animate()
    {
        if (xDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (xDirection <0 && facingRight)
        {
            FlipCharacter();
        }
        
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.tag == "BodyVirus")
        {
            SceneManager.LoadScene("Tutorial");
        }

        if(collision.gameObject.tag == "NVirus")
        {
            SceneManager.LoadScene("NLevel");
        }

        if(collision.gameObject.tag == "NPlus1Virus")
        {
            SceneManager.LoadScene("NPlus1Level");
        }
    }
}
