using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject startScreenPanel;
    public TextMeshProUGUI timerText;
    public GameObject crosshair;

    public GameObject weapon;

    private float elapsedTime = 0f;
    private bool timerRunning = false;

    [Header("End Screen")]
    public GameObject endScreenPanel;
    public TextMeshProUGUI endTimerText;

    void Start()
    {
        crosshair.SetActive(false);
        weapon.SetActive(false);
        endScreenPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (timerRunning)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            int hundredths = Mathf.FloorToInt((elapsedTime % 1f) * 100f);
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
        }
    }

    public void OnPlayButtonClicked()
    {
        weapon.SetActive(true);
        crosshair.SetActive(true);
        PlayerController.gameStarted = true;
        startScreenPanel.SetActive(false);
        timerText.gameObject.SetActive(true);
        timerRunning = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void CheckTargets()
    {
        if (GameObject.FindGameObjectsWithTag("Target").Length == 0)
        {
            timerRunning = false;
            PlayerController.gameStarted = false;
            endTimerText.text = timerText.text;
            endScreenPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            crosshair.SetActive(false);
            timerText.gameObject.SetActive(false);

        }
    }

    public void OnPlayAgainClicked()
    {
        crosshair.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}