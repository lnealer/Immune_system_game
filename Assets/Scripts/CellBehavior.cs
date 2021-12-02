using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CellBehavior : MonoBehaviour
{
    public bool isInfected;

    // copy below here for movement stuff
    // move with arrow keys for debugging
    private Rigidbody2D rb;
    private float xDirection;
    private float yDirection;

    void Update()
    {
        ProcessInput();
    }

    void FixedUpdate()
    {
        Run();
    }
    void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Run()
    {
        rb.velocity = new Vector2(xDirection * 4, yDirection * 4);
    }

    private void ProcessInput()
    {
        xDirection = Input.GetAxis("Horizontal");
        yDirection = Input.GetAxis("Vertical");
    }
}
