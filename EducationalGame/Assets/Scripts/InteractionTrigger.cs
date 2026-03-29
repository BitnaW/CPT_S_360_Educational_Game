using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionTrigger : MonoBehaviour
{
    private IInteractable inRange = null; // determines if an entity is within interacting range 
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed) // allows for use of the unity interaction system
        {
            inRange?.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            inRange = interactable;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == inRange)
        {
            inRange = null;
        }
    }
}
