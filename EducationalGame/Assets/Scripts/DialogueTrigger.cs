using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    public Npc character;
    private bool inRange;
    private DialogueManager mgr;

    private void Start()
    {
        mgr = FindFirstObjectByType<DialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    private void Update()
    {
        if (inRange && Keyboard.current.rKey.wasReleasedThisFrame)
        {
            mgr.StartDialogue(character);
        }
    }
    
}
