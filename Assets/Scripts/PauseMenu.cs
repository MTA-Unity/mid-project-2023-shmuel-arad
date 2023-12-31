﻿using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject deathMenu;

    private void OnEnable()
    {
        if (deathMenu.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    public void OnResume()
    {
        Time.timeScale = 1.0f;
    }
    
    public void OnQuit()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnQuit();
        }
    }
}
