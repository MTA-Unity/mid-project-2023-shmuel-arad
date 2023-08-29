using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the shield bonus effect script
public class SpeedBonus : Bonus 
{
    [Header("Speed settings")]
    public float speedBoost;
    public float duration;

    private bool _active;

    protected override void BonusCollided(Collider2D collidingObject)
    {
        // Activate the bonus
        _active = true;
        StartCoroutine("SpeedBoostActivated");
    }

    IEnumerator SpeedBoostActivated()
    {
        // Change the speed of the entire game for the bonus's duration
        Time.timeScale = speedBoost;
        yield return new WaitForSeconds(duration * speedBoost);
        Time.timeScale = 1f;

        // Destroy the bonus only after the effect has taken place
        Destroy(gameObject);
    }

    protected override void DestroyBonus()
    {
        // If the speed bonus exited the screen but it was already activated, we wait for it to finish and then destroy it
        // (from the coroutine)
        if (!_active)
        {
            Destroy(gameObject); 
        }
    }
}
