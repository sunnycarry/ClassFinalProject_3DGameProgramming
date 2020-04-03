using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public Attributes playerAttr;
    public SkillRecord skillRecord;
    public GameObject[] allSkillButton;
    public Skill[] playerSkills;
    
    public int[] skillGet;
    public int[] skillChange;

    private Sprite[] allImg;
    private string dataPath;
    // Start is called before the first frame update
    void Start()
    {
        skillGet = new int[10] { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0 };
        skillChange = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        allImg = new Sprite[10];
        InitAllImage();
        InitRecord();
        ResetPicture();
        ResetPlayerSkill();
    }

    public void InitRecord()
    {
        if (IsRecordExist())
            ReadRecord();
        else
            CreateRecord();
    }

    private bool IsRecordExist()
    {
        dataPath = System.IO.Path.Combine(Application.persistentDataPath, "SkillRecord");
        if (System.IO.File.Exists(dataPath))
            return true;
        return false;

    }

    public void ReadRecord()
    {

        skillRecord = ScriptableObject.CreateInstance<SkillRecord>();
        JsonUtility.FromJsonOverwrite(System.IO.File.ReadAllText(dataPath), skillRecord);
        this.skillGet = skillRecord.skillGet;
        this.skillChange = skillRecord.skillChange;

    }

    public void SaveRecord()
    {
        skillRecord = new SkillRecord();
        skillRecord.Init();
        skillRecord.skillGet = this.skillGet;
        skillRecord.skillChange = this.skillChange;

        string path = System.IO.Path.Combine(Application.persistentDataPath, "SkillRecord");
        System.IO.File.WriteAllText(path, JsonUtility.ToJson(skillRecord, true));
    }

    public void CreateRecord()
    {
        skillRecord = new SkillRecord();
        skillRecord.Init();

        string path = System.IO.Path.Combine(Application.persistentDataPath, "SkillRecord");
        System.IO.File.WriteAllText(path, JsonUtility.ToJson(skillRecord, true));
    }


    public void ResetPicture()
    {
        for (int i = 0; i < 10; ++i)
            allSkillButton[i].GetComponent<Image>().sprite = allImg[skillChange[i]];
    }

    public void InitAllImage()
    {
        for (int i = 0; i < 10; ++i)
            allImg[i] = allSkillButton[i].GetComponent<Image>().sprite;

    }

    public int GetState(int idx)
    {
        return skillGet[idx];
    }

    public void SwapSkillOrderArray(int idx1, int idx2)
    {
        int tmp = skillChange[idx1];
        skillChange[idx1] = skillChange[idx2];
        skillChange[idx2] = tmp;
    }

    public void SwapSkillImage(int idx1, int idx2)
    {
        Sprite s = allSkillButton[idx1].GetComponent<Image>().sprite;
        allSkillButton[idx1].GetComponent<Image>().sprite = allSkillButton[idx2].GetComponent<Image>().sprite;
        allSkillButton[idx2].GetComponent<Image>().sprite = s;
    }

    public void SwapPlayerSkill(int idx1, int idx2)
    {
        Skill tmpS = playerAttr.skills[idx1];
        playerAttr.skills[idx1] = playerAttr.skills[idx2];
        playerAttr.skills[idx2] = tmpS;
    }

    public void ResetPlayerSkill()
    {
        for (int i = 0; i < 4; ++i)
            playerAttr.skills[i] = playerSkills[skillChange[i]];
    }
    public void unlockSkill(int index) {
        skillRecord.skillGet[index] = 1;
        SaveRecord();
    }
}
