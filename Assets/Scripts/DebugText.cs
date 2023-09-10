using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * Abstract class representing a retrievable bonus. 
 * A bonus class must implement the 'BonusCollided' method that defines what it does
 */
public class DebugText : MonoBehaviour 
{
    public static string textToDisplay = "";
    public TMP_Text text;

    private void Update()
    {
        text.text = textToDisplay;
    }
}
