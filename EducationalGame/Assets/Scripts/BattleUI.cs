using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    public static BattleUI Instance;
    [Header("Player")] [SerializeField] private HealthBar playerHealthBar;
    [SerializeField] private TMP_Text playerHPText;

    [Header("Enemies")] [SerializeField] private Transform enemyPanel;
    [SerializeField] private GameObject enemyUIPrefab;

    [Header("Log")] [SerializeField] private TMP_Text battleLog;
    [SerializeField] private TMP_Text turnText;

    public void SetTurnText(string data)
    {
        turnText.text = data;
    }


    private Dictionary<ProcessTarget, EnemyBattleUI> enemyBlocks = new();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void UpdatePlayerHealth(int current, int max)
    {
        playerHealthBar.UpdateHealthBar(current, max);
        playerHPText.text = $"{current}/{max}";
    }

    public void RegisterEnemy(ProcessTarget target)
    {
        GameObject block = Instantiate(enemyUIPrefab, enemyPanel);
        EnemyBattleUI ui = block.GetComponent<EnemyBattleUI>();
        ui.Init(target);
        enemyBlocks[target] = ui;
    }

    public void UpdateEnemyHealth(ProcessTarget target)
    {
        if (enemyBlocks.TryGetValue(target, out EnemyBattleUI ui))
            ui.Refresh();
    }

    public void RemoveEnemy(ProcessTarget target)
    {
        if (enemyBlocks.TryGetValue(target, out EnemyBattleUI ui))
        {
            Destroy(ui.gameObject);
            enemyBlocks.Remove(target);
        }
    }

    public void Log(string message)
    {
        if (battleLog != null)
        {
            battleLog.text = message;
        }

        Debug.Log(message);
    }
}