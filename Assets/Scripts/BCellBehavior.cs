using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCellBehavior : MonoBehaviour
{
    
    public GameObject antibodyBar;
    public GameObject antibody;

    void Start()
    {
        antibodyBar = GameObject.Find("AntibodyBar");
        antibodyBar.SetActive(false);
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.name == "Macrophage" && collision.gameObject.GetComponent<MacrophageBehavior>().readAntigen)
        {
            antibodyBar.SetActive(true);
            StartCoroutine(DelayDestroyBCell());
        }
    }

    private IEnumerator DelayDestroyBCell()
    {
        yield return new WaitForSeconds(5f);
        Vector3 pos = transform.position;
        Destroy(gameObject);
        Instantiate(antibody, pos, Quaternion.identity);
    }
}
