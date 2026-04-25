using UnityEngine;
using UnityEngine.SceneManagement;

public class Level4GoalTrigger : MonoBehaviour
{
    // enable this object once player reaches the end
    [SerializeField] private SignalSpawner signalSpawner;
    [SerializeField] private string victorySceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        Level4RunnerPlayer player = collision.GetComponent<Level4RunnerPlayer>();
        if (player == null)
        {
            player = collision.GetComponentInParent<Level4RunnerPlayer>();
        }

        if (player != null)
        {
            player.OnReachedGoal();
        }

        if (signalSpawner != null)
        {
            signalSpawner.StopSpawning();
            signalSpawner.DestroyAllSignals();
        }

        if (!string.IsNullOrEmpty(victorySceneName))
        {
            SceneManager.LoadScene(victorySceneName);
        }

        // goal should only trigger once
        gameObject.SetActive(false);
    }
}