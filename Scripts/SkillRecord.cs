using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillRecord", menuName = "MyMenu/SkillRecord")]
public class SkillRecord : ScriptableObject
{
    public int[] skillGet;
    public int[] skillChange;

    public void Init()
    {
        skillGet = new int[10] { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0 };
        skillChange = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    }
}
