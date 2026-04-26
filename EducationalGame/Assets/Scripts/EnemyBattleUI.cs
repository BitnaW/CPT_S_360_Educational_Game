using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Button attackButton;

    private ProcessTarget target;
    
    public void Init(ProcessTarget t)
    {
        target = t;
        nameText.text = t.gameObject.name;
        Refresh();
        attackButton.onClick.AddListener(() => BattleManager.Instance.OnPlayerSelectTarget(target));
    }

    public void Refresh()
    {
        hpText.text = $"{target.remainingTime}/{target.burstTime}";
        healthBar.UpdateHealthBar(target.remainingTime, target.burstTime);
    }
}
