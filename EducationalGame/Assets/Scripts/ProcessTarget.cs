using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class ProcessTarget : MonoBehaviour
{
    private Rigidbody rb;
    // TODO: Ingrid - Use system serialize or serialize field, not both 
    [SerializeField] public int remainingTime;
    [SerializeField] public int burstTime;
    [SerializeField] public int arrivalTime;
    public TMP_Text  healthText;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        healthText.SetText(remainingTime.ToString());
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
            healthText.SetText(remainingTime.ToString());
        }
        
    }
    
    

}
