using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] public Transform spawnPoint;
    [SerializeField] private int health = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        //health = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (--health > 0)
        {
            health--;
        }
        else
        {
            Debug.Log("Target destroyed!");
            Destroy(gameObject);
        }
        
    }
}
