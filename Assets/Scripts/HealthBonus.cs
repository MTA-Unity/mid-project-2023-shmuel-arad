using UnityEngine;

// This is the health bonus effect script
public class HealthBonus : Bonus
{
    public int repairPoints;

    protected override void BonusCollided(Collider2D collidingObject)
    {
        // Increase health by repair points, and destroy bonus
        TextPopupManager.DisplayTextOnPlayer("+" + repairPoints + " HP", Color.green);
        collidingObject.gameObject.GetComponent<CarHealth>().health += repairPoints;
        Destroy(gameObject);
    }
}
