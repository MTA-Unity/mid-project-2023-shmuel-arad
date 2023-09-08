using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// In charge of moving the car by the user input
public class CarControl : MonoBehaviour
{
    public float carHorizontalSpeed;
    public float carVerticalSpeed;

    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
    }

    private static float GetHorizontalAxis()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Quaternion attitude = Input.gyro.attitude;
            attitude = Quaternion.Euler(90, 0, 0) * new Quaternion(attitude.x, attitude.y, -attitude.z, -attitude.w);

            Vector3 direction = attitude * Vector3.left;
            float degrees = Mathf.Atan2(direction.y, new Vector2(direction.x, direction.z).magnitude) * Mathf.Rad2Deg;

            return Mathf.Clamp(degrees / 30f, -1f, 1f);
        }

        return Input.GetAxis("Horizontal");
    }

    private float GetVerticalAxis()
    {
        if (Input.touchSupported)
        {
            if (Input.touchCount == 0)
            {
                return 0;
            }

            return Input.GetTouch(0).deltaPosition.y > 0 ? 1 : -1;
        }

        return Input.GetAxis("Vertical");
    }

    private void Update()
    {
        Vector3 carPosition = gameObject.transform.position;

        // Calculate x position by horizontal speed and time passed
        carPosition.x += GetHorizontalAxis() * carHorizontalSpeed * Time.deltaTime;

        // Calculate y position by vertical speed and time passed
        carPosition.y += GetVerticalAxis() * carVerticalSpeed * Time.deltaTime;

        // Make sure the location doesn't exceed the road
        carPosition.x = Mathf.Clamp(carPosition.x, -2f, 2f);
        carPosition.y = Mathf.Clamp(carPosition.y, -4.4f, 4.5f);

        gameObject.transform.position = carPosition;


        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
