using UnityEngine;

//allow popup screen when exit destination is reached
public class LevelCompletion : MonoBehaviour
{
    [Header("Level Completion UI")]
    public GameObject levelCompleteScreen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0f; //freeze game to display completion screen
            if (levelCompleteScreen != null)
            {
                levelCompleteScreen.SetActive(true);
            }
        }
    }

}
