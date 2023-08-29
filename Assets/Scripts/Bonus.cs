using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Abstract class representing a retrievable bonus. 
 * A bonus class must implement the 'BonusCollided' method that defines what it does
 */
public abstract class Bonus : MonoBehaviour 
{
    [Header("Bonuses settings")]
    public float bonusSpeed = 5f;

    private void Update()
    {
        // Move down on the road
        gameObject.transform.Translate(bonusSpeed * Time.deltaTime * new Vector3(0, -1, 0));
    }

    private void OnTriggerEnter2D(Collider2D collidingObject)
    {
        // If the bonus collided with the player (shielded or not)
        if (collidingObject.gameObject.CompareTag("Player") || collidingObject.gameObject.CompareTag("Shield"))
        {
            BonusCollided(collidingObject);
        }
        // If the bonus collided with the end of the screen
        else if (collidingObject.gameObject.CompareTag("EndOfRoad"))
        {
            DestroyBonus();
        }
    }

    // Abstract method that defines what the bonus does
    protected abstract void BonusCollided(Collider2D collidingObject);

    // Virtual method that defines what to do when the bonus reaches the end of the screen
    protected virtual void DestroyBonus()
    {
        Destroy(gameObject);
    }
}
