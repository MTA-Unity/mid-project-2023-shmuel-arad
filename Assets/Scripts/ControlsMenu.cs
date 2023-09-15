using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using TMPro;

// This script is in charge of the controls menu
public class ControlsMenu : MonoBehaviour
{
    public GameObject mainMenu;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
