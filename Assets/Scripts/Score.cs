using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script manages the score of the player
public class Score : MonoBehaviour {
    float elapsed = 0f;

    public TMP_Text scoreUI;

    private void Update()
    {
        // Show score as time elapsed from the start of the game
        elapsed += Time.deltaTime;
        scoreUI.text = "Score: " + ((int)elapsed).ToString();
    }
}
