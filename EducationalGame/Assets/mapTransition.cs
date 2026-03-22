using System;
using Unity.Cinemachine;
using UnityEngine;

public class mapTransition : MonoBehaviour
{
    
    [SerializeField] Animator animator;

    // this is the boundary you're transitioning to
    [SerializeField] PolygonCollider2D mapBoundary;
    [SerializeField] Transform spawnPoint;
    CinemachineConfiner2D confiner;
    
    private void Awake()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FadeTransition(collision.gameObject);
            //collision.transform.position = spawnPoint.position;
            //confiner.BoundingShape2D = mapBoundary; 
        }
    }
    
    async void FadeTransition(GameObject player)
    {
        await screenFader.instance.FadeOut();
       
        confiner.BoundingShape2D = mapBoundary; 
        player.transform.position = spawnPoint.position;
        
        await screenFader.instance.FadeIn();
    }

   
}
