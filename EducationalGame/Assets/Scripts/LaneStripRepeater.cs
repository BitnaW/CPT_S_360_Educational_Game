using UnityEngine;

// creates a repeating stip for the level 4 minigame
[RequireComponent(typeof(SpriteRenderer))]
public class LaneStripRepeater : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private Camera targetCamera;
    [SerializeField] private float yPosition = 0f;
    [SerializeField] private float zPosition = 0f;
    [SerializeField] private float extraWidth = 6f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        if (followTarget == null || targetCamera == null)
        {
            return;
        }

        float visibleWidth = targetCamera.orthographicSize * 2f * targetCamera.aspect;
        float tiledWidth = visibleWidth + extraWidth;

        // for spriterenderer in tiled draw mode, size controls repeated area
        Vector2 size = spriteRenderer.size;
        size.x = tiledWidth;
        spriteRenderer.size = size;

        transform.position = new Vector3(followTarget.position.x, yPosition, zPosition);
    }
}