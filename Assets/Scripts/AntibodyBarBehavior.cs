using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntibodyBarBehavior : MonoBehaviour
{
    private float maxTime;
    private float startTime;
    private float timeLeft;
    private float maxLength = 0.3f;
    
    public Vector3 scale;

    void Start()
    {
        scale = transform.localScale;
        //transform.position = transform.parent.position;
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
            scale.x = (((timeLeft - startTime) / maxTime) * maxLength);
            transform.localScale = scale;
        }
        else {
            Destroy(gameObject);
        }
    }
}
