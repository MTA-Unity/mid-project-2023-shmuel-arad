using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the speed bonus effect script
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
        TextPopupManager.DisplayTextOnPlayer("UNLIMITED DASH!");
        var prevTimeout = carControl.dashTimeout;
        var prevCost = carControl.dashScoreCost;

        carControl.dashTimeout = 0.1f;
        carControl.dashScoreCost = 0;

        yield return new WaitForSeconds(duration);

        carControl.dashTimeout = prevTimeout;
        carControl.dashScoreCost = prevCost;

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
