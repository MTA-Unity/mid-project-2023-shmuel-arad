using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// In charge of moving the car by the user input
public class CarControl : MonoBehaviour
{
    public float carHorizontalSpeed;
    public float carVerticalSpeed;
    public Button gasButton;
    public Button brakeButton;

    private bool gasClicked = false;
    private bool brakeClicked = false;

    private void Start()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
    }

    public void GasButtonClicked()
    {
        var scale = gasButton.transform.localScale;
        scale.y = 0.9f;
        gasButton.transform.localScale = scale;

        gasClicked = true;
    }

    public void GasButtonReleased()
    {
        var scale = gasButton.transform.localScale;
        scale.y = 1;
        gasButton.transform.localScale = scale;

        gasClicked = false;
    }

    public void BrakeButtonClicked()
    {
        var scale = brakeButton.transform.localScale;
        scale.y = 0.9f;
        brakeButton.transform.localScale = scale;

        brakeClicked = true;
    }

    public void BrakeButtonReleased()
    {
        var scale = brakeButton.transform.localScale;
        scale.y = 1;
        brakeButton.transform.localScale = scale;

        brakeClicked = false;
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
        if (gasClicked != brakeClicked)
        {
            return gasClicked ? 1f : -1f;
        }

        return Input.GetAxis("Vertical");
    }

    Rigidbody2D carRigidbody2D;
    float velocityVsUp;

    float maxSpeed = 3;
    float driftFactor = 0.95f;
    float accelerationFactor = 3.0f;
    float turnFactor = 5f;


    private void ApplyEngineForce()
    {
        float accelerationInput = GetVerticalAxis();

        //Calculate how much "forward" we are going in terms of the direction of our velocity
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        //Limit so we cannot go faster then the max speed in the "forward" direction
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        //Limit so we cannot go faster then the max speed in the "reverse" direction
        if (velocityVsUp < -maxSpeed && accelerationInput < 0)
            return;

        //Limit so we cannot go faster in any direction while accelerating
        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;


        //Apply drag if there is no accelerations so the car stops when the player lets go of the accelerator
        if (accelerationInput == 0)
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        else
            carRigidbody2D.drag = 0;

        //Create force for the engine
        Vector2 engineForceVector = accelerationFactor * accelerationInput * transform.up;

        //Apply force and pushes the car forward
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);

    }
    private void ApplySteering()
    {
        float steeringInput = GetHorizontalAxis();
        //Limit the cars ability to turn when moving slowly
        float minSpeedBeforAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforAllowTurningFactor = Mathf.Clamp01(minSpeedBeforAllowTurningFactor);

        var rotationAngle = carRigidbody2D.rotation;

        //Update the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforAllowTurningFactor;

        rotationAngle = Mathf.Clamp(rotationAngle, -85, 85);

        //Apply steering bu rotating the car object
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    private void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        //Limit car drift
        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }


    private void Update()
    {
        ApplyEngineForce();
        ApplySteering();
        KillOrthogonalVelocity();

        Vector3 carPosition = gameObject.transform.position;

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
