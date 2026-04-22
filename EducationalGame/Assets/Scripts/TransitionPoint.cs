using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// Adding this to a game object with a collider allows to for it to trigger a scene change
// I got rid of the movement on the same scene for now, since we're really only moving between scenes at the moment
public class TransitionPoint : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader; // make sure each level you want to transition to/from has a level loader 
    // if this isn't working make sure to check that you have all scenes loaded in the build settings
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            levelLoader.LoadNextLevel();
        }
    }
}
