using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    [SerializeField] HealthBar playerHealthBar;
    [SerializeField] private int playerHealth;
    [SerializeField] private int maxHealth = 20;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = maxHealth;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Process")) // checks for damage from enemy targets if you get the order wrong
        {
            TakeDamage(2);
        }
    }

    public void TakeDamage(int amount)
    {
        playerHealth = Mathf.Max(0, playerHealth - amount);
        playerHealthBar.UpdateHealthBar(playerHealth, maxHealth);

        if (playerHealth <= 0)
        {
            GameOver(); // TODO: make a game over scene/overlay
        }
    }

    private void GameOver()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
