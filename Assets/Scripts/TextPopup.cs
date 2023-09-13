using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMesh))]
public class TextPopup : MonoBehaviour
{
    public string displayText;
    public GameObject followedObject;
    public float duration = 1.3f;
    public float startFontSize = 0.2f;
    public float endFontSize = 0.3f;
    public Vector2 popupTravel = new(0.2f, 0.4f);
    public float offsetY = 1.2f;
    public float offsetX = 0;
    public Color textColor = Color.white;

    private Vector3 traveledDistance;
    private TextMesh text;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMesh>();
        text.text = displayText;
        elapsedTime = 0;
        traveledDistance = new (offsetX, offsetY, 0);
        transform.Translate(offsetX, offsetY, 0);
        FixedUpdate();
    }

    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > duration)
        {
            Destroy(gameObject);
        }
        else
        {
            text.characterSize = startFontSize + (endFontSize - startFontSize) * (elapsedTime / duration);
            text.color = new Color(textColor.r, textColor.g, textColor.b, 1 - elapsedTime / duration);

            if (followedObject != null)
            {
                traveledDistance.x += popupTravel.x * Time.deltaTime / elapsedTime;
                traveledDistance.y += popupTravel.y * Time.deltaTime / elapsedTime;
                transform.position = followedObject.transform.position + traveledDistance;
            }
            else
            {
                transform.Translate(new Vector3(
                    popupTravel.x * Time.deltaTime / elapsedTime,
                    popupTravel.y * Time.deltaTime / elapsedTime));
            }
        }
    }
}