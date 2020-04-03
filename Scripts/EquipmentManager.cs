using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public int targetId;

    public Button[] equipments;
    public bool[] isLock = new bool[6];

    private string dataPath;
    private EquipState equipState;


    private void Awake()
    {
        if (IsEquipStateExist())
            ReadEquipState();
        else
            SaveEquipState();

    }

    public bool GetState(int idx)
    {
        return isLock[idx];
    }

    public void ButItem()
    {
        bool canBuy = equipments[targetId].GetComponent<Equipment>().GetCanBuy();
        if (canBuy)
        {
            equipments[targetId].GetComponent<Equipment>().UnlockEquipment();
            isLock[targetId] = false;
            SaveEquipState();
        }
        else
        {
            equipments[targetId].GetComponent<Equipment>().ClosePanel();
        }
    }

    public void SetTargetItem(int idx)
    {
        targetId = idx;
    }

    private bool IsEquipStateExist()
    {
        dataPath = System.IO.Path.Combine(Application.persistentDataPath, "EquipState");
        if (System.IO.File.Exists(dataPath))
            return true;
        return false;

    }

    public void ReadEquipState()
    {

        equipState = ScriptableObject.CreateInstance<EquipState>();
        JsonUtility.FromJsonOverwrite(System.IO.File.ReadAllText(dataPath), equipState);
        this.isLock[0] = equipState.item1;
        this.isLock[1] = equipState.item2;
        this.isLock[2] = equipState.item3;
        this.isLock[3] = equipState.item4;
        this.isLock[4] = equipState.item5;
        this.isLock[5] = equipState.item6;

    }

    public void SaveEquipState()
    {
        equipState = new EquipState();
        equipState.item1 = isLock[0];
        equipState.item2 = isLock[1];
        equipState.item3 = isLock[2];
        equipState.item4 = isLock[3];
        equipState.item5 = isLock[4];
        equipState.item6 = isLock[5];

        string path = System.IO.Path.Combine(Application.persistentDataPath, "EquipState");
        System.IO.File.WriteAllText(path, JsonUtility.ToJson(equipState, true));
    }

}
