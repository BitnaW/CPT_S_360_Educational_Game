using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class ProcessTarget : MonoBehaviour
{
    private Rigidbody rb;
    public int remainingTime;
    public int burstTime;
    public int arrivalTime;
    private HealthBar healthBar;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // I know this seems useless, but we need it for collisions
    }

    // fixed this, it was deducting by 2
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (remainingTime - 1 == 0)
        {
            Debug.Log("Target destroyed!");
            Destroy(gameObject);
        }
        else if (remainingTime > 0)
        {
            remainingTime--;
            healthBar.UpdateHealthBar(remainingTime, burstTime);
        }
        
    }
    
    

}
