using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the shield bonus effect script
public class SpeedBonus : Bonus 
{
    [Header("Speed settings")]
    public float duration;

    private bool _active;

    protected override void BonusCollided(Collider2D collidingObject)
    {
        // Activate the bonus
        _active = true;
        GetComponent<Renderer>().enabled = false;
        StartCoroutine(SpeedBoostActivated(collidingObject.GetComponent<CarControl>()));
    }

    IEnumerator SpeedBoostActivated(CarControl carControl)
    {
        carControl.accelerationFactor *= 1.2f;
        carControl.turnFactor *= 1.2f;
        carControl.maxSpeed *= 1.2f;

        yield return new WaitForSeconds(duration);

        carControl.accelerationFactor /= 1.2f;
        carControl.turnFactor /= 1.2f;
        carControl.maxSpeed /= 1.2f;

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
