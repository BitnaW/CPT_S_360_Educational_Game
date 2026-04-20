using UnityEngine;
//script to allow npc dialogue trigger when in a certain area
public class MapEntryTrigger : MonoBehaviour
{
    public NPC npc;
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            npc.StartDialogue();
        }
    }
}