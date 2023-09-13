using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages the civil car's behavior
public class Wall : MonoBehaviour 
{
    public int damage = 1;
    public float interval = 0.2f;
    public Camera gameCamera;

    private float timeLeftToDamage = 0f;
    private CameraShake shake;
    private CarHealth currentCollider;

    void Start()
    {
        currentCollider = null;
        timeLeftToDamage = 0;
        shake = gameCamera.GetComponent<CameraShake>();
    }

    void FixedUpdate()
    {
        if (timeLeftToDamage > 0)
        {
            timeLeftToDamage -= Time.deltaTime;
        }
        else if (currentCollider != null)
        {
            timeLeftToDamage = interval;
            currentCollider.health -= damage;
            TextPopupManager.DisplayTextOnPlayer($"-{damage} HP", Color.red);
        }
    }

    // This is called when the player collides with the wall
    private void OnCollisionEnter2D(Collision2D collidingObject)
    {
        if (collidingObject.gameObject.CompareTag("Player"))
        {
            currentCollider = collidingObject.gameObject.GetComponent<CarHealth>();

            shake.Shake(1000f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        currentCollider = null;
        shake.enabled = false;
    }
}
