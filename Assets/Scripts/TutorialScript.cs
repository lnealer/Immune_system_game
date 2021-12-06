using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    private float timer;
    private float timeInterval = 4f;

    [SerializeField] private bool startText = true;
    [SerializeField] private bool macrophageText = false;
    [SerializeField] private bool tCellText = false;
    [SerializeField] private bool bCellText = false;
    [SerializeField] private bool macrophageDeployed = false;
    [SerializeField]  private bool bCellDeployed = false;
    [SerializeField] private bool tCellDeployed = false;
    [SerializeField] private NeutralCellBehavior[] cells;

    void Start()
    {
        StartText();
        startText = true;
        cells = GameObject.FindObjectsOfType<NeutralCellBehavior>();
    }

    void CheckCells()
    {
        cells = GameObject.FindObjectsOfType<NeutralCellBehavior>();
        foreach (NeutralCellBehavior cell in cells)
        {
            if (cell.isInfected)
            {
                Debug.Log("Infected cell found");
                tCellText = true;
            }
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeInterval & startText)
        {
            startText = false;
            MacrophageText();
            macrophageText = true;
        }
        
        CheckCells();
        if (tCellText & !tCellDeployed)
        {
            TCellText();
        }
        else if (!macrophageDeployed & !startText)
        {
            MacrophageText();
            macrophageText = true;
        }
        else if (bCellText)
        {
            BCellText();
            bCellText = true;
        }


        if (macrophageText & Input.GetButtonDown("One"))
        {
            macrophageDeployed = true;
            macrophageText = false;
            bCellText = true;
        }
        if (tCellText & Input.GetButtonDown("Three"))
        {
            tCellText = false;
            tCellDeployed = true;
        }

        if (bCellText & Input.GetButtonDown("Two"))
        {
            bCellText = false;
            bCellDeployed = true;
            FinalText();
        }

    }

    public void ShowText()
    {
        gameObject.SetActive(true);
    }

    public void HideText()
    {
        gameObject.SetActive(false);
    }

    void MacrophageText()
    {
        HideText();
        gameObject.GetComponent<Text>().text = "Press 1 to deploy a macrophage to read the virus's antigen";
        ShowText();
    }

    void BCellText()
    {
        HideText();
        gameObject.GetComponent<Text>().text = "Great! Now that you've got a macrophage, press 2 to send out a bCell and create an antibody.";
        ShowText();
    }

    void TCellText()
    {
        HideText();
        gameObject.GetComponent<Text>().text = "The virus is trying to replicate... better press 3 to deploy a tCell!";
        ShowText();
    }

    void StartText()
    {
        gameObject.GetComponent<Text>().text = "Oh no! A virus has invaded!";
        ShowText();
    }

    void FinalText()
    {
        gameObject.GetComponent<Text>().text = "Awesome job! Now wait while the BCell makes the antibody.";
    }
}
