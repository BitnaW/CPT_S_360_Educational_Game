using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneHistory
{
    private const string LastSceneKey = "LastGameplayScene";
    private static readonly string[] NonReplayableScenes =
    {
        "menuScene",
        "Fail",
        "Victory"
    };

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void RegisterSceneTracking()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public static void RememberCurrentScene(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            return;
        }

        PlayerPrefs.SetString(LastSceneKey, sceneName);
        PlayerPrefs.Save();
    }

    public static string GetLastScene()
    {
        return PlayerPrefs.GetString(LastSceneKey, string.Empty);
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Array.Exists(NonReplayableScenes, sceneName => sceneName == scene.name))
        {
            return;
        }

        RememberCurrentScene(scene.name);
    }
}
