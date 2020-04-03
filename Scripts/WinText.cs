using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinText : MonoBehaviour
{
    public SkillManager skillMgr;
    public Image winSkill ;
    Text win;

    // Update is called once per frame
    void OnEnable()
    {
        win = this.GetComponent<Text>();
        int curStage = GameFSM._instance.stageMgr.GetStageID();
        win.text = "Coin: " + "150\n" +
                   "Exp: " + (curStage*20 + 20).ToString() + "\n" + 
                   "New Skill: \n\n\n"+
                    skillMgr.playerSkills[curStage+4].description ;
        winSkill.sprite = skillMgr.playerSkills[curStage+4].icon;
    }
}
