using System;
using Unity.Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    
    [SerializeField] Animator animator;
    
    [SerializeField] PolygonCollider2D mapBoundary;  // this is the boundary you're transitioning to
    [SerializeField] Transform spawnPoint; // represents the space you have to land on to teleport
    private CinemachineConfiner2D confiner;
    
    private void Awake()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FadeTransition(collision.gameObject);
        }
    }
    
    async void FadeTransition(GameObject player)
    {
        await screenFader.instance.FadeOut();
       
        confiner.BoundingShape2D = mapBoundary; 
        MovePlayer(player);
        
        await screenFader.instance.FadeIn();
    }

    // method to be able to move player to a spawn point 
    private void MovePlayer(GameObject player)
    {
        player.transform.position = spawnPoint.position;
    }

   
}
