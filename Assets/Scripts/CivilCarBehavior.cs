using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages the civil car's behavior
public class CivilCarBehavior : MonoBehaviour 
{
    public float civilCarSpeed;
    public float timeUntilDestroyed = 2f;
    public int crashDamage = 25;
    public Camera gameCamera;
    bool hasCrashed = false;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -civilCarSpeed);
    }

    // This is called when a civil car's trigger is entered, which happens when there's a direct collision between the cars
    void OnTriggerEnter2D(Collider2D collidingObject)
    {
        // If the civil car collides with the player without a shield, damage the player, and destroy the car
        if (collidingObject.gameObject.CompareTag("Player") && !hasCrashed)
        {
            hasCrashed = true;

            collidingObject.gameObject.GetComponent<CarHealth>().health -= crashDamage;

            gameCamera.GetComponent<CameraShake>().Shake(0.5f);

            TextPopupManager.DisplayTextOnPlayer($"-{crashDamage} HP", Color.red);

            Debug.Log("Civil car collision");

            StartCoroutine(DestroyCivilCar());
        }
        // Destroy the civil car upon exiting the screen
        else if (collidingObject.gameObject.CompareTag("EndOfRoad"))
        {
            Destroy(gameObject);
        }
        else if (collidingObject.gameObject.CompareTag("CivilCar"))
        {
            StartCoroutine(DestroyCivilCar());
        }
    }

    private IEnumerator DestroyCivilCar()
    {
        yield return new WaitForSeconds(timeUntilDestroyed);
        Destroy(gameObject);
    }

    // This is called when a civil car collides with the player, which happens when there's mild collision between the cars
    private void OnCollisionEnter2D(Collision2D collidingObject)
    {
        // Don't destroy the civil car, just damage the player slightly
        if (collidingObject.gameObject.CompareTag("Player") && !hasCrashed)
        {
            hasCrashed = true;
            collidingObject.gameObject.GetComponent<CarHealth>().health -= crashDamage / 5;
            TextPopupManager.DisplayTextOnPlayer($"-{crashDamage / 5} HP", Color.red);
        }
    }
}
