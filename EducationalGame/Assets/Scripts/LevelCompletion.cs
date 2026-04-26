using UnityEngine;
using UnityEngine.UI;

public class LevelCompletion : MonoBehaviour
{
    [Header("Level Completion UI")]
    public Image winScreenImage;
    public Sprite[] winFrames;
    public float winFrameRate = 0.3f;
    [SerializeField] private LevelLoader levelLoader;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            StartCoroutine(PlayWinScreen());
        }
    }

    private System.Collections.IEnumerator PlayWinScreen()
    {
        float timeElapsed = 0f;
        winScreenImage.gameObject.SetActive(true);
        winScreenImage.color = new Color(1, 1, 1, 1);
        int frameIndex = 0;
        while (timeElapsed < 3f) 
        {
            winScreenImage.sprite = winFrames[frameIndex % winFrames.Length];
            frameIndex++;
            timeElapsed += winFrameRate;
            yield return new WaitForSecondsRealtime(winFrameRate);
        }
        Time.timeScale = 1f;
        levelLoader.LoadNextLevel();
    }
}