using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private static readonly int Start = Animator.StringToHash("Start");
    [SerializeField] private GameObject triggerPoint;
    //[SerializeField] private string enterScene; // unsure if we need this 
    [SerializeField] private string exitScene;
    
    public Animator transition;
    public float transitionTime = 1f; // feel free to adjust in editor if this is too fast/slow
    
    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName(exitScene))
        {
            StartCoroutine(LoadLevel(exitScene));
        }
    }

    IEnumerator LoadLevel(string levelName)
    {
        // play transition animation, wait, load scene
        transition.SetTrigger(Start);
        yield return new WaitForSeconds(transitionTime); // pauses coroutine 
        SceneManager.LoadScene(levelName);
    }
}
