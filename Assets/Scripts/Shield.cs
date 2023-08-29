using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is effects the player for the duration of the shield bonus
public class Shield : MonoBehaviour 
{
    public float shieldDuration;

    private void Update()
    {
        shieldDuration -= Time.deltaTime;

        // When the time expires, return the player to their original tag, they are no longer shielded
        if (shieldDuration <= 0)
        {
            gameObject.transform.parent.tag = "Player";
            Destroy(gameObject);
        }
    }
}
