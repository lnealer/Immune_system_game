using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public float health;

    void Start()
    {
        health = GameValues.health;
    }

    public void LoseHealth(float healthLost)
    {
        if (health <= 0)
        {
            return;
        }
        // reduce health
        health -= healthLost;

        if (health <= 0)    
        {
            // character dies
            Debug.Log("you died");
        }
        
    }

    public void GainHealth(int healthGained)
    {
        if (health >= 100)
        {
            return;
        }
        // increase health
        health += healthGained;
    }

    public void Update()
    {
        if ( health <= 0)
        {
            GameValues.losses++;
            SceneManager.LoadScene("GameOver");
        }

        Scene scene = SceneManager.GetActiveScene();
        GameObject[] virusObjects = GameObject.FindGameObjectsWithTag("Virus");
        if(virusObjects.Length == 0 && scene.name != "HumanBody" && scene.name != "NLevelVaccine")
        {
            GameValues.health = health;
            if (scene.name == "NLevel")
            {
                SceneManager.LoadScene("NLevelVaccine");
            }
            else
            {
                SceneManager.LoadScene("HumanBody");
            }
        }
    }
}
