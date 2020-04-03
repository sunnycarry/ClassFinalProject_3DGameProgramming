using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAward", menuName ="MyMenu/Reward")]
public class Reward : ScriptableObject
{
    public int money;
    public int exp;
    
    // because scriptable object can't store type "DateTime"
    // use Convert.ToDateTime() in RewardManager to Convert and Compute 
    public string timeLastLogOut;

}
