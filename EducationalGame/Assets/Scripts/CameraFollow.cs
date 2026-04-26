using UnityEngine;

// For the lane switch on the game 4 minigame
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed = 6f;
    [SerializeField] private Vector3 offset = new Vector3(2f, 0f, -10f);
    [SerializeField] private bool followY = false;

    private float lockedY;

    private void Start()
    {
        lockedY = transform.position.y;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        float targetY = followY ? target.position.y + offset.y : lockedY;
        Vector3 desired = new Vector3(target.position.x + offset.x, targetY, offset.z);

        transform.position = Vector3.Lerp(transform.position, desired, followSpeed * Time.deltaTime);
    }
}