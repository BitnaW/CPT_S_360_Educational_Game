using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalSpawner : MonoBehaviour
{
    // prefabs should already contain signalobstacle
    [SerializeField] private List<GameObject> signalPrefabs = new List<GameObject>();
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    [SerializeField] private float startDelay = 1f;
    [SerializeField] private float minSpawnInterval = 0.6f;
    [SerializeField] private float maxSpawnInterval = 1.5f;
    [SerializeField] private float difficultyRampPerSecond = 0.03f;

    private bool stopSpawning;

    private List<GameObject> spawnedSignals = new List<GameObject>();

    private void OnEnable()
    {
        stopSpawning = false;
        StartCoroutine(SpawnLoop());
    }

    private void OnDisable()
    {
        stopSpawning = true;
        StopAllCoroutines();
    }

    public void StopSpawning()
    {
        stopSpawning = true;
    }

    public void DestroyAllSignals()
    {
        foreach (GameObject signal in spawnedSignals)
        {
            if (signal != null)
            {
                Destroy(signal);
            }
        }

        spawnedSignals.Clear();
    }

    private IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(startDelay);

        float elapsed = 0f;

        while (!stopSpawning)
        {
            SpawnOne();

            float intervalMax = Mathf.Max(minSpawnInterval, maxSpawnInterval - (elapsed * difficultyRampPerSecond));
            float wait = Random.Range(minSpawnInterval, intervalMax);
            yield return new WaitForSeconds(wait);

            elapsed += wait;
        }
    }

    private void SpawnOne()
    {
        if (signalPrefabs.Count == 0 || spawnPoints.Count == 0)
        {
            return;
        }

        GameObject prefab = signalPrefabs[Random.Range(0, signalPrefabs.Count)];
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Count)];

        GameObject signal = Instantiate(prefab, point.position, point.rotation);

        spawnedSignals.Add(signal);
    }
}