using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHp : MonoBehaviour
{
    public PlayerStates target;

    public Image hp;

    private float fullHp;
    private float curHp;
    private bool isSet;
    void Awake()
    {
        isSet = false;
    }

    void Update()
    {
        if (!isSet)
            return;
        hp.fillAmount = target.GetHp()/fullHp;

    }

    public void SetMaxHp(float hp)
    {
        fullHp = hp;
        isSet = true;
    }
}
