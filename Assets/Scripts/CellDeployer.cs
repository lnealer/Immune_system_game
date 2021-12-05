using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDeployer : MonoBehaviour
{

    public string macrophageKey = "One";
    public string bCellKey = "Two";
    public string tCellKey = "Three";
    public int healthLoss = 10;

    // keep track of how many are deployed
    public int tCellCount;
    public int macrophageCount;
    public int bCellCount;

    public int tCellMax = 3; 
    public int macrophageMax = 3;
    public int bCellMax = 3;

    // prefabs of cells
    public GameObject macroPrefab;
    public GameObject tCellPrefab;
    public GameObject bCellPrefab;

    private Vector2 initPosition = new Vector2(0, 0); // where cells are initially deployed
    private GameManagerScript gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    void Update()
    {
        if (Input.GetButtonDown("One"))
        {
            if (macrophageCount < macrophageMax)
            {
                Debug.Log("Macrophage deployed");
                macrophageCount++;
                Instantiate(macroPrefab, initPosition, Quaternion.identity);
                gameManager.LoseHealth(10);
            }
        }
        if (Input.GetButtonDown("Two"))
        {
            if (bCellCount < bCellMax)
            {
                Debug.Log("Bcell deployed");
                bCellCount++;
                Instantiate(bCellPrefab, initPosition, Quaternion.identity);
                gameManager.LoseHealth(10);
            }
        }
        if (Input.GetButtonDown("Three"))
        {
            if (tCellCount < tCellMax)
            {
                Debug.Log("Tcell deployed");
                tCellCount++;
                Instantiate(tCellPrefab, initPosition, Quaternion.identity);
                gameManager.LoseHealth(10);
            }
        }
    }

    public int GetCountByName(string cell)
    {
        if (cell == "Macrophage")
        {
            return macrophageCount;
        }
        if (cell == "BCell")
        {
            return bCellCount;
        }
        if (cell == "TCell")
        {
            return tCellCount;
        }
        else 
        {
            return 0;
        }
    }

        public int GetMaxByName(string cell)
    {
        if (cell == "Macrophage")
        {
            return macrophageMax;
        }
        if (cell == "BCell")
        {
            return bCellMax;
        }
        if (cell == "TCell")
        {
            return tCellMax;
        }
        else 
        {
            return 0;
        }
    }
}
