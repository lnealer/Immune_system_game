using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacrophageBehavior : ImmuneBehavior
{
    public bool readAntigen = false;
    //public Rigidbody2D rb;
    private float speed;
    public float moveSpeed = 1.5f;
    private Vector2 goal;
    private bool goalAcquired;
    private Animator animator;
    private bool makingAntibody;

    private float antibodyInterval;
    private float antibodyTimer;
    private float scale;

    void Start()
    {
        speed = moveSpeed;
        animator = GetComponent<Animator>();
        scale = transform.localScale.y;
    }

    void Update()
    {   
        if (makingAntibody)
        {
            antibodyTimer += Time.deltaTime;
            if (antibodyTimer >= antibodyInterval )
            {
                goalAcquired = false;
                speed =moveSpeed;
            }
        }

        if (!goalAcquired)
        {
            if (Vector2.Distance(transform.position, goal) < 1f)
            {
                // set new random goal
                goal = RandomGoal();
            }
        }

        Animate();
        transform.position = Vector2.MoveTowards(transform.position, goal, speed*Time.deltaTime);

        if(readAntigen)
        {
            GameObject[] bCellObjects = GameObject.FindGameObjectsWithTag("BCell");
            if(bCellObjects.Length > 0)
            {
                //Vector3 bCellPosition = bCellObjects[0].transform.position;
                //UpdatePosition(bCellPosition, speed);

                goal = bCellObjects[0].transform.position;
                goalAcquired = true;
            }
            else if (goalAcquired == true)
            {
                goalAcquired = false;
                goal = RandomGoal();
            }
        }
        else
        {
            GameObject[] virusObjects = GameObject.FindGameObjectsWithTag("Virus");
            if(virusObjects.Length > 0)
            {
                goal = virusObjects[0].transform.position;
                goalAcquired = true;
            }
        }

    }

    void OnTriggerStay2D (Collider2D collider)
    {
        if(collider.gameObject.tag == "Virus")
        {
            readAntigen = true;
        }
        
        if(collider.gameObject.tag == "BCell")
        {
            if(readAntigen)
            {
                speed = 0f;
                antibodyInterval = collider.gameObject.GetComponent<BCellBehavior>().convertToAntibodyTime;
                antibodyTimer = 0f;
                makingAntibody = true;
            }
        }
    }

    void Animate()
    {
        animator.SetBool("Walk", speed == moveSpeed);   
        FlipCharacter();
    }

    Vector2 RandomGoal()
    {
        return new Vector2(Random.Range(-5, 5), Random.Range(-3, 3));
    }

    void FlipCharacter()
    {
        // flip the enemy transform to look to point direction
        if (goal.x > transform.position.x)
        {
            transform.localScale = new Vector3(1,1,1)*scale;
        }
        else
        {
            transform.localScale = new Vector3(-1,1,1)*scale;   
        }
    }
}
