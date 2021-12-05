using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountScript : MonoBehaviour
{
    private string cell;
    private Text countTextBox;

    private int total;
    private int count;
    private CellDeployer cellDeployer;
    
    void Start()
    {
        cellDeployer = GameObject.FindObjectOfType<CellDeployer>();
        countTextBox = GetComponent<Text>();
        cell = transform.parent.name;
        count = cellDeployer.GetMaxByName(cell);
    }

    void Update()
    {
        count = cellDeployer.GetCountByName(cell);
        total = cellDeployer.GetMaxByName(cell);
        SetCount(count, total);
    }

    private void SetCount(int current, int total)
    {
        countTextBox.text = "" + count + "/" + total;
    }
}
