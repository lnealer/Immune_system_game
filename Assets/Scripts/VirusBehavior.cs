using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VirusBehavior : MonoBehaviour
{
    public float accelerationTime = 2f;
    public float maxSpeed = 3f;
    public float infectCellTime = 20f;
    private float timeLeft;
    public Rigidbody2D rb;
    private Vector2 movement;
    private bool antibodyCollision;
    public float antibodyClock;
    public float replicationDamage; // damage done by replication

    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "NPlus1Level" && GameValues.levelNVaccine != 0)
        {
            maxSpeed = maxSpeed * (1 - (1/GameValues.levelNVaccine));
            accelerationTime = accelerationTime * (1 - (1/GameValues.levelNVaccine));
        }

        if (GameValues.losses > 5)
        {
            maxSpeed = maxSpeed * .7f;
            accelerationTime = accelerationTime * .7f;
        }

        rb = GetComponent<Rigidbody2D>();
        antibodyCollision = false;
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
    }

    void FixedUpdate()
    {
        rb.AddForce(movement * maxSpeed);
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.tag == "NeutralCell")
        {
            if(!(collision.gameObject.GetComponent<NeutralCellBehavior>().isInfected))
            {
                Debug.Log("Virus hit healthy cell");
                collision.gameObject.GetComponent<NeutralCellBehavior>().isInfected = true;
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
                StartCoroutine(DelayRestartVirus());
            }
        }

        if(collision.gameObject.tag == "Antibody")
        {
            antibodyCollision = true;
        }
    }

    void OnCollisionExit2D (Collision2D collision)
    {
        if(collision.gameObject.tag == "Antibody")
        {
            antibodyCollision = false;
        }
    } 

    void OnCollisionStay2D (Collision2D collision)
    {
        if(collision.gameObject.tag == "Antibody" && antibodyClock < 0)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private IEnumerator DelayRestartVirus()
    {
        yield return new WaitForSeconds(infectCellTime);
        rb.constraints = RigidbodyConstraints2D.None;
    }
}
