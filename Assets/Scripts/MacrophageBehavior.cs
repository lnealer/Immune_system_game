using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacrophageBehavior : MonoBehaviour
{
    public bool readAntigen;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        readAntigen = true;
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
                Debug.Log("Macrophage on BCell");
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
                StartCoroutine(DelayRestartMacrophage());
            }
        }
    }

    private IEnumerator DelayRestartMacrophage()
    {
        yield return new WaitForSeconds(5f);
        rb.constraints = RigidbodyConstraints2D.None;
    }
}
