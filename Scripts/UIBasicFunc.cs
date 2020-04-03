using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBasicFunc : MonoBehaviour
{
    public GameObject[] panels;
    public void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }

    public void OpenPanel(int index)
    {
        panels[index].SetActive(true);
    }
}
