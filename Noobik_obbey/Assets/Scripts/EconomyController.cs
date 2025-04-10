using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomyController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyBalanceTEXT_UI;
    [SerializeField] private TextMeshProUGUI forceBananceTEXT_UI;

    private void Start()
    {
        RefreshStatsUI();
    }

    private void RefreshStatsUI()
    {
        PlayerPrefs.Save();

        moneyBalanceTEXT_UI.text = GetBanaceMoney().ToString();
        forceBananceTEXT_UI.text = GetBanaceForce().ToString();
    }

    public int GetBanaceMoney()
    {
        return PlayerPrefs.GetInt("pp_money");
    }

    public int GetBanaceForce()
    {
        return PlayerPrefs.GetInt("pp_force");
    }

    public void PlusBanaceMoney(int money)
    {
        PlayerPrefs.SetInt("pp_money", GetBanaceMoney() + money);

        RefreshStatsUI();
    }

    public void PlusBanaceForce(int force)
    {
        PlayerPrefs.SetInt("pp_force", GetBanaceForce() + force);

        RefreshStatsUI();
    }

    public void MinusBanaceMoney(int minus)
    {
        PlayerPrefs.SetInt("pp_money", GetBanaceMoney() - minus);

        RefreshStatsUI();
    }

    public void MinusBanaceForce(int minus)
    {
        PlayerPrefs.SetInt("pp_force", GetBanaceForce() - minus);

        RefreshStatsUI();
    }





}
