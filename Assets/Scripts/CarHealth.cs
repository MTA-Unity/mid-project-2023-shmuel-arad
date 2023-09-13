using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// In charge of managing the car's health status and changing scenes when dying
public class CarHealth : MonoBehaviour 
{
    public GameObject deathMenu;

    public TMP_Text healthText;
    public int maxHealth = 100;
    public int health;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        // If the car is dying, show death menu
        if (health <= 0)
        {
            healthText.text = "DEAD";
            deathMenu.SetActive(true);

            return;
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        healthText.text = "HEALTH: " + health + " / " + maxHealth;
    }
}
