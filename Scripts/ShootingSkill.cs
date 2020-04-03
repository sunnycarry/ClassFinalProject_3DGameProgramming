using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyMenu/Shooting Skill")]
public class ShootingSkill : Skill
{
    public float accelerationRate;
    public GameObject trajectory;

    public  override void Used(GameObject user, GameObject[] target) {
        PlayerStates userStates = user.GetComponent<PlayerStates>();
        //apply to the targets
        int numOfAttack = 0;
        for (int n = 0; numOfAttack < attackArea && n < target.Length ; n++)
        {
            if (!target[n].activeSelf) continue; //if the target is alive, ignore it.
            numOfAttack++;
            //particle effect
            for (int i = 0; i < particleOnEnemy.Length; i++)
            {
                GameObject p = Instantiate(particleOnEnemy[i]);

                GameObject t = Instantiate(trajectory);
                ParticleTrajectory pt = t.GetComponent<ParticleTrajectory>();
                //motion with a trajectory and do damage when finish.
                pt.Init(user, target[n], p, accelerationRate , userStates.GetCurrentAttack() * damageFactor);
            }
        }
        //apply to user
        userStates.RecoverMp(-consumeMPRatio);
        userStates.RecoverHp(recoverHPRatio);
        userStates.AttackBuff(attackBuff, bufftime);
        userStates.ArmorBuff(armorBuff, bufftime);
        //particle effect
        for (int i = 0; i < particleOnUser.Length; i++)
        {
            GameObject p = Instantiate(particleOnUser[i]);
            p.transform.position += user.transform.position;
        }
        Debug.Log(ToString() + "is used");
    }

    
}

