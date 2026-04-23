using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class LevelTimer : MonoBehaviour
{
    public float timeRemaining = 60f;
    public TMP_Text timerText;
    private bool timerRunning = true;

//script to handle death conditions
[Header("Death UI")]
public Image deathScreenImage;  
public Sprite[] deathFrames;    
public float deathFrameRate = 0.3f;
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
        deathScreenImage.gameObject.SetActive(true);
        deathScreenImage.color = new Color(1, 1, 1, 1);
        StartCoroutine(PlayDeathAnimation());

        yield return new WaitForSecondsRealtime(deathScreenDuration);

        Time.timeScale = 1f;
        //if restart, do logic here:
        //SceneManager.LoadScene("GameOverScene");
    }
    private System.Collections.IEnumerator PlayDeathAnimation()
    {
        int frameIndex = 0;
        while (true)
        {
            deathScreenImage.sprite = deathFrames[frameIndex % deathFrames.Length];
            frameIndex++;
            yield return new WaitForSecondsRealtime(deathFrameRate);
        }
    }
}