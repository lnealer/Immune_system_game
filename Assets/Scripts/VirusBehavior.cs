using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VirusBehavior : MonoBehaviour
{
    public float accelerationTime = 2f;
    //public float maxSpeed = 3f;
    public float moveSpeed;
    private float speed;
    public float infectCellTime = 20f;
    private float timeLeft;
    public Rigidbody2D rb;
    private Vector2 movement;
    private bool antibodyCollision;
    public float antibodyClock;
    public float replicationDamage; // damage done by replication

    private Vector2 goal;
    private bool goalAcquired;
    private Animator animator;
    private float scale;

    void Start()
    {
        scale = transform.localScale.y;
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "NPlus1Level" && GameValues.levelNVaccine != 0)
        {
            moveSpeed *= (1 - (1/GameValues.levelNVaccine));
            accelerationTime = accelerationTime * (1 - (1/GameValues.levelNVaccine));
        }

        if (GameValues.losses > 5)
        {
            //maxSpeed = maxSpeed * .7f;
            accelerationTime *= .7f;
            moveSpeed *= .7f;

        }
        speed = moveSpeed;

        goal = RandomGoal();

        //rb = GetComponent<Rigidbody2D>();
        antibodyCollision = false;
        animator = GetComponent<Animator>();
    }
 
    void Update () 
    {
        timeLeft -= Time.deltaTime;
        if (Time.time >= timeLeft){
            movement = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f));
            timeLeft += accelerationTime;
        }

        if(antibodyCollision)
        {
            antibodyClock -= Time.deltaTime;
        }

        // movement
        if (Vector2.Distance(transform.position, goal) < 1f)
        {
            goal = RandomGoal();
        } 

        Animate();
        transform.position = Vector2.MoveTowards(transform.position, goal, speed*Time.deltaTime);

    }
    
    void Animate()
    {
        animator.SetBool("Walk", speed == moveSpeed);   
        FlipCharacter();
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

    // void FixedUpdate()
    // {
    //     rb.AddForce(movement * maxSpeed);
    // }

    Vector2 RandomGoal()
    {
        return new Vector2(Random.Range(-5, 5), Random.Range(-4, 4));
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.tag == "NeutralCell")
        {
            string cellName = collision.gameObject.name;
            if(!(collision.gameObject.GetComponent<NeutralCellBehavior>().isInfected))
            {
                animator.SetBool("Attack", true);
                Debug.Log("Virus hit healthy cell");
                collision.gameObject.GetComponent<NeutralCellBehavior>().isInfected = true;
                //rb.constraints = RigidbodyConstraints2D.FreezePosition;
                speed = 0;
                StartCoroutine(DelayRestartVirus(cellName));

            }
        }

        if(collision.gameObject.tag == "Antibody")
        {
            antibodyCollision = true;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Virus")
        {
            goal = RandomGoal();
        }

    }

    void OnCollisionExit2D (Collision2D collision)
    {
        if(collision.gameObject.tag == "Antibody")
        {
            antibodyCollision = false;
        }
    } 

    // void OnCollisionStay2D (Collision2D collision)
    // {
    //     if(collision.gameObject.tag == "Antibody" && antibodyClock <= 0)
    //     {
    //         Destroy(collision.gameObject);
    //         Destroy(gameObject);
    //     }
    // }

    private IEnumerator DelayRestartVirus(string cellName)
    {
        while ((infectCellTime > 0) && (GameObject.Find(cellName) != null))
        {
            
            yield return new WaitForSeconds(.1f);
            infectCellTime -= .1f;
        }
        //rb.constraints = RigidbodyConstraints2D.None;
        animator.SetBool("Attack", false);
        speed = moveSpeed;
    }
}
