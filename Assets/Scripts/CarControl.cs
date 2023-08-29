using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// In charge of moving the car by the user input
public class CarControl : MonoBehaviour
{
    public float carHorizontalSpeed;
    public float carVerticalSpeed;

    private void Update()
    {
        Vector3 carPosition = gameObject.transform.position;

        // Calculate x position by horizontal speed and time passed
        carPosition.x += Input.GetAxis("Horizontal") * carHorizontalSpeed * Time.deltaTime;

        // Calculate y position by vertical speed and time passed
        carPosition.y += Input.GetAxis("Vertical") * carVerticalSpeed * Time.deltaTime;

        // Make sure the location doesn't exceed the road
        carPosition.x = Mathf.Clamp(carPosition.x, -2.40f, 2.40f);
        carPosition.y = Mathf.Clamp(carPosition.y, -4f, 4f);

        gameObject.transform.position = carPosition;
    }
}
