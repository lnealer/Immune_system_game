using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntibodyBarBehavior : MonoBehaviour
{
    Image antibodyBar;
    public float maxTime;
    float startTime;
    float timeLeft;
    //public GameObject antibody;

    void Start()
    {
        antibodyBar = GetComponent<Image>();
        transform.position = transform.parent.parent.position;
    }
    
    void OnEnable()
    {
        maxTime = 5f;
        startTime = Time.time;
        timeLeft = startTime + maxTime;
        Debug.Log("Antibody progress bar enabled");
    }

    void Update()
    {
        if ((timeLeft - startTime) > 0) {
            timeLeft -= Time.deltaTime;
            antibodyBar.fillAmount = (timeLeft - startTime) / maxTime;
        }
        else {
            //antibody.SetActive(true);
            Destroy(gameObject);
        }
    }
}
