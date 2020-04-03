using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private StageProgress stageProgress;

    private int curStage;
    private string dataPath;

    public Attributes[] monsterAttr;
    public Attributes[] bossAttr;
    public GameObject[] monsterTypes;


    public void Init()
    {
        if (IsProgressExist())
            ReadProgress();
        else
            SaveProgress();
    }

    private bool IsProgressExist()
    {
        dataPath = System.IO.Path.Combine(Application.persistentDataPath, "StageProgess");
        if (System.IO.File.Exists(dataPath))
            return true;
        return false;

    }

    public void ReadProgress()
    {

        stageProgress = ScriptableObject.CreateInstance<StageProgress>();
        JsonUtility.FromJsonOverwrite(System.IO.File.ReadAllText(dataPath), stageProgress);
        this.curStage = stageProgress.stageID;

    }

    public void SaveProgress()
    {
        stageProgress = new StageProgress();
        stageProgress.stageID = curStage;

        string path = System.IO.Path.Combine(Application.persistentDataPath, "StageProgess");
        System.IO.File.WriteAllText(path, JsonUtility.ToJson(stageProgress, true));
    }

    public int GetStageID()
    {
        return curStage;
    }

    public void AddStageID()
    {
        curStage += 1;
    }

    public Attributes GetMonsterAttributes()
    {
        return monsterAttr[curStage];
    }

    public GameObject GetMonsterType()
    {
        return monsterTypes[curStage];
    }
    public Attributes GetBossAttributes() {
        return bossAttr[curStage];
    }
    public void setStageID(int id) {
        curStage = id;
        SaveProgress();
    }

}
