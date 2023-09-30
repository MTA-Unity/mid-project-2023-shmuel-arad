using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script manages the score of the player
public class Score : MonoBehaviour {
    public static float CurrentScore { get; private set; }

    public TMP_Text scoreUI;
    public GameObject playerCar;

    private int scoreHundreds;
    private Vector3 prevCarLocation;

    public static void AddScore(int score)
    {
        CurrentScore += score;
        if (CurrentScore < 0) CurrentScore = 0;
    }

    private void Start()
    {
        scoreHundreds = LevelManager.levelSelected + 1;
        CurrentScore = LevelManager.levelSelected * 100;
        prevCarLocation = playerCar.transform.position;
    }

    private void FixedUpdate()
    {
        float multiplier = 1;

        multiplier += (playerCar.transform.position.y + 3) / 10f;

        if (playerCar.transform.position.x < 0)
        {
            if (prevCarLocation.x >= 0)
            {
                TextPopupManager.DisplayTextOnPlayer("x2", Color.gray, 0.6f);
            }

            multiplier *= 2;
        }

        multiplier = Mathf.Clamp(multiplier, 1, multiplier);

        prevCarLocation = playerCar.transform.position;

        // Show score as time elapsed from the start of the game
        CurrentScore += Time.deltaTime * multiplier;
        scoreUI.text = "SCORE: " + ((int)CurrentScore) + "\nx" + decimal.Round((decimal)multiplier, 1);


        if ((CurrentScore - (scoreHundreds * 100) > 0) && (scoreHundreds <= 3))
        {
            LevelManager.UnlockLevel(scoreHundreds);

            TextPopupManager.DisplayTextMidScreen($"{scoreHundreds}00 POINTS\nLevel {scoreHundreds + 1} Unlocked!");
            scoreHundreds++;
        }
    }
}
