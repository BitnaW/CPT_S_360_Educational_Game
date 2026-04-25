using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStateCoordinator : MonoBehaviour
{
    [SerializeField] private GameObject startScreenPanel;
    [SerializeField] private GameObject stageSelectPanel;
    [SerializeField] private GameObject optionsPanel;

    // used to switch between menu screens
    private enum MenuScreen
    {
        Start,
        StageSelect,
        Options
    }

    // history stack for back button
    private readonly Stack<MenuScreen> history = new Stack<MenuScreen>();
    private MenuScreen currentScreen = MenuScreen.Start;

    private void Start()
    {
        // force menu to start screen on scene load
        SwitchTo(MenuScreen.Start, false);
    }

    public void ShowStart()
    {
        SwitchTo(MenuScreen.Start, true);
    }

    public void ShowStageSelect()
    {
        SwitchTo(MenuScreen.StageSelect, true);
    }

    public void ShowOptions()
    {
        SwitchTo(MenuScreen.Options, true);
    }

    public void Back()
    {
        // no previous screen saved, do nothing
        if (history.Count == 0)
        {
            return;
        }

        SwitchTo(history.Pop(), false);
    }

    public void LoadStage(string sceneName)
    {
        // prevent trying to load empty scene names
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError("Stage scene name is empty.");
            return;
        }

        // make sure scene exists in build settings
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError($"Scene '{sceneName}' is not available. Check Build Settings.");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        // only works in build, not in editor play mode
        Application.Quit();
    }

    private void SwitchTo(MenuScreen nextScreen, bool recordHistory)
    {
        // save current screen so back button can return to it
        if (recordHistory && nextScreen != currentScreen)
        {
            history.Push(currentScreen);
        }

        currentScreen = nextScreen;

        // only one panel should be visible at a time
        startScreenPanel.SetActive(currentScreen == MenuScreen.Start);
        stageSelectPanel.SetActive(currentScreen == MenuScreen.StageSelect);
        optionsPanel.SetActive(currentScreen == MenuScreen.Options);
    }
}