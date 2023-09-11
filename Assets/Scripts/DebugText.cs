using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * A simple class for debug text displaying
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
