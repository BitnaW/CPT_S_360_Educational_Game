using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void onPlayButton()
    {
        // the (1) is the order of levels/scenes
        // make sure they're in the right order in the build settings 
        // menu should be 0
        SceneManager.LoadScene(1);
    }
    
    public void onQuitButton()
    {
        Application.Quit();
    }
}
