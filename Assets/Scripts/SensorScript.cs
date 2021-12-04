using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorScript : MonoBehaviour
{
    public GameObject tCell;
    void OnTriggerStay2D(Collider2D collider)
    {
        Debug.Log("sensor triggered:" + collider.gameObject.name);

        if (collider.gameObject.tag == "Cell")
        {
            Debug.Log("tcell located cell");
            if (collider.gameObject.GetComponent<NeutralCellBehavior>().isInfected) // some kind of bool check here
            {
                Debug.Log("tcell located infected cell");
                if (!tCell.GetComponent<TCellBehavior>().targetAcquired)
                {
                    tCell.GetComponent<TCellBehavior>().targetAcquired = true;
                    tCell.GetComponent<TCellBehavior>().infectedTarget = collider.transform;
                }
            }
        }
    }
}
