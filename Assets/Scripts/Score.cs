using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script manages the score of the player
public class Score : MonoBehaviour {
    static float currentScore = 0f;

    public TMP_Text scoreUI;

    public static void AddScore(int score)
    {
        currentScore += score;
    }

    private void Start()
    {
        currentScore = 0;
    }

    private void Update()
    {
        // Show score as time elapsed from the start of the game
        currentScore += Time.deltaTime;
        scoreUI.text = "Score: " + ((int)currentScore).ToString();
    }
}
