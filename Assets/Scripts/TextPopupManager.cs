using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A manager class that is in charge of displaying popup texts
public class TextPopupManager : MonoBehaviour 
{
    public GameObject textPopupPrefab;
    public GameObject playerCar;

    private static GameObject _textPopupPrefab;
    private static GameObject _playerCar;

    private static GameObject _playerPopup;
    private static GameObject _screenPopup;

    void Start()
    {
        _textPopupPrefab = textPopupPrefab;
        _playerCar = playerCar;
    }

    public static void DisplayTextOnPlayer(string text)
    {
        if (_playerPopup == null)
        {
            _playerPopup = Instantiate(_textPopupPrefab);
            TextPopup popupComponent = _playerPopup.GetComponent<TextPopup>();

            popupComponent.displayText = text;
            popupComponent.followedObject = _playerCar;
        }
    }

    public static void DisplayTextMidScreen(string text)
    {
        if (_screenPopup == null)
        {
            _screenPopup = Instantiate(_textPopupPrefab);
            _screenPopup.transform.position = Vector3.zero;

            TextPopup popupComponent = _screenPopup.GetComponent<TextPopup>();

            popupComponent.displayText = text;
            popupComponent.popupTravel.x = 0;
            popupComponent.popupTravel.y = 0.15f;
            popupComponent.offsetY = 0;
            popupComponent.startFontSize = 0.5f;
            popupComponent.endFontSize = 0.6f;
        }
    }
}
