using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script manages the main overlay UI functionality
public class MainOverlay : MonoBehaviour
{
    public GameObject pauseMenu;

    void Update()
    {
        // When the user tries to pause the game while playing
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
        }
    }
}
