using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TCellBehavior : MonoBehaviour
{
    
    private Animator animator;
    public float speed;
    private float scale;
    private bool targetAcquired = false;
    private NeutralCellBehavior[] cells;
    private Vector2 infectedTarget; // cell tcell will travel to
    private bool destroyed;
    private bool randomMovement;
    //private Vector2 randomGoal;

    private Rigidbody2D rb;



    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        scale = transform.localScale.x;
        cells = GameObject.FindObjectsOfType<NeutralCellBehavior>();
        //cells = GameObject.Find("Cells") gameObject.GetComponentsInChildren<Transform>();
    }


    void Update()
    {
        Animate();
        CheckCells();
        MoveToTarget();
        // if (targetAcquired)
        // {
        //     MoveToTarget();
        // }
        // else if (!targetAcquired & !destroyed)
        // {
        //     CheckCells();
        // }
        // else
        if (!targetAcquired)
        {
            randomMovement = true;
        }
    }

    void Animate()
    {
        // animator.SetBool("Walk",  (Mathf.Abs(rb.velocity.x) > 0 || 
        //                             Mathf.Abs(rb.velocity.y) > 0));
    }

    void CheckCells()
    {
        //Transform[] cells = gameObject.GetComponentsInChildren<Transform>();
        //cells = GameObject.FindObjectsOfType<NeutralCellBehavior>();
        foreach (NeutralCellBehavior cell in cells)
        {
            if (cell.isInfected)
            {
                targetAcquired = true;
                infectedTarget = cell.transform.position;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // when the tcell collides with an infected cell, both are destroyed
        if (collider.gameObject.tag == "Cell"|| collider.gameObject.tag == "NeutralCell")
        {
            if (collider.gameObject.GetComponent<NeutralCellBehavior>().isInfected) 
            {
                targetAcquired = false;
                destroyed = true;
                // destroy cell
                Destroy(collider.gameObject);

                // destroy tcell
                Die();
            }
        }
    }

    void Die()
    {
        animator.Play("Death");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length+0.5f);
    }

     void MoveToTarget()
    {
        // flip the enemy transform to look to point direction
        FlipCharacter();

        // move towards goal
        transform.position = Vector2.MoveTowards(transform.position, infectedTarget, speed*Time.deltaTime);
        animator.SetBool("Walk", true);

        if (randomMovement)
        {
            if (Vector2.Distance(transform.position, infectedTarget) < 1f)
            {
                // set new random goal
                infectedTarget = RandomGoal();
            }
        }

    }

        void FlipCharacter()
    {
        //float scaleX = transform.localScale.x;
        //bool flipped = false;
        // flip the enemy transform to look to point direction
        if (infectedTarget.x > transform.position.x)
        {
            transform.localScale = new Vector3(1,1,1)*scale;
        }
        else
        {
            transform.localScale = new Vector3(-1,1,1)*scale;   
        }

    }

    Vector2 RandomGoal()
    {
        return new Vector2(Random.Range(-3, 3), Random.Range(-3, 3));
    }

}
