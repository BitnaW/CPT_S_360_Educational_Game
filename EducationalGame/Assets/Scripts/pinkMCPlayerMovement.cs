using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class pinkMCPlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private bool isFrozen = false;

    [Header("Freeze UI")]
    public Image freezeImage;   
    public Sprite[] highPenaltyFreezeFrames; //hard coded animation  
    public Sprite[] midPenaltyFreezeFrames;

    public float frameRate = 0.15f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (freezeImage != null)
            freezeImage.color = new Color(1, 1, 1, 0); // hidden at start
    }

    void Update()
    {
        if (!isFrozen)
            rb.linearVelocity = moveInput * moveSpeed;
        else
            rb.linearVelocity = Vector2.zero;
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);
        if (context.canceled)
            animator.SetBool("isWalking", false);
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

    public void FreezeForSeconds(float duration, Sprite[] frames)
    {
        StartCoroutine(FreezeCoroutine(duration, frames));
    }
    private IEnumerator FreezeCoroutine(float duration, Sprite[] frames)
{
    isFrozen = true;
    animator.SetBool("isWalking", false);
    if (freezeImage != null)
    {
        freezeImage.color = new Color(1, 1, 1, 1);
        StartCoroutine(PlayFreezeAnimation(duration, frames));
    }
    yield return new WaitForSeconds(duration);
    if (freezeImage != null)
        freezeImage.color = new Color(1, 1, 1, 0);
    isFrozen = false;
}

private IEnumerator PlayFreezeAnimation(float duration, Sprite[] frames)
{
    float elapsed = 0f;
    int frameIndex = 0;
    while (elapsed < duration)
    {
        if (frames.Length > 0)
            freezeImage.sprite = frames[frameIndex % frames.Length];
        frameIndex++;
        elapsed += frameRate;
        yield return new WaitForSeconds(frameRate);
    }
}

}