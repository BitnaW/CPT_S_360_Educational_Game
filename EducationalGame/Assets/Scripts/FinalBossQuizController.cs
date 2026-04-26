using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalBossQuizController : MonoBehaviour
{
    [Header("Quiz Setup")]
    [SerializeField] private FinalBossQuestionBank questionBank;
    [SerializeField] private int questionsRequiredToWin = 10;
    [SerializeField] private int questionsPerRun = 20;
    [SerializeField] private float delayBetweenQuestionsSeconds = 1.1f;
    [SerializeField] private float delayBeforeSceneChangeSeconds = 1.8f;
    [SerializeField] private string victorySceneName;
    [SerializeField] private string failSceneName;

    [Header("Scene References")]
    [SerializeField] private pinkMCPlayerMovement playerMovement;
    [SerializeField] private HealthBar bossHealthBar;
    [SerializeField] private FinalBossBossVisuals bossVisuals;
    [SerializeField] private SettingsMenu settingsMenu;
    [SerializeField] private GameObject quizPanel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text questionCounterText;
    [SerializeField] private TMP_Text bossHealthText;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private FinalBossAnswerButton[] answerButtons;

    [Header("Dialogue Box")]
    [SerializeField] private bool useDialogueBoxForQuestions = true;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueNameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private string dialogueSpeakerName = "Boss";

    [Header("Behavior")]
    [SerializeField] private bool freezePlayerOnStart = true;
    [SerializeField] private string titleLabel = "FINAL BOSS";

    private readonly List<FinalBossQuestion> selectedQuestions = new List<FinalBossQuestion>();

    private int currentQuestionsRequiredToWin;
    private int currentQuestionsPerRun;
    private int correctAnswers;
    private int answeredQuestions;
    private int currentBossHealth;
    private bool quizFinished;

    private void Start()
    {
        if (settingsMenu == null)
        {
            settingsMenu = FindFirstObjectByType<SettingsMenu>();
        }

        List<FinalBossQuestion> availableQuestions = GetValidQuestions(questionBank.questions);
        currentQuestionsPerRun = Mathf.Clamp(questionsPerRun, 1, availableQuestions.Count);
        currentQuestionsRequiredToWin = Mathf.Clamp(questionsRequiredToWin, 1, currentQuestionsPerRun);
        currentBossHealth = currentQuestionsRequiredToWin;

        SelectQuestions(availableQuestions, currentQuestionsPerRun);

        if (freezePlayerOnStart)
        {
            LockPlayerControls();
        }

        if (quizPanel != null)
        {
            quizPanel.SetActive(true);
        }

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(useDialogueBoxForQuestions);
        }

        if (titleText != null)
        {
            titleText.text = titleLabel;
        }

        HookUpAnswerButtons();
        UpdateBossHealthUi();
        ShowCurrentQuestion();
    }

    private void Update()
    {
        SyncDialogueVisibility();
    }

    private List<FinalBossQuestion> GetValidQuestions(FinalBossQuestion[] questionBankQuestions)
    {
        List<FinalBossQuestion> validQuestions = new List<FinalBossQuestion>();
        if (questionBankQuestions == null)
        {
            return validQuestions;
        }

        for (int index = 0; index < questionBankQuestions.Length; index++)
        {
            FinalBossQuestion question = questionBankQuestions[index];
            if (question == null || string.IsNullOrWhiteSpace(question.prompt))
            {
                continue;
            }

            if (question.correctAnswerIndex < 0 || question.correctAnswerIndex >= answerButtons.Length)
            {
                continue;
            }

            validQuestions.Add(question);
        }

        return validQuestions;
    }

    private void SelectQuestions(List<FinalBossQuestion> availableQuestions, int numberToSelect)
    {
        for (int index = availableQuestions.Count - 1; index > 0; index--)
        {
            int swapIndex = UnityEngine.Random.Range(0, index + 1);
            FinalBossQuestion cachedQuestion = availableQuestions[index];
            availableQuestions[index] = availableQuestions[swapIndex];
            availableQuestions[swapIndex] = cachedQuestion;
        }

        selectedQuestions.Clear();
        for (int index = 0; index < numberToSelect; index++)
        {
            selectedQuestions.Add(availableQuestions[index]);
        }
    }

    private void HookUpAnswerButtons()
    {
        for (int index = 0; index < answerButtons.Length; index++)
        {
            Button button = answerButtons[index].button;
            if (button == null)
            {
                continue;
            }

            int capturedIndex = index;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => SubmitAnswer(capturedIndex));
        }
    }

    private void LockPlayerControls()
    {
        if (playerMovement == null)
        {
            return;
        }

        Rigidbody2D playerBody = playerMovement.GetComponent<Rigidbody2D>();
        if (playerBody != null)
        {
            playerBody.linearVelocity = Vector2.zero;
        }

        playerMovement.enabled = false;
    }

    private void ShowCurrentQuestion()
    {
        if (answeredQuestions >= selectedQuestions.Count)
        {
            LoseBattle();
            return;
        }

        FinalBossQuestion currentQuestion = selectedQuestions[answeredQuestions];

        if (questionCounterText != null)
        {
            questionCounterText.text = $"Question {answeredQuestions + 1} of {currentQuestionsPerRun}";
        }

        if (useDialogueBoxForQuestions)
        {
            if (dialogueNameText != null)
            {
                dialogueNameText.text = dialogueSpeakerName;
            }

            if (dialogueText != null)
            {
                dialogueText.text = currentQuestion.prompt;
            }
        }

        SyncDialogueVisibility();

        if (feedbackText != null)
        {
            feedbackText.text = string.Empty;
        }

        for (int index = 0; index < answerButtons.Length; index++)
        {
            FinalBossAnswerButton answerButton = answerButtons[index];

            if (answerButton.button != null)
            {
                answerButton.button.gameObject.SetActive(true);
                answerButton.button.interactable = true;
            }

            TMP_Text label = answerButton.label;
            if (label == null && answerButton.button != null)
            {
                label = answerButton.button.GetComponentInChildren<TMP_Text>(true);
                answerButtons[index].label = label;
            }
        }

        UpdateProgressText();
    }

    private void SubmitAnswer(int selectedAnswerIndex)
    {
        if (quizFinished || answeredQuestions >= selectedQuestions.Count)
        {
            return;
        }

        FinalBossQuestion currentQuestion = selectedQuestions[answeredQuestions];

        bool isCorrect = selectedAnswerIndex == currentQuestion.correctAnswerIndex;
        answeredQuestions++;

        if (isCorrect)
        {
            correctAnswers++;
            currentBossHealth = Mathf.Max(0, currentBossHealth - 1);
            bool shouldRevealPenguin = currentBossHealth <= 1;

            if (feedbackText != null)
            {
                feedbackText.text = "Correct! The boss takes damage.";
            }

            if (bossVisuals != null)
            {
                bossVisuals.PlayDamageReaction(shouldRevealPenguin);
            }

            UpdateBossHealthUi();
        }
        else if (feedbackText != null)
        {
            feedbackText.text = $"Wrong! You take damage.";
        }

        for (int index = 0; index < answerButtons.Length; index++)
        {
            if (answerButtons[index].button != null)
            {
                answerButtons[index].button.interactable = false;
            }
        }

        UpdateProgressText();

        if (correctAnswers >= currentQuestionsRequiredToWin)
        {
            StartCoroutine(FinishAfterDelay(victorySceneName));
            return;
        }

        int remainingQuestions = currentQuestionsPerRun - answeredQuestions;
        if (remainingQuestions <= 0 || correctAnswers + remainingQuestions < currentQuestionsRequiredToWin)
        {
            StartCoroutine(FinishAfterDelay(failSceneName));
            return;
        }

        StartCoroutine(AdvanceAfterDelay());
    }

    private IEnumerator AdvanceAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delayBetweenQuestionsSeconds);
        ShowCurrentQuestion();
    }

    private IEnumerator FinishAfterDelay(string sceneToLoad)
    {
        quizFinished = true;
        yield return new WaitForSecondsRealtime(delayBeforeSceneChangeSeconds);

        if (!Application.CanStreamedLevelBeLoaded(sceneToLoad))
        {
            Debug.LogError($"FinalBossQuizController: Scene '{sceneToLoad}' is not available.", this);
            yield break;
        }

        SceneManager.LoadScene(sceneToLoad);
    }

    private void UpdateBossHealthUi()
    {
        if (bossHealthBar != null)
        {
            bossHealthBar.UpdateHealthBar(currentBossHealth, currentQuestionsRequiredToWin);
        }

        if (bossHealthText != null)
        {
            bossHealthText.text = $"{currentBossHealth}/{currentQuestionsRequiredToWin}";
        }
    }

    private void UpdateProgressText()
    {
        if (progressText == null)
        {
            return;
        }

        progressText.text = $"Correct: {correctAnswers}/{currentQuestionsRequiredToWin}";
    }

    private void LoseBattle()
    {
        StartCoroutine(FinishAfterDelay(failSceneName));
    }

    private void SyncDialogueVisibility()
    {
        if (!useDialogueBoxForQuestions || dialoguePanel == null)
        {
            return;
        }

        bool shouldShowDialogue = settingsMenu == null || !settingsMenu.IsOpen;
        if (dialoguePanel.activeSelf != shouldShowDialogue)
        {
            dialoguePanel.SetActive(shouldShowDialogue);
        }
    }
}

[Serializable]
public class FinalBossAnswerButton
{
    public Button button;
    public TMP_Text label;
}
