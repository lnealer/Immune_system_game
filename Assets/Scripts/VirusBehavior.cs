using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBehavior : MonoBehaviour
{
    private float accelerationTime = 2f;
    private float maxSpeed = 3f;
    private float timeLeft;
    public Rigidbody2D rb;
    private Vector2 movement;
    private bool antibodyCollision;
    public float antibodyClock;
    public float replicationDamage; // damage done by replication

    void Start()
    {
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
        if(collision.gameObject.name == "Neutral Cell")
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
        yield return new WaitForSeconds(10f);
        rb.constraints = RigidbodyConstraints2D.None;
    }
}
