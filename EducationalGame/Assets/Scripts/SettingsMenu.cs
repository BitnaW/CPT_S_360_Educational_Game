using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject settingsGearButton; // gear icon/button in HUD

    // both can point to menuScene
    [SerializeField] private string mainMenuSceneName = "menuScene";
    [SerializeField] private string levelSelectSceneName = "menuScene";

    [SerializeField] private bool pauseGameplay = true;
    [SerializeField] private bool allowEscapeToggle = true;

    private bool isOpen;
    public bool IsOpen => isOpen;

    private void Start()
    {
        CloseSettingsImmediate();
    }

    private void Update()
    {
        if (!allowEscapeToggle)
        {
            return;
        }

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ToggleSettings();
        }
    }

    public void ToggleSettings()
    {
        if (isOpen)
        {
            CloseSettings();
        }
        else
        {
            OpenSettings();
        }
    }

    public void OnSettingsButton()
    {
        ToggleSettings();
    }

    public void OnCloseSettingsButton()
    {
        CloseSettings();
    }

    public void OnMainMenuButton()
    {
        Time.timeScale = 1f;
        LoadSceneSafe(mainMenuSceneName);
    }

    public void OnLevelSelectButton()
    {
        Time.timeScale = 1f;
        LoadSceneSafe(levelSelectSceneName);
    }

    public void OnQuitButton()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void OnPlayAgainButton()
    {
        Time.timeScale = 1f;

        string lastScene = SceneHistory.GetLastScene();
        if (!string.IsNullOrWhiteSpace(lastScene) && Application.CanStreamedLevelBeLoaded(lastScene))
        {
            SceneManager.LoadScene(lastScene);
            return;
        }

        Debug.LogError("No previous scene recorded for Play Again.");
    }

    private void OpenSettings()
    {
        if (settingsPanel == null)
        {
            Debug.LogError("SettingsMenu: settingsPanel is not assigned.", this);
            return;
        }

        settingsPanel.SetActive(true);

        if (settingsGearButton != null)
        {
            settingsGearButton.SetActive(false);
        }

        isOpen = true;

        if (pauseGameplay)
        {
            Time.timeScale = 0f;
        }
    }

    private void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        if (settingsGearButton != null)
        {
            settingsGearButton.SetActive(true);
        }

        isOpen = false;

        if (pauseGameplay)
        {
            Time.timeScale = 1f;
        }
    }

    private void CloseSettingsImmediate()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        if (settingsGearButton != null)
        {
            settingsGearButton.SetActive(true);
        }

        isOpen = false;
        Time.timeScale = 1f;
    }

    private void LoadSceneSafe(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError("Scene name is empty.");
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError($"Scene '{sceneName}' is not available. Check Build Settings.");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}
