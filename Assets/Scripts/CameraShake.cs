using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public bool ShakeEnabled { get; set; }

	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;

	float shakeDuration = 0f;
	float shakeAmount = 0.05f;
	Vector3 originalPos;

	public void Shake(float duration)
    {
		enabled = true;
		shakeDuration = Mathf.Max(duration, shakeDuration);
    }

	void Awake()
	{
		ShakeEnabled = true;

		if (camTransform == null)
		{
			camTransform = transform;
		}
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

    void OnDisable()
    {
        camTransform.localPosition = originalPos;
		shakeDuration = 0;
	}

    void FixedUpdate()
	{
		if (!ShakeEnabled) return;
		if (shakeDuration > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime;
		}
		else
		{
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
		}
	}
}
