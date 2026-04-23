using UnityEngine;
using UnityEngine.UI;

public class LevelCompletion : MonoBehaviour
{
    [Header("Level Completion UI")]
    public Image winScreenImage;
    public Sprite[] winFrames;
    public float winFrameRate = 0.3f;

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
        winScreenImage.gameObject.SetActive(true);
        winScreenImage.color = new Color(1, 1, 1, 1);
        int frameIndex = 0;
        while (true)
        {
            winScreenImage.sprite = winFrames[frameIndex % winFrames.Length];
            frameIndex++;
            yield return new WaitForSecondsRealtime(winFrameRate);
        }
    }
}