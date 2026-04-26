using UnityEngine;

// keeps spawn points a fixed distance ahead of player
public class SpawnFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private float xOffsetAhead = 0f;

    private void LateUpdate()
    {
        if (followTarget == null)
        {
            return;
        }

        Vector3 position = transform.position;
        position.x = followTarget.position.x + xOffsetAhead;
        transform.position = position;
    }
}