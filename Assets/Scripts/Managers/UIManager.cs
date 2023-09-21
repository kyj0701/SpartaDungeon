using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Player Info")]
    public TextMeshProUGUI levelText;
    public Slider expBar;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI maxExpText;
    public TextMeshProUGUI goldText;

    [Header("Player Status")]
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI maxHealthText;
    public TextMeshProUGUI criticalText;

    void Start()
    {
        UpdateInfoText();
    }

    public void UpdateInfoText()
    {
        levelText.text = GameManager.Instance.Player.Level.ToString();
        expBar.value = GameManager.Instance.Player.Exp;
        expBar.maxValue = GameManager.Instance.Player.MaxExp;
        expText.text = GameManager.Instance.Player.Exp.ToString();
        maxExpText.text = "/ " + GameManager.Instance.Player.MaxExp.ToString();
        goldText.text = GameManager.Instance.Player.Gold.ToString();
    }

    public void UpdateStatusText()
    {
        attackText.text = GameManager.Instance.Player.status.Attack.ToString();
        if (GameManager.Instance.Player.status.PlusAttack > 0) attackText.text += " (+" + GameManager.Instance.Player.status.PlusAttack + ")";
        defenseText.text = GameManager.Instance.Player.status.Defense.ToString();
        if (GameManager.Instance.Player.status.PlusDefense > 0) defenseText.text += " (+" + GameManager.Instance.Player.status.PlusDefense + ")";
        maxHealthText.text = GameManager.Instance.Player.status.MaxHealth.ToString();
        if (GameManager.Instance.Player.status.PlustMaxHealth > 0) maxHealthText.text += " (+" + GameManager.Instance.Player.status.PlustMaxHealth + ")";
        criticalText.text = GameManager.Instance.Player.status.Critical.ToString();
        if (GameManager.Instance.Player.status.PlustCritical > 0) criticalText.text += " (+"+ GameManager.Instance.Player.status.PlustCritical + ")";
    }
}
