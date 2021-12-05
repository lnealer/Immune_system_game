using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private float health = 100;
    private Image healthBar;
    private GameManagerScript gameManager;

    void Start()
    {
        gameManager =  GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        healthBar = this.GetComponent<Image>();
    }

    private void Update()
    {
        health = gameManager.health;
        healthBar.fillAmount = health / 100;
    }


}
