using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacrophageBehavior : ImmuneBehavior
{
    public bool readAntigen;
    public Rigidbody2D rb;
    private float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        readAntigen = false;
        speed = 1.5f;
    }

    void Update()
    {
        if(readAntigen)
        {
            GameObject[] bCellObjects = GameObject.FindGameObjectsWithTag("BCell");
            if(bCellObjects.Length > 0)
            {
                Vector3 bCellPosition = bCellObjects[0].transform.position;
                UpdatePosition(bCellPosition, speed);
            }
        }
        else
        {
            GameObject[] virusObjects = GameObject.FindGameObjectsWithTag("Virus");
            if(virusObjects.Length > 0)
            {
                Vector3 virusPosition = virusObjects[0].transform.position;
                UpdatePosition(virusPosition, speed);
            }
        }
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.name == "Virus")
        {
            readAntigen = true;
        }
        
        if(collision.gameObject.name == "BCell")
        {
            if(readAntigen)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
            }
        }
    }
}
