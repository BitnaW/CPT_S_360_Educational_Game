using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    [SerializeField] private TargetManager targetManager;
    [SerializeField] private Shooter player;
    [SerializeField] private List<Button> buttons;


    private bool isBusy;

    private void Awake()
    {
        Instance = this;
    }

    public void OnPlayerSelectTarget(ProcessTarget target)
    {
        if (isBusy) return;
        StartCoroutine(PlayTurn(target));
    }

    private IEnumerator PlayTurn(ProcessTarget selectedTarget)
    {
        isBusy = true;
        BattleUI.Instance.SetTurnText("Player's Turn");
        ButtonsUsable(false);

        targetManager.OnTargetHit(selectedTarget);
        yield return new WaitForSeconds(2);
        if (player.playerHealth <= 0)
            // put death/game over screen here 
            yield break;

        BattleUI.Instance.SetTurnText("Enemy's Turn");
        foreach (var enemy in targetManager.GetValidTargets())
        {
            enemy.AttackPlayer(player); // everyone attacks player when it's the enemy turn 
            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(2);

        if (targetManager.GetValidTargets().Count == 0)
        {
            // win condition
            BattleUI.Instance.SetTurnText("Player won");
            yield break;
        }

        BattleUI.Instance.SetTurnText("Player's Turn");
        isBusy = false;
        ButtonsUsable(true);
    }

    private void ButtonsUsable(bool state)
    {
        foreach (var button in buttons) button.interactable = state;
    }
}