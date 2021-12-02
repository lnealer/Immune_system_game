using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shape
{
    Circle,
    Square,
    Hexagon
}

[RequireComponent(typeof(BoxCollider2D))]
public class TCellBehavior : MonoBehaviour
{
    public Shape shape;
    
    private Animator animator;
    private Transform infectedTarget; // cell tcell will travel to


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // when the tcell collides with an infected cell, both are destroyed
        if (collider.gameObject.tag == "Cell")
        {
            if (collider.gameObject.GetComponent<CellBehavior>().isInfected) // some kind of bool check here
            {
                // destroy cell
                // collider.gameObject.CellDestroy()  // add this when method is written
                Destroy(collider.gameObject);

                // destroy tcell
                animator.Play("Death");
                Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length+0.5f);
            }
        }
    }

}
