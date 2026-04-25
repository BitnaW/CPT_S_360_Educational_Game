using UnityEngine;

public static class SceneHistory
{
    private const string LastSceneKey = "LastGameplayScene";

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
}