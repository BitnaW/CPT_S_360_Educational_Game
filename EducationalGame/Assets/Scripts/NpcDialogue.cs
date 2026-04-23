using UnityEngine;

[CreateAssetMenu(fileName = "NewNpcDialogue", menuName = "Npc Dialogue")]
public class NpcDialogue : ScriptableObject
{
    public string npcName;
    public string[] dialogueLines;
    public bool[] autoProgressLines;
    public float dialogueSpeed = 0.05f;
    public float autoProgressDelay = 1.5f;


    //new additions, referenced GameCodeLibrary, might remove 
    public AudioClip voiceSound;
    public float voicePitch = 1f;
}
