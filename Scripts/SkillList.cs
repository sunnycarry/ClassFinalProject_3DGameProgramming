using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillList : MonoBehaviour
{
    public SkillManager skillMgr;
    public GameObject[] skillImg;
    public GameObject changePanel;

    private int skillNeedChange = 0;
    private int skillReplace = 0;

    void OnEnable()
    {
        ShowSkillIcon();
    }

    public void ShowSkillIcon()
    {
        for (int i = 4; i < 10; ++i)
        {
            if (skillMgr.GetState(i) == 1)
                skillImg[i].SetActive(true);
            else
                skillImg[i].SetActive(false);
        }
    }

    
    public void OpenSkillList(int skillIdx)
    {
        skillNeedChange = skillIdx;
        changePanel.SetActive(true);
    }

    // 選擇要更換的技能
    public void SelectSkillReplace(int skillIdx)
    {
        skillReplace = skillIdx;
        changePanel.SetActive(false);
        skillMgr.SwapSkillOrderArray(skillNeedChange, skillReplace);
        skillMgr.ResetPicture();
        skillMgr.ResetPlayerSkill();
        skillMgr.SaveRecord();
    }
}
