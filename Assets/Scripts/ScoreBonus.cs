using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the shield bonus effect script
public class ScoreBonus : Bonus
{
    [Header("Score settings")]
    public int scoreToAdd = 20;

    protected override void BonusCollided(Collider2D collidingObject)
    {
        // If the shield bonus collided with an unshielded player
        if (collidingObject.CompareTag("Player") || collidingObject.CompareTag("Shield"))
        {
            TextPopupManager.DisplayTextOnPlayer("+" + scoreToAdd + " POINTS!", Color.cyan);
            Score.AddScore(scoreToAdd);
            Destroy(gameObject);
        }
    }
}
