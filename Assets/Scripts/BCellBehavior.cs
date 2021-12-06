using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCellBehavior : MonoBehaviour
{
    public GameObject antibody;
    private GameObject antibodyBar;

    void Start()
    {
        antibodyBar = transform.GetChild(0).GetChild(0).gameObject;
        Debug.Log(antibodyBar);
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.tag == "Macrophage" && collision.gameObject.GetComponent<MacrophageBehavior>().readAntigen)
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
