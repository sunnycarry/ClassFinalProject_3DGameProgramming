using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using System.IO;

public class RewardManager : MonoBehaviour
{
    private Reward reward;

    public Text moneyText;
    public Text expText;
    public Text levelText;

    [SerializeField]
    private GameObject rewardPanel;
    [SerializeField]
    private Text panelExpText;
    [SerializeField]
    private Text panelMoneyText;
    [SerializeField]
    private Text panelOfflineText;

    public int money;
    private int exp;
    public int levelExp;

    private string timeLastLogOut;
    private DateTime timeNowLogIn;
    private TimeSpan timespan;

    [Header("Reward Rate( per second)")]
    public float moneyRate;
    public float expRate;

    private string dataPath;

    void Awake()
    {
        if (IsRewardExist())
        {
            ReadRewardInfo();
            if (GameFSM._instance.boot)
            {
                ComputeReward();
                SaveRewardInfo();
                ShowRewardPanel();
            }
        }
        else 
        {
            SaveRewardInfo();
        }
        GameFSM._instance.boot = false;
        RefreshReward();
    }

    private bool IsRewardExist()
    {
        dataPath = System.IO.Path.Combine(Application.persistentDataPath, "Reward");
        if (System.IO.File.Exists(dataPath))
            return true;
        return false;
        
    }

    private void ComputeReward()
    {
        timeLastLogOut = reward.timeLastLogOut;
        timeNowLogIn = DateTime.Now;
        timespan = timeNowLogIn.Subtract(Convert.ToDateTime(timeLastLogOut));
        this.money += (int)(timespan.TotalSeconds * moneyRate);
        this.exp += (int)(timespan.TotalSeconds * expRate);

    }

    public void RefreshReward()
    {
        moneyText.text = this.money.ToString();
        expText.text = (this.exp % levelExp).ToString() + " / " + levelExp.ToString();
        levelText.text = GetLevel().ToString();
    }

    private void ReadRewardInfo()
    {
        reward = ScriptableObject.CreateInstance<Reward>();
        JsonUtility.FromJsonOverwrite(System.IO.File.ReadAllText(dataPath), reward);
        this.money = reward.money;
        this.exp = reward.exp;
        this.timeLastLogOut = reward.timeLastLogOut;
    }

    public void SaveRewardInfo()
    {
        reward = new Reward();
        reward.money = this.money;
        reward.exp = this.exp;
        reward.timeLastLogOut = DateTime.Now.ToString();

        string path = System.IO.Path.Combine(Application.persistentDataPath, "Reward");
        System.IO.File.WriteAllText(path, JsonUtility.ToJson(reward, true));
    }

    private void ShowRewardPanel()
    {
        SetRewardText();
        rewardPanel.SetActive(true);
    }

    private void SetRewardText()
    {
        panelExpText.text = ((int)timespan.TotalSeconds * expRate).ToString();
        panelMoneyText.text = ((int)timespan.TotalSeconds * moneyRate).ToString();

        string offlineTime = ((int)timespan.Hours).ToString("d2") + ":" 
                            + ((int)timespan.Minutes).ToString("d2") + ":" 
                            + ((int)timespan.Seconds).ToString("d2");
        panelOfflineText.text = offlineTime;
    }

    public void ClosePanel()
    {
        rewardPanel.SetActive(false);
    }

    public void AddMoney(int m)
    {
        this.money += m;
    }

    public void AddExp(int e)
    {
        this.exp += e;
    }

    public void NormalVictory(int money, int exp)
    {
        AddMoney(money);
        AddExp(20 + GameFSM._instance.stageMgr.GetStageID()*exp);
        RefreshReward();
        SaveRewardInfo();
    }

    public int GetLevel()
    {
        return 1+ this.exp / levelExp;
    }
}
