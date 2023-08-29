using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// In charge of managing the car's health status and changing scenes when dying
public class CarHealth : MonoBehaviour 
{
    public TMP_Text healthText;
    public int maxHealth = 100;
    public int health;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        // If the car is dying, return to main menu
        if (health <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }

        healthText.text = "Health: " + health + " / " + maxHealth;
    }
}
