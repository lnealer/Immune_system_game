using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralCellBehavior : MonoBehaviour
{
    public bool isInfected;

    // Start is called before the first frame update
    void Start()
    {
        isInfected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.name == "Virus")
        {
            Debug.Log("Hit by virus");
            StartCoroutine(Flash());
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        
    }

    private IEnumerator Flash()
    {
        for(var n = 0; n < 10; n++)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSecondsRealtime(0.5f);
            gameObject.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSecondsRealtime(0.5f);
        }
        Destroy(gameObject);
    }
}
