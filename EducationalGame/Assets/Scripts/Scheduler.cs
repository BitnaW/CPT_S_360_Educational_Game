using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// base class
public abstract class Scheduler
{
    public abstract ProcessTarget GetNextTarget(List<ProcessTarget> validTargets);
}

// FCFS
public class FcfsProcessScheduler : Scheduler
{
    public override ProcessTarget GetNextTarget(List<ProcessTarget> validTargets)
    {
        // sorted by arrival time 
        // using Linq methods to keep it a little simpler
        return validTargets.OrderBy(t => t.arrivalTime).FirstOrDefault();
    }
}

// SJF
public class SjfProcessScheduler : Scheduler
{
    public override ProcessTarget GetNextTarget(List<ProcessTarget> validTargets)
    {
        // sorted by lowest burst time 
        return validTargets.OrderBy(t => t.burstTime).FirstOrDefault();
    }
}

// RR
public class RrProcessScheduler : Scheduler
{
    private int currentIndex = 0;
    public override ProcessTarget GetNextTarget(List<ProcessTarget> validTargets)
    {
        if (validTargets.Count == 0)
        {
            return null;
        }
        
        currentIndex %= validTargets.Count; // mods then equals 
        return validTargets[currentIndex++];
    }
}