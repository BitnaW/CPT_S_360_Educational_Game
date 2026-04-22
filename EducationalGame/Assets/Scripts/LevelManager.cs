using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class LevelTimer : MonoBehaviour
{
    public float timeRemaining = 60f;
    public TMP_Text timerText;
    private bool timerRunning = true;

//script to handle death conditions
    [Header("Death UI")]
    public GameObject deathScreen;
    public float deathScreenDuration = 2f;



    void Update()
    {
        if (!timerRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerRunning = false;
            OnTimeUp();
        }

        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.SetText("Time Remaining: " + seconds);
    }

    public void DeductTime(float amount)
    {
        timeRemaining -= amount;
    }

    void OnTimeUp()
    {
        StartCoroutine(ShowDeathThenGameOver());
    }

    private System.Collections.IEnumerator ShowDeathThenGameOver()
    {
        Time.timeScale = 0f;

        if (deathScreen != null)
        {
            deathScreen.SetActive(true);
        }

        yield return new WaitForSeconds(deathScreenDuration); //real time to show death screen

        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOverScene"); 
    }

}