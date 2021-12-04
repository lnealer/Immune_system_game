using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCellBehavior : MonoBehaviour
{
    
    public GameObject antibodyBar;

    void Start()
    {
        antibodyBar = GameObject.Find("AntibodyBar");
        antibodyBar.SetActive(false);
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.name == "Macrophage")
        {
            antibodyBar.SetActive(true);
            StartCoroutine(DelayDestroyBCell());
        }
    }

    private IEnumerator DelayDestroyBCell()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
