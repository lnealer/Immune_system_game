using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntibodyBehavior : ImmuneBehavior
{
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] virusObjects = GameObject.FindGameObjectsWithTag("Virus");
        if(virusObjects.Length > 0)
        {
            Vector3 virusPosition = virusObjects[0].transform.position;
            UpdatePosition(virusPosition, speed);
        }
    }
}
