using System;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed = 10f;
    [SerializeField] private float lifetime = 2f;
    GameObject[] targetPrefabs;
    
    void Start()
    {
        // invoke allows the setting of an expiration time for bullet if it doesn't hit anything
        Invoke(nameof(DestroyBullet),  lifetime);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;
    }
    
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) // if the collision isn't with the player
        {
            Debug.Log("Bullet Collision!");
            CancelInvoke(nameof(DestroyBullet)); // cancel the delete timer and delete it now
            DestroyBullet();
        }
        
        
    }
}