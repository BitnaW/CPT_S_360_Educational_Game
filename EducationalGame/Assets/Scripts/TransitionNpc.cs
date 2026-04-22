using UnityEngine;

[System.Serializable]
public class TransitionNpc : NPC {
    
    [SerializeField] private LevelLoader levelLoader;
    
    protected override void NextLine()
    {
        if (isTyping) // lets you skip ahead to the full line
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping =  false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            // if there's another line displays it
            StartCoroutine(base.TypeLine());
        }
        else // if no more dialogue it ends the convo
        {
            levelLoader.LoadNextLevel();
            EndDialogue();
        }
    }
    
}
