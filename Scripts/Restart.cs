using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Restart : MonoBehaviour
{
    public void Again() {
        //reset stage
        GameFSM._instance.stageMgr.setStageID(0);
        //reset stage
        SkillRecord skillRecord = new SkillRecord();
        skillRecord.Init();

        string path = System.IO.Path.Combine(Application.persistentDataPath, "SkillRecord");
        System.IO.File.WriteAllText(path, JsonUtility.ToJson(skillRecord, true));
        //reset reward
        Reward reward = new Reward();
        reward.money = 0;
        reward.exp = 0;
        reward.timeLastLogOut = DateTime.Now.ToString();

        path = System.IO.Path.Combine(Application.persistentDataPath, "Reward");
        System.IO.File.WriteAllText(path, JsonUtility.ToJson(reward, true));

        //reset equippment
        EquipState equipState = new EquipState();
        equipState.item1 = true;
        equipState.item2 = true;
        equipState.item3 = true;
        equipState.item4 = true;
        equipState.item5 = true;
        equipState.item6 = true;

        path = System.IO.Path.Combine(Application.persistentDataPath, "EquipState");
        System.IO.File.WriteAllText(path, JsonUtility.ToJson(equipState, true));
        //go back to endless scene1
        GameFSM._instance.Transition(2);
    }
    public void Back() {
        GameFSM._instance.stageMgr.setStageID(5);
        //go back to endless scene2
        GameFSM._instance.Transition(3);
    }
}
