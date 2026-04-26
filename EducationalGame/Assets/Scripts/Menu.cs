using UnityEngine;

public class Menu : MonoBehaviour
{
    // menu coordinator that handles panel switching, scene loading
    [SerializeField] private MenuStateCoordinator menuStateCoordinator;

    // called by the play button, opens the stage select panel
    public void onPlayButton()
    {
        menuStateCoordinator.HandlePlayButton();
    }

    // called by options button, opens the options panel
    public void onOptionsButton()
    {
        menuStateCoordinator.ShowOptions();
    }

    // called by back buttons, returns to previous menu screen
    public void onBackButton()
    {
        menuStateCoordinator.Back();
    }

    // called by quit button, closes app in build
    public void onQuitButton()
    {
        menuStateCoordinator.QuitGame();
    }

    // called by stage buttons, loads whatever scene name the button passes in
    public void onSelectStageByName(string sceneName)
    {
        menuStateCoordinator.LoadStage(sceneName);
    }
}
