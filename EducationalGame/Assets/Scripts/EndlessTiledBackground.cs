using System.Collections.Generic;
using UnityEngine;

// spawns/recycles background tiles around the player
public class EndlessTiledBackground : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform followTarget;

    // how many tiles to keep around center in each direction
    [SerializeField] private int halfTilesX = 8;
    [SerializeField] private int halfTilesY = 4;

    // if <= 0, script tries to read from tile prefab sprite bounds
    [SerializeField] private float tileWidth = 0f;
    [SerializeField] private float tileHeight = 0f;

    // parallax multipliers (1 = same speed as target, <1 = slower/farther away)
    [SerializeField] private float parallaxX = 0.5f;
    [SerializeField] private float parallaxY = 0.1f;

    [SerializeField] private float backgroundZ = 10f;

    private readonly Dictionary<Vector2Int, GameObject> activeTiles = new Dictionary<Vector2Int, GameObject>();
    private readonly Queue<GameObject> pooledTiles = new Queue<GameObject>();

    private void Awake()
    {
        if (tilePrefab == null)
        {
            Debug.LogError("EndlessTiledBackground: tilePrefab is not assigned.", this);
            enabled = false;
            return;
        }

        if (followTarget == null && Camera.main != null)
        {
            followTarget = Camera.main.transform;
        }

        if (tileWidth <= 0f || tileHeight <= 0f)
        {
            SpriteRenderer spriteRenderer = tilePrefab.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && spriteRenderer.sprite != null)
            {
                Vector2 size = spriteRenderer.sprite.bounds.size;
                if (tileWidth <= 0f)
                {
                    tileWidth = size.x * tilePrefab.transform.localScale.x;
                }

                if (tileHeight <= 0f)
                {
                    tileHeight = size.y * tilePrefab.transform.localScale.y;
                }
            }
        }

        if (tileWidth <= 0f || tileHeight <= 0f)
        {
            Debug.LogError("EndlessTiledBackground: tileWidth/tileHeight must be > 0.", this);
            enabled = false;
        }
    }

    private void LateUpdate()
    {
        if (followTarget == null)
        {
            return;
        }

        float px = followTarget.position.x * parallaxX;
        float py = followTarget.position.y * parallaxY;

        int centerX = Mathf.FloorToInt(px / tileWidth);
        int centerY = Mathf.FloorToInt(py / tileHeight);

        HashSet<Vector2Int> needed = new HashSet<Vector2Int>();

        for (int y = centerY - halfTilesY; y <= centerY + halfTilesY; y++)
        {
            for (int x = centerX - halfTilesX; x <= centerX + halfTilesX; x++)
            {
                Vector2Int cell = new Vector2Int(x, y);
                needed.Add(cell);

                if (!activeTiles.ContainsKey(cell))
                {
                    GameObject tile = GetTile();
                    tile.transform.position = new Vector3(x * tileWidth, y * tileHeight, backgroundZ);
                    tile.name = $"BG_{x}_{y}";
                    activeTiles[cell] = tile;
                }
            }
        }

        // recycle tiles no longer needed
        List<Vector2Int> toRemove = new List<Vector2Int>();
        foreach (KeyValuePair<Vector2Int, GameObject> pair in activeTiles)
        {
            if (!needed.Contains(pair.Key))
            {
                ReturnTile(pair.Value);
                toRemove.Add(pair.Key);
            }
        }

        foreach (Vector2Int key in toRemove)
        {
            activeTiles.Remove(key);
        }
    }

    private GameObject GetTile()
    {
        if (pooledTiles.Count > 0)
        {
            GameObject pooled = pooledTiles.Dequeue();
            pooled.SetActive(true);
            return pooled;
        }

        GameObject created = Instantiate(tilePrefab, transform);
        return created;
    }

    private void ReturnTile(GameObject tile)
    {
        tile.SetActive(false);
        pooledTiles.Enqueue(tile);
    }
}