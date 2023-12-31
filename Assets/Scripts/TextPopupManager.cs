﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A manager class that is in charge of displaying popup texts
public class TextPopupManager : MonoBehaviour 
{
    public static bool PopupsEnabled { get; set; }
    public GameObject textPopupPrefab;
    public GameObject playerCar;

    private static GameObject _textPopupPrefab;
    private static GameObject _playerCar;

    private static float playerPopupTimeout = 0.01f;
    private static float screenPopupTimeout = 0.01f;

    private static float timeSinceLastPlayerPopup = 0;
    private static float timeSinceLastScreenPopup = 0;

    void Start()
    {
        PopupsEnabled = true;
        timeSinceLastPlayerPopup = 0;
        timeSinceLastScreenPopup = 0;
        _textPopupPrefab = textPopupPrefab;
        _playerCar = playerCar;
    }

    void Update()
    {
        timeSinceLastPlayerPopup += Time.deltaTime;
        timeSinceLastScreenPopup += Time.deltaTime;
    }

    public static void DisplayTextOnPlayer(string text, float duration = 1.3f)
    {
        DisplayTextOnPlayer(text, Color.white, duration);
    }

    public static void DisplayTextOnObject(GameObject parent, string text, float duration = 1.3f)
    {
        DisplayTextOnObject(parent, text, Color.white, duration);
    }

    public static void DisplayTextOnObject(GameObject parent, string text, Color textColor, float duration = 1.3f)
    {
        if (PopupsEnabled)
        {
            GameObject playerPopup = Instantiate(_textPopupPrefab);
            TextPopup popupComponent = playerPopup.GetComponent<TextPopup>();

            popupComponent.displayText = text;
            popupComponent.followedObject = parent;
            popupComponent.textColor = textColor;
            popupComponent.duration = duration;
            popupComponent.popupTravel *= duration / 1.3f;

            if (parent.transform.position.x > 0)
            {
                popupComponent.popupTravel.x *= -1;
                popupComponent.offsetX *= -1;
            }

            if (parent.transform.position.y +
                popupComponent.popupTravel.y +
                popupComponent.offsetY > 4)
            {
                popupComponent.popupTravel.y *= -1;
                popupComponent.offsetY *= -1;
            }
        }
    }

    public static void DisplayTextOnPlayer(string text, Color textColor, float duration = 1.3f)
    {
        if (timeSinceLastPlayerPopup > playerPopupTimeout)
        {
            timeSinceLastPlayerPopup = 0;
            DisplayTextOnObject(_playerCar, text, textColor, duration);
        }
    }

    public static void DisplayTextMidScreen(string text)
    {
        if (PopupsEnabled && timeSinceLastScreenPopup > screenPopupTimeout)
        {
            timeSinceLastScreenPopup = 0;
            GameObject screenPopup = Instantiate(_textPopupPrefab);
            screenPopup.transform.position = Vector3.zero;

            TextPopup popupComponent = screenPopup.GetComponent<TextPopup>();

            popupComponent.displayText = text;
            popupComponent.popupTravel.x = 0;
            popupComponent.popupTravel.y = 0.15f;
            popupComponent.offsetY = 0;
            popupComponent.startFontSize = 0.4f;
            popupComponent.endFontSize = 0.5f;
        }
    }
}
