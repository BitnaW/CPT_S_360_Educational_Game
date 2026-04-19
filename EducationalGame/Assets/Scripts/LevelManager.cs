using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public float timeRemaining = 60f;
    public TMP_Text timerText;
    private bool timerRunning = true;

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
        Debug.Log("Time's up! Trigger fail state here");
        // load fail screen, reload scene, etc.
    }
}