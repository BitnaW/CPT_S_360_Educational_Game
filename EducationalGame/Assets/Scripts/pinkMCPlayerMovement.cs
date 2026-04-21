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
    public Sprite[] freezeFrames; //hard coded animation  
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

    public void FreezeForSeconds(float duration)
    {
        StartCoroutine(FreezeCoroutine(duration));
    }

    private IEnumerator FreezeCoroutine(float duration)
    {
        isFrozen = true;
        animator.SetBool("isWalking", false);
        if (freezeImage != null)
        {
            freezeImage.color = new Color(1, 1, 1, 1); // show
            StartCoroutine(PlayFreezeAnimation(duration));
        }
        yield return new WaitForSeconds(duration);
        if (freezeImage != null)
            freezeImage.color = new Color(1, 1, 1, 0); // hide
        isFrozen = false;
    }

    private IEnumerator PlayFreezeAnimation(float duration)
    {
        float elapsed = 0f;
        int frameIndex = 0;
        while (elapsed < duration)
        {
            if (freezeFrames.Length > 0)
                freezeImage.sprite = freezeFrames[frameIndex % freezeFrames.Length];
            frameIndex++;
            elapsed += frameRate;
            yield return new WaitForSeconds(frameRate);
        }
    }
}