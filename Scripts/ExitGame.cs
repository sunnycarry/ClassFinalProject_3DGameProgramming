using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public RewardManager rewardMgr;
    public SkillManager skillMgr;
    public EquipmentManager equipMgr;

    public void Quit()
    {
        GameFSM._instance.Transition(1);
        rewardMgr.SaveRewardInfo();
        skillMgr.SaveRecord();
        equipMgr.SaveEquipState();
        Debug.Log("exit");
    }
}
