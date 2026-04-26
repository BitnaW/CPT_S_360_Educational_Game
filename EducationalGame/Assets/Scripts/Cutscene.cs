using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class Cutscene : MonoBehaviour
    {
        [SerializeField] private LevelLoader levelLoader;
        public NpcDialogue dialogueData;
        public GameObject blackScreen;
        public TMP_Text dialogueText;
        private int dialogueIndex;
        private bool isTyping;

        private void Start()
        {
            blackScreen.SetActive(true);
            StartCoroutine(TypeLine());
        }

        private void Update()
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                HandleInput();
            }
        }

        private void HandleInput()
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
                isTyping = false;
            }
            else
            {
                dialogueIndex++;
                if (dialogueIndex < dialogueData.dialogueLines.Length)
                {
                    StartCoroutine(TypeLine());
                }

                else
                {
                    levelLoader.LoadNextLevel();
                }
            }
        }

        private IEnumerator TypeLine()
        {
            isTyping = true;
            dialogueText.SetText("");
            foreach (char character in dialogueData.dialogueLines[dialogueIndex])
            {
                dialogueText.text += character;
                yield return new WaitForSeconds(dialogueData.dialogueSpeed);
            }

            isTyping = false;

            if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
            {
                yield return new WaitForSeconds(dialogueData.autoProgressDelay);
                dialogueIndex++;
                if (dialogueIndex < dialogueData.dialogueLines.Length)
                {
                    StartCoroutine(TypeLine());
                }
                else
                {
                    levelLoader.LoadNextLevel();
                }
                   
            }
        }
        
    }
}