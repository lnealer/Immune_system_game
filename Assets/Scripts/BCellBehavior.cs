using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BCellBehavior : MonoBehaviour
{
    public GameObject antibody;
    public GameObject antibodyBar;

    private float convertToAntibodyTime = 5f;

    void Start()
    {
        //antibodyBar = transform.GetChild(0).gameObject;
        //Debug.Log(antibodyBar);

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "NPlus1Level" && GameValues.levelNVaccine != 0)
        {
            convertToAntibodyTime = convertToAntibodyTime/2;
        }
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.tag == "Macrophage" && collision.gameObject.GetComponent<MacrophageBehavior>().readAntigen)
        {
            Instantiate(antibodyBar, transform);
            StartCoroutine(DelayDestroyBCell());
        }
    }

    private IEnumerator DelayDestroyBCell()
    {
        yield return new WaitForSeconds(convertToAntibodyTime);
        Vector3 pos = transform.position;
        Destroy(gameObject);
        Instantiate(antibody, pos, Quaternion.identity);
    }
}
