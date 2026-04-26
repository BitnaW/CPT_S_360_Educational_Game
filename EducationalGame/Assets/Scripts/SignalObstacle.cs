using UnityEngine;

public enum SignalType
{
    SigKill,
    SigSegv,
    SigChild
}

public class SignalObstacle : MonoBehaviour
{
    [SerializeField] private SignalType signalKind = SignalType.SigSegv;
    [SerializeField] private int damageAmount = 2;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float despawnX = -30f;

    public SignalType SignalKind => signalKind;
    public int DamageAmount => damageAmount;

    private void Update()
    {
        // move toward player from right to left
        transform.position += Vector3.left * (moveSpeed * Time.deltaTime);

        if (transform.position.x < despawnX)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        Level4RunnerPlayer player = collision.GetComponent<Level4RunnerPlayer>();
        if (player == null)
        {
            player = collision.GetComponentInParent<Level4RunnerPlayer>();
        }

        if (player != null)
        {
            player.ApplySignal(this);
        }

        Destroy(gameObject);
    }
}