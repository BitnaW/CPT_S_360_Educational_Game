using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Npc : MonoBehaviour
{
    
    public string[] sentences; // all the dialogue for this character
    public string characterName;
    // if the character is not meant to repeat lines, the last sentence will show as a way of 
    // indicating conversation is done
    public string lastSentence = String.Empty; 
    public bool hasCyclicalDialogue;
    private bool spokenTo;

    public bool SpokenTo
    {
        get { return spokenTo; }
        set { spokenTo = value; }
    }
    
}
