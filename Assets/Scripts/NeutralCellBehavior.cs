using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralCellBehavior : MonoBehaviour
{
    public bool isInfected;

    void Start()
    {
        isInfected = false;
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.name == "Virus")
        {
            Debug.Log("Hit by virus");
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            StartCoroutine(Flash());
            StartCoroutine(DelayDestroyCell());
        }
    }

    private IEnumerator Flash()
    {
        for(var n = 0; n < 10; n++)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.5f);
            gameObject.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator DelayDestroyCell()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("Cell destroyed by virus!");
        Destroy(gameObject);
    }
}
