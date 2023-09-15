using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public TMP_Text finalScoreText;
    public GameObject scoreObject;
    public GameObject playerCar;
    public Camera gameCamera;

    private void OnEnable()
    {
        finalScoreText.text = $"FINAL SCORE: {(int)Score.CurrentScore}";
        scoreObject.GetComponent<Score>().enabled = false;
        playerCar.GetComponent<CarControl>().ControlsEnabled = false;
        TextPopupManager.PopupsEnabled = false;
        gameCamera.GetComponent<CameraShake>().ShakeEnabled = false;
        Bonus.BonusesEnabled = false;
    }

    public void OnQuit()
    {
        TextPopupManager.PopupsEnabled = true;
        Bonus.BonusesEnabled = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRestart()
    {
        TextPopupManager.PopupsEnabled = true;
        Bonus.BonusesEnabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnQuit();
        }
    }
}
