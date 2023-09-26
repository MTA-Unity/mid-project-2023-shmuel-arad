using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// In charge of moving the car by the user input
public class CarControl : MonoBehaviour
{
    public bool ControlsEnabled { get; set; }

    [Header("Car Physics")]
    public float maxSpeed = 3f;
    public float driftFactor = 0.95f;
    public float accelerationFactor = 4f;
    public float turnFactor = 7f;
    public float roadDrag = 135f;
    public float dashForce = 200f;

    [Header("Car Settings")]
    public int maxRotation = 70;

    [Header("Car Dash Settings")]
    public float dashTimeout = 2f;
    public int dashSwipeSpeed = 1000;
    public int dashScoreCost = 5;

    [Header("Car Bullets")]
    public GameObject bulletPrefab;
    public float bulletFireInterval;
    public float bulletYOffset = 1.3f;
    public float bulletXOffset = 0.2f;

    [Header("Car Buttons")]
    public Button gasButton;
    public Button brakeButton;

    private bool gasClicked = false;
    private bool brakeClicked = false;

    private Rigidbody2D carRigidbody2D;

    private float lastDashTime = 0;
    private float lastBulletTime = 0;
    private float lastBulletPosition;

    private void Start()
    {
        ControlsEnabled = true;
        carRigidbody2D = GetComponent<Rigidbody2D>();
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }

        lastBulletPosition = bulletXOffset;
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

            if (degrees > 15f) return 1;
            if (degrees < -15f) return -1;

            return degrees / 15f;
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

    private void ApplyEngineForce(float accelerationInput)
    {
        //Calculate how much "forward" we are going in terms of the direction of our velocity
        float velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        //Limit so we cannot go faster then the max speed in the "forward" direction
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        //Limit so we cannot go faster then the max speed in the "reverse" direction
        if (velocityVsUp < -maxSpeed && accelerationInput < 0)
            return;

        //Limit so we cannot go faster in any direction while accelerating
        if (carRigidbody2D.velocity.magnitude > maxSpeed && accelerationInput > 0)
            return;


        //Create force for the engine
        Vector2 engineForceVector = accelerationFactor * accelerationInput * transform.up;

        //Apply drag if there is no accelerations so the car stops when the player lets go of the accelerator
        if (accelerationInput == 0)
        {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
            engineForceVector.y -= (roadDrag + roadDrag * Mathf.Abs(carRigidbody2D.rotation) / 90f) * Time.fixedDeltaTime;
        }
        else
        {
            carRigidbody2D.drag = 0;
        }

        //Apply force and pushes the car forward
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    private void ApplySteering(float steeringInput)
    {
        //Limit the cars ability to turn when moving slowly
        float minSpeedBeforAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforAllowTurningFactor = Mathf.Clamp01(minSpeedBeforAllowTurningFactor);

        var rotationAngle = carRigidbody2D.rotation;

        //Update the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforAllowTurningFactor;

        rotationAngle = Mathf.Clamp(rotationAngle, -maxRotation, maxRotation);

        //Apply steering bu rotating the car object
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    private void KillOrthogonalVelocity(float accelerationInput)
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        //Limit car drift
        carRigidbody2D.velocity = forwardVelocity + (accelerationInput == 0 ? 1 : driftFactor) * rightVelocity;
    }

    private void PerformDashIfPossible(bool right)
    {
        if (Time.realtimeSinceStartup - lastDashTime > dashTimeout)
        {
            if (dashScoreCost > 0)
            {
                TextPopupManager.DisplayTextOnPlayer($"-{dashScoreCost} POINTS!");
                Score.AddScore(-dashScoreCost);
            }

            lastDashTime = Time.realtimeSinceStartup;
            carRigidbody2D.AddForce(new Vector2((right ? 1 : -1) * dashForce, 0));
        }
    }

    private void DetectDashes()
    {
        if (Input.touchSupported && Input.touchCount > 0)
        {
            Touch lastTouch = Input.GetTouch(0);

            if (lastTouch.deltaTime == 0) return;

            if (lastTouch.deltaPosition.x / lastTouch.deltaTime > dashSwipeSpeed)
            {
                PerformDashIfPossible(true);
            }
            else if (lastTouch.deltaPosition.x / lastTouch.deltaTime < -dashSwipeSpeed)
            {
                PerformDashIfPossible(false);
            }
        }
        else if (Input.GetKey(KeyCode.G))
        {
            PerformDashIfPossible(true);
        }
        else if (Input.GetKey(KeyCode.F))
        {
            PerformDashIfPossible(false);
        }
    }

    public void DetectBullets()
    {
        lastBulletTime += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            if (lastBulletTime >= bulletFireInterval)
            {
                lastBulletTime = 0;

                GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.rotation * new Vector3(lastBulletPosition, bulletYOffset), transform.rotation);
                Physics2D.IgnoreCollision(carRigidbody2D.GetComponent<Collider2D>(), bullet.GetComponent<Collider2D>());
                lastBulletPosition *= -1;
            }
        }
    }

    private void FixedUpdate()
    {
        float accelerationInput = ControlsEnabled ? GetVerticalAxis() : 0;
        float steeringInput = ControlsEnabled ? GetHorizontalAxis() : 0;

        ApplyEngineForce(accelerationInput);
        ApplySteering(steeringInput);
        KillOrthogonalVelocity(accelerationInput);
        if (ControlsEnabled) DetectDashes();
        if (ControlsEnabled) DetectBullets();
    }
}
