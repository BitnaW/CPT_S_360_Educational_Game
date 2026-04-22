using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private List<ProcessTarget> processes;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private string schedulingMode;
    [SerializeField] private Shooter player; 
    
    private List<ProcessTarget> validTargets = new();
    private Scheduler scheduler;

    void Start()
    {
        // you can set it in the inspector 
        scheduler = schedulingMode switch
        {
            "RR" => new RrProcessScheduler(),
            "SJF" => new SjfProcessScheduler(),
            _ => new FcfsProcessScheduler() // default value is FCFS
        };
        
        for (int i = 0; i < processes.Count; i++)
        {
            ProcessTarget target = processes[i];
            target.remainingTime = target.burstTime;
            StartCoroutine(SpawnProcess(target,  spawnPoints[i]));
        }
    }


    private IEnumerator SpawnProcess(ProcessTarget process, Transform spawnPoint)
    {
        // wait, then spawn
        yield return new WaitForSeconds(process.arrivalTime);
        ProcessTarget spawned = Instantiate(process, spawnPoint.position, spawnPoint.rotation);
        spawned.targetManager = this;
        validTargets.Add(spawned);
    }

    // targets hold their own damage checking don't worry about that here 
    public void OnTargetHit(ProcessTarget target)
    {
        ProcessTarget valid = scheduler.GetNextTarget(validTargets);
        if (target == valid) // valid target, inflict damage
        {
            target.TargetTakeDamage();
        }
        else
        {
            player.TakeDamage(3); // wrong target, player takes damage
        }
    }

    public void OnTargetDestroyed(ProcessTarget target)
    {
        validTargets.Remove(target);
    }
}