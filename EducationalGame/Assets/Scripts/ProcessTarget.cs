using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ProcessTarget : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] public int remainingTime;
    [SerializeField] public int burstTime;
    [SerializeField] public int arrivalTime;
    [SerializeField] private HealthBar healthBar;
    
    [HideInInspector] public TargetManager targetManager; // don't have to initialize it in the inspector 
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            targetManager.OnTargetHit(this); // passed through to the manager to determine if it was a valid target
        }
    }

    public void TargetTakeDamage()
    {
        remainingTime--;
        BattleUI.Instance.UpdateEnemyHealth(this);
        BattleUI.Instance.Log($"{gameObject.name} took 1 damage! Health: {remainingTime}/{burstTime}");
        healthBar.UpdateHealthBar(remainingTime, burstTime);
        if (remainingTime <= 0)
        {
            targetManager.OnTargetDestroyed(this);
            Destroy(gameObject);
        }
    }

    public void AttackPlayer(Shooter player)
    {
        player.TakeDamage(1);
    }
}
