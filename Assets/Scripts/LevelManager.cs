using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

// This script is in charge of the levels menu
public class LevelManager : MonoBehaviour
{
    public GameObject mainMenu;
    public Button[] levelButtons = new Button[3];

    public static int levelSelected = 0;

    private static int levelUnlocked = -1;
    private static string unlockedLevelFilePath;

    /**
     * Get the highest unlocked level from previous runs
     */
    private static void LoadUnlockedLevel()
    {
        unlockedLevelFilePath = Application.persistentDataPath + "/levelReached.dat";

        // When getting to the levels menu for the first time, load the unlocked levels from a saved file in the FS if exists
        if (levelUnlocked == -1)
        {
            // If the user unlocked some levels in previous runs, the file will exist
            if (File.Exists(unlockedLevelFilePath)) levelUnlocked = int.Parse(File.ReadAllText(unlockedLevelFilePath));
            else levelUnlocked = 0;
        }
    }

    void Start()
    {
        LoadUnlockedLevel();

        levelButtons[levelSelected].interactable = false;
        // Add a listener to every level button in case selected
        for (int levelIndex = 0; levelIndex < levelButtons.Length; levelIndex++)
        {
            int currentLevel = levelIndex;
            levelButtons[levelIndex].onClick.AddListener(() => LevelSelected(currentLevel));
            levelButtons[levelIndex].gameObject.SetActive(true);
        }
        
        // Deactivate the levels that weren't unlocked
        for (int levelIndex = levelUnlocked + 1; levelIndex < levelButtons.Length; levelIndex++)
        {
            levelButtons[levelIndex].gameObject.SetActive(false);
        }
    }

    /**
     * Change the level played to be the level selected
     */
    public void LevelSelected(int level)
    {
        if (level <= levelUnlocked)
        {
            levelButtons[levelSelected].interactable = true;
            levelSelected = level;
            levelButtons[levelSelected].interactable = false;
        }
    }

    /**
     * Unlock the given level
     */
    public static void UnlockLevel(int level)
    {
        LoadUnlockedLevel();

        if (level < 3 && level > levelUnlocked)
        {
            levelUnlocked = level;

            // Save the unlock level for future runs
            File.WriteAllText(unlockedLevelFilePath, levelUnlocked.ToString());
        }
    }

    /**
     * When the user tries to exit the game
     */
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
