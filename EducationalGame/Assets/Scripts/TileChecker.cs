using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
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

    [Header("Popup UI")]
    public GameObject penaltyPopup;   // drag your panel here
    public TMP_Text popupText;        // drag the text here
    public float popupDuration = 2f;  // how long it shows

    private LevelTimer levelTimer;
    private Vector3Int lastCheckedCell;
    private Coroutine popupCoroutine;

    void Start()
    {
        levelTimer = FindObjectOfType<LevelTimer>();
        penaltyPopup.SetActive(false);
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
        {
            defaultTilemap.SetTile(gridPos, null);
        }

        if (highPenaltyTilemap.HasTile(gridPos))
        {
            levelTimer.DeductTime(highPenaltyTime);
            ShowPopup($"Page Fault! -{highPenaltyTime}s");
        }
        else if (midPenaltyTilemap.HasTile(gridPos))
        {
            levelTimer.DeductTime(midPenaltyTime);
            ShowPopup($"Page Fault! -{midPenaltyTime}s");
        }
    }

    void ShowPopup(string message)
    {
        popupText.SetText(message);
        penaltyPopup.SetActive(true);

        // if a popup is already showing, restart its timer
        if (popupCoroutine != null) StopCoroutine(popupCoroutine);
        popupCoroutine = StartCoroutine(HidePopupAfterDelay());
    }

    IEnumerator HidePopupAfterDelay()
    {
        yield return new WaitForSeconds(popupDuration);
        penaltyPopup.SetActive(false);
    }
}