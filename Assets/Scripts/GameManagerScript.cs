using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public float health;

    void Start()
    {
        health = 100;
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
}
