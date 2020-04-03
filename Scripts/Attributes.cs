using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyMenu/Attributes")]
public class Attributes : ScriptableObject
{
    // The permanant attributes. Only when level up, get the exp and wear equipment will modify these values.
    public float maxHP;
    public float maxMP;
    public float attack;
    public float armor;

    public int level;
    public float exp;

    public Skill normalAttack;
    public Skill[] skills;
        

}
