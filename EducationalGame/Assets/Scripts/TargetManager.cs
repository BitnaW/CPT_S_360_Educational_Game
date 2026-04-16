using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private List<ProcessTarget> processes;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private string schedulingMode;

    void Start()
    {
        int i = 0;
        foreach (var process in processes)
        {
            process.remainingTime = process.burstTime;
            StartCoroutine(RunProcess(process, spawnPoints[i]));
            i++;
        }
    }
    
    private IEnumerator RunProcess(ProcessTarget process, Transform spawnPoint)
    {
        // wait, then spawn
        yield return new WaitForSeconds(process.arrivalTime);
        Instantiate(process, spawnPoint.position, spawnPoint.rotation);
    }
}