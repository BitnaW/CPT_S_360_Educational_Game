using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Level4RunnerPlayer : MonoBehaviour
{
    // movement
    [SerializeField] private float forwardSpeed = 4f;
    [SerializeField] private float laneOffset = 2f;
    [SerializeField] private float laneChangeSpeed = 12f;
    [SerializeField] private int startingLane = 0; // -1 = bottom, 0 = middle, 1 = top
    [SerializeField] private float laneCenterY = 0f;

    // health
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private HealthBar playerHealthBar;

    // fail scene
    [SerializeField] private string failSceneName = "menuScene";
    [SerializeField] private float gameOverDelay = 1f;

    private int currentHealth;
    private int currentLane;
    private float targetLaneY;
    private bool canMove = true;
    private bool isDying;

    private void Start()
    {
        currentHealth = maxHealth;

        currentLane = Mathf.Clamp(startingLane, -1, 1);
        targetLaneY = laneCenterY + (currentLane * laneOffset);

        Vector3 position = transform.position;
        position.y = targetLaneY;
        transform.position = position;

        UpdateHealthUI();
    }

    private void Update()
    {
        if (!canMove)
        {
            return;
        }

        transform.position += Vector3.right * (forwardSpeed * Time.deltaTime);

        Vector3 position = transform.position;
        position.y = Mathf.Lerp(position.y, targetLaneY, laneChangeSpeed * Time.deltaTime);
        transform.position = position;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!canMove || !context.performed)
        {
            return;
        }

        Vector2 input = context.ReadValue<Vector2>();

        if (input.y > 0.1f)
        {
            SetLane(currentLane + 1);
        }
        else if (input.y < -0.1f)
        {
            SetLane(currentLane - 1);
        }
    }

    private void SetLane(int lane)
    {
        currentLane = Mathf.Clamp(lane, -1, 1);
        targetLaneY = laneCenterY + (currentLane * laneOffset);
    }

    public void ApplySignal(SignalObstacle signal)
    {
        switch (signal.SignalKind)
        {
            case SignalType.SigKill:
                KillPlayer();
                break;

            case SignalType.SigSegv:
                TakeDamage(signal.DamageAmount);
                break;

            case SignalType.SigChild:
                break;
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDying)
        {
            return;
        }

        currentHealth = Mathf.Max(0, currentHealth - Mathf.Max(0, amount));
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            KillPlayer();
        }
    }

    public void OnReachedGoal()
    {
        canMove = false;
    }

    private void KillPlayer()
    {
        if (isDying)
        {
            return;
        }

        isDying = true;
        canMove = false;
        StartCoroutine(LoadGameOverAfterDelay());
    }

    private IEnumerator LoadGameOverAfterDelay()
    {
        yield return new WaitForSeconds(gameOverDelay);

        Time.timeScale = 1f;

        // remember where the player died so Play Again can return there
        SceneHistory.RememberCurrentScene(SceneManager.GetActiveScene().name);

        if (!string.IsNullOrWhiteSpace(failSceneName) && Application.CanStreamedLevelBeLoaded(failSceneName))
        {
            SceneManager.LoadScene(failSceneName);
        }
        else
        {
            Debug.Log("Player died. Assign a valid failSceneName if you want scene reload.");
        }
    }

    private void UpdateHealthUI()
    {
        if (playerHealthBar != null)
        {
            playerHealthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
    }
}