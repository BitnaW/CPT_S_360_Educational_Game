using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTransition : TransitionPoint, IInteractable
{
    public NpcDialogue dialogueData; 
    // UI Elements 
    public GameObject dialoguePanel;
    public TMP_Text  nameText, dialogueText;
    private int dialogueIndex;
    protected bool isTyping, isDialogueActive;

    // will re-trigger dialogue if you enter then exit then enter again
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Interact();
        }
    }
    
    public void Interact()
    {
        if (dialogueData == null)
        {
            return;
        }
        if (isDialogueActive) 
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        EndDialogue();
    }
    
    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
    }
    
    public bool CanInteract()
    {
        return !isDialogueActive;
    }
    
    protected void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0; // starts the dialogue index all the way back to the beginning
        nameText.SetText(dialogueData.npcName);
        dialoguePanel.SetActive(true); // opens the dialogue panel
        StartCoroutine(TypeLine()); // starts typing a line
    }

    protected virtual void NextLine()
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
            StartCoroutine(TypeLine());
        }
        else // if no more dialogue it ends the convo
        {
            EndDialogue();
            levelLoader.LoadNextLevel();
        }
    }

    protected IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");
        
        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.dialogueSpeed);
        }
        
        isTyping = false;

        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }
   
}
