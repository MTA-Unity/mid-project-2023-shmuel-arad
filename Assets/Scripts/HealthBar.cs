using UnityEngine;
using TMPro;
using UnityEngine.UI;

// In charge of managing the car's health status and changing scenes when dying
public class HealthBar : MonoBehaviour 
{
    public GameObject playerObject;

    public TMP_Text healthText;
    public Slider healthSlider;
    public Image healthFill;

    private CarHealth carHealth;

    private void Start()
    {
        carHealth = playerObject.GetComponent<CarHealth>();
        healthSlider.maxValue = carHealth.maxHealth;
    }

    private void Update()
    {
        float carHealthRatio = ((float)carHealth.health / carHealth.maxHealth);
        healthFill.color = new Color(1 - carHealthRatio, carHealthRatio, 0);
        healthText.text = carHealth.health > 0 ? carHealth.health.ToString() : "DEAD";
        healthSlider.value = carHealth.health;
    }
}
