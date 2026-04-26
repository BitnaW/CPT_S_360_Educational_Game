using UnityEngine;
using UnityEngine.SceneManagement;

public static class IntroFlow
{
    private const string IntroSeenPrefKey = "IntroSeen";
    private static bool openStageSelectOnMenuLoad;

    public static bool TryStartIntro(string introSceneName)
    {
        MarkIntroSeen();
        openStageSelectOnMenuLoad = true;
        SceneManager.LoadScene(introSceneName);
        return true;
    }

    public static void ReturnToMenuAndOpenStageSelect(string menuSceneName)
    {
        openStageSelectOnMenuLoad = true;
        SceneManager.LoadScene(menuSceneName);
    }

    public static bool ConsumeOpenStageSelectOnMenuLoad()
    {
        bool shouldOpen = openStageSelectOnMenuLoad;
        openStageSelectOnMenuLoad = false;
        return shouldOpen;
    }

    public static bool HasSeenIntro()
    {
        return PlayerPrefs.GetInt(IntroSeenPrefKey, 0) == 1;
    }

    public static void ResetIntroSeen()
    {
        PlayerPrefs.DeleteKey(IntroSeenPrefKey);
        PlayerPrefs.Save();
        openStageSelectOnMenuLoad = false;
    }

    private static void MarkIntroSeen()
    {
        PlayerPrefs.SetInt(IntroSeenPrefKey, 1);
        PlayerPrefs.Save();
    }
}
