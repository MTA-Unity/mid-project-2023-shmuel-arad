using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages the civil car's behavior
public class CivilCarBehavior : MonoBehaviour 
{
    public float civilCarSpeed;
    public int direction = -1;
    public int crashDamage = 25;

    void Update()
    {
        // Move down by the direction given
        gameObject.transform.Translate(civilCarSpeed * Time.deltaTime * new Vector3(0, direction, 0));
    }

    // This is called when a civil car's trigger is entered, which happens when there's a direct collision between the cars
    void OnTriggerEnter2D(Collider2D collidingObject)
    {
        // If the civil car collides with the player without a shield, damage the player, and destroy the car
        if (collidingObject.gameObject.CompareTag("Player"))
        {
            collidingObject.gameObject.GetComponent<CarHealth>().health -= crashDamage;

            Debug.Log("Civil car collision");

            Destroy(gameObject);
        }
        // Destroy the civil car upon exiting the screen
        else if (collidingObject.gameObject.CompareTag("EndOfRoad"))
        {
            Destroy(gameObject);
        }
    }

    // This is called when a civil car collides with the player, which happens when there's mild collision between the cars
    private void OnCollisionEnter2D(Collision2D collidingObject)
    {
        // Don't destroy the civil car, just damage the player slightly
        if (collidingObject.gameObject.CompareTag("Player"))
        {
            collidingObject.gameObject.GetComponent<CarHealth>().health -= crashDamage / 5;
        }
    }
}
