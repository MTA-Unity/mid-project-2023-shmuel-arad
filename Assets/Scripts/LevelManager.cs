using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

// This script is in charge of the levels menu
public class LevelManager : MonoBehaviour
{
    public List<Button> levelButtons;

    public static int levelSelected = 0;

    private static int levelUnlocked = -1;
    private static string unlockedLevelFilePath;

    private static void LoadUnlockedLevel()
    {
        unlockedLevelFilePath = Application.persistentDataPath + "/levelReached.dat";

        if (levelUnlocked == -1)
        {
            if (File.Exists(unlockedLevelFilePath)) levelUnlocked = int.Parse(File.ReadAllText(unlockedLevelFilePath));
            else levelUnlocked = 0;
        }
    }

    void Start()
    {
        LoadUnlockedLevel();

        levelButtons[levelSelected].interactable = false;
        for (int levelIndex = 0; levelIndex < levelButtons.Count; levelIndex++)
        {
            int currentLevel = levelIndex;
            levelButtons[levelIndex].onClick.AddListener(() => LevelSelected(currentLevel));
            levelButtons[levelIndex].gameObject.SetActive(true);
        }

        for (int levelIndex = levelUnlocked + 1; levelIndex < levelButtons.Count; levelIndex++)
        {
            levelButtons[levelIndex].gameObject.SetActive(false);
        }
    }

    public void LevelSelected(int level)
    {
        if (level <= levelUnlocked)
        {
            levelButtons[levelSelected].interactable = true;
            levelSelected = level;
            levelButtons[levelSelected].interactable = false;
        }
    }

    public static void UnlockLevel(int level)
    {
        LoadUnlockedLevel();

        if (level < 3 && level > levelUnlocked)
        {
            levelUnlocked = level;
            File.WriteAllText(unlockedLevelFilePath, levelUnlocked.ToString());
        }
    }
}
