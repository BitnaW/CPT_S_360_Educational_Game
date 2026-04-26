using UnityEngine;
using UnityEngine.InputSystem;

public class IntroSceneController : MonoBehaviour
{
    [SerializeField] private string menuSceneName = "menuScene";

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SkipIntro();
        }
    }

    public void OnSkipButton()
    {
        SkipIntro();
    }

    public void OnIntroFinished()
    {
        SkipIntro();
    }

    private void SkipIntro()
    {
        Time.timeScale = 1f;
        IntroFlow.ReturnToMenuAndOpenStageSelect(menuSceneName);
    }
}
