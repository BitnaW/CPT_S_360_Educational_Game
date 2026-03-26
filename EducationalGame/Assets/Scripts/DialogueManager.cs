using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // for when we have UI
    // public Text nameText;
    // public Text dialogueText;
    
    public Queue<string> dialogue =  new ();
    private Npc speakingTo; 
    
    public void StartDialogue(Npc npc)
    {
        speakingTo = npc;

        if (speakingTo.sentences.Length == 0)
        {
            return;
        }
        // load all the npc dialogue into queue
        if (!speakingTo.SpokenTo)
        {
            foreach (string sentence in speakingTo.sentences)
            {
                dialogue.Enqueue(sentence);
            }
            speakingTo.SpokenTo = true;
        }
        
        SpeakLine();
        
    }

    public void SpeakLine()
    {
        if (dialogue.Count == 0)
        {
            endDialogue();
            return;
        }
        string line = dialogue.Dequeue();

        if (speakingTo.hasCyclicalDialogue)
        {
            Debug.Log(speakingTo.characterName + " says: " + line);
            dialogue.Enqueue(line);
        }
        else
        {
            Debug.Log(speakingTo.characterName + " says: " + line);
        }
        
    }
    
    public void endDialogue()
    {
        if (speakingTo.lastSentence != string.Empty)
        {
            Debug.Log(speakingTo.characterName + " says: " + speakingTo.lastSentence);
        }
        else
        {
            Debug.Log(speakingTo.characterName + " Has nothing else to say.");
        }
    }
   
}
