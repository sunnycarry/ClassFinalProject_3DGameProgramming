using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    public int attack;
    public int armor;
    public int unlockMoney;

    public bool isLock;
    public int index;
    public RewardManager rewardMgr;
    public GameObject infoPanel;
    public EquipmentManager equipMgr;
    private bool canUnlock;

    void Start()
    {
        this.gameObject.SetActive(true);
        isLock = equipMgr.GetState(index);
        if(isLock)
            SetImageAlpha(0.4f);
        else
            SetImageAlpha(1);


    }

    public void OnClickUp()
    {
        if (isLock)
        {
            string info = GetUnlockInformation();
            SetPanelInfo(info);
            infoPanel.SetActive(true);
            equipMgr.SetTargetItem(index);
        }
    }

    private string GetUnlockInformation()
    {
        string s = "";
        int moneyNeed = GetMoneyDiff();
        if (moneyNeed < 0)
        {
            s += "You still need " + (-1*moneyNeed).ToString() + " \n to unlock it!";
            canUnlock = false;
        }
        else
        {
            s += "Unlock or not?\n (Remain " + moneyNeed.ToString() + " dollars)";
            canUnlock = true;
        }

        return s;
    }

    private int GetMoneyDiff()
    {
        int diff = rewardMgr.money - this.unlockMoney;
        return diff;
    }

    private void SetPanelInfo(string s)
    {
        infoPanel.transform.GetChild(0).GetComponent<Text>().text = s;
    }

    public void UnlockEquipment()
    {
        isLock = false;
        SetImageAlpha(1);
        rewardMgr.AddMoney(-1*unlockMoney);
        rewardMgr.RefreshReward();
        infoPanel.SetActive(false);
    }

    public void SetImageAlpha(float alpha)
    {
        Color c = this.GetComponent<Image>().color;
        c.a = alpha;
        this.GetComponent<Image>().color = c;
    }

    public bool GetCanBuy()
    {
        if (isLock == true && canUnlock == true)
        {
            return true;
        }
        return false;
    }

    public void ClosePanel()
    {
        infoPanel.SetActive(false);
    }
}
