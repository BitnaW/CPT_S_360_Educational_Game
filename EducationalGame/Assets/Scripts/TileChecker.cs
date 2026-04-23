using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class TileChecker : MonoBehaviour
{
    [Header("Tilemaps")]
    public Tilemap defaultTilemap;
    public Tilemap noPenaltyTilemap;
    public Tilemap midPenaltyTilemap;
    public Tilemap highPenaltyTilemap;

    [Header("Penalty Values (seconds deducted)")]
    public float midPenaltyTime = 5f;
    public float highPenaltyTime = 10f;

    private LevelTimer levelTimer;
    private Vector3Int lastCheckedCell;
    private pinkMCPlayerMovement playerMovement;

    void Start()
    {
        levelTimer = FindObjectOfType<LevelTimer>();
        playerMovement = FindObjectOfType<pinkMCPlayerMovement>();
    }

    void Update()
    {
        Vector3Int gridPos = noPenaltyTilemap.WorldToCell(transform.position);
        if (gridPos != lastCheckedCell)
        {
            lastCheckedCell = gridPos;
            CheckCurrentTile(gridPos);
        }
    }

    void CheckCurrentTile(Vector3Int gridPos)
    {
        if (defaultTilemap.HasTile(gridPos))
            defaultTilemap.SetTile(gridPos, null);

        if (highPenaltyTilemap.HasTile(gridPos))
        {
            levelTimer.DeductTime(highPenaltyTime);
            playerMovement.FreezeForSeconds(highPenaltyTime, playerMovement.highPenaltyFreezeFrames);
        }
        else if (midPenaltyTilemap.HasTile(gridPos))
        {
            levelTimer.DeductTime(midPenaltyTime);
            playerMovement.FreezeForSeconds(midPenaltyTime, playerMovement.midPenaltyFreezeFrames);
        }
    }
}