using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyMenu/Skill")]
public class Skill : ScriptableObject
{
    /********** Content of the skill *******/
    public string description; //description of the skill.
    public Sprite icon;        //the icon of the skill.
    public GameObject[] particleOnEnemy;
    public GameObject[] particleOnUser;

    /********* Combate information **********/
    public float damageFactor; // the damage = attack * damageFactor;
    public float recoverHPRatio; // the HP recovery amount = recoverRatio * maxHP ; 
    public float consumeMPRatio; // the MP consume amount = ratio * maxMP.
    public float armorBuff;   //buff the armor.
    public float attackBuff;  //buff the attack.
    public float criticalProb; //the probabiliy of critical attack.
    public int attackArea; //the number of enemies can be attacked.
    public float bufftime;
    
    /********* State inforamtion ************/
    public int level;
    public float exp;
    public bool isOwn;

    public virtual void Used(GameObject user, GameObject[] target) {
        PlayerStates userStates = user.GetComponent<PlayerStates>();

        //apply to target
        int numOfAttack = 0;
        for (int n = 0; numOfAttack < attackArea && n < target.Length; n++)
        {
            if (!target[n].activeSelf) continue; //if the target isn't alive, ignore it.
            numOfAttack++;
            PlayerStates targetState = target[n].GetComponent<PlayerStates>();
            if (targetState != null)
            {
                targetState.GetHit(userStates.GetCurrentAttack() * damageFactor);
            }
            //particle effect on enemy
            for (int i = 0; i < particleOnEnemy.Length; i++)
            {
                GameObject p = Instantiate(particleOnEnemy[i]);
                p.transform.position += target[n].transform.position;      
            }
        }

        //apply to user
        userStates.RecoverMp(-consumeMPRatio);
        userStates.RecoverHp(recoverHPRatio);
        userStates.AttackBuff(attackBuff , bufftime);
        userStates.ArmorBuff(armorBuff, bufftime);
        //particle effect on user
        for (int i = 0; i < particleOnUser.Length; i++)
        {
            GameObject p = Instantiate(particleOnUser[i]);
            p.transform.position += user.transform.position;
        }
        Debug.Log(ToString() + "is used");
    }
}
