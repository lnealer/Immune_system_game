using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralCellBehavior : MonoBehaviour
{
    public bool isInfected;
    public GameObject virus;
    private float virusReplicationDamage;
    private float cellInfectionTime;

    void Start()
    {
        isInfected = false;
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.tag == "Virus")
        {
            Debug.Log("Hit by virus");
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            virusReplicationDamage = collision.gameObject.GetComponent<VirusBehavior>().replicationDamage;
            cellInfectionTime = collision.gameObject.GetComponent<VirusBehavior>().infectCellTime;
            StartCoroutine(Flash());
            StartCoroutine(DelayDestroyCell());
        }
    }

    private IEnumerator Flash()
    {
        for(var n = 0; n < 100; n++)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.5f);
            gameObject.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator DelayDestroyCell()
    {
        yield return new WaitForSeconds(cellInfectionTime);
        Debug.Log("Cell destroyed by virus!");
        Instantiate(virus, transform.position, Quaternion.identity);
        GameObject.Find("GameManager").GetComponent<GameManagerScript>().LoseHealth(virusReplicationDamage);
        Destroy(gameObject);
    }
}
