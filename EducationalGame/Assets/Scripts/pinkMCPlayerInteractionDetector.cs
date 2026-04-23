using UnityEngine;
using UnityEngine.InputSystem;

public class pinkMCPlayerInteractionDetector : MonoBehaviour
{
    private IInteractable inRange = null;
    //public GameObject interactionIcon; not using ts
    void Start()
    {
        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inRange?.Interact(); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable Interactable))
        {
            inRange = Interactable;
            //set icon true if you want to use one 
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //moving away from interactable
    {
        if (collision.TryGetComponent(out IInteractable Interactable) && Interactable == inRange)
        {
            if(!inRange.CanInteract()) return; 
            {
                inRange = null;
            }
            //set icon false if you want to use one 
        }
    }
}
