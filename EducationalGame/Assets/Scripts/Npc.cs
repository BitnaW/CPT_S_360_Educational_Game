using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NPC : MonoBehaviour, IInteractable
{
    public NpcDialogue dialogueData; // represents the scriptable object that contains the dialogue associated
    
    // UI Elements 
    public GameObject dialoguePanel;
    public TMP_Text  nameText, dialogueText;
    public Button closeButton; 
    //opt. addition
    //public Image portraitImage;

    protected int dialogueIndex;
    protected bool isTyping, isDialogueActive;
    
    //set dialogue panel to empty until interaction  
    void Start()
    {
        dialoguePanel.SetActive(false); // dialogue panel is closed at the start
        nameText.SetText(""); 
        dialogueText.SetText("");
    }


    public void Interact()
    {
        if (dialogueData == null)
        {
            return;
        }
        if (isDialogueActive) // if you call interact while in convo it will fill in complete line
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    public bool CanInteract()
    {
        return true; //changed for level 2, so you can access dialogue wherever
    }

    public void StartDialogue() //changed to public to allow interact to call it
    {
        isDialogueActive = true;
        dialogueIndex = 0; // starts the dialogue index all the way back to the beginning
        nameText.SetText(dialogueData.npcName);
        dialoguePanel.SetActive(true); // opens the dialogue panel
        StartCoroutine(TypeLine()); // starts typing a line

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(EndDialogue);
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

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        if (dialoguePanel)
        {
            dialoguePanel.SetActive(false);
        }
            
    }
    
    
}
