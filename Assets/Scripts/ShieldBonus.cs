using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the shield bonus effect script
public class ShieldBonus : Bonus
{
    [Header("Shield settings")]
    public GameObject shield;

    protected override void BonusCollided(Collider2D collidingObject)
    {
        // If the shield bonus collided with an unshielded player
        if (collidingObject.CompareTag("Player"))
        {
            // Change their tag to note that they are shielded
            collidingObject.gameObject.tag = "Shield";

            // The shield will be placed slightly "below" the car
            Vector3 playerCarPosition = collidingObject.transform.position;
            playerCarPosition.z = -0.1f;

            // Create the shield object as a child object of the player
            GameObject shieldObj = (GameObject)Instantiate(shield, playerCarPosition, Quaternion.identity);
            shieldObj.transform.parent = collidingObject.transform;

            Destroy(gameObject);
        }
    }
}
