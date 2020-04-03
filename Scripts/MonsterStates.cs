using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStates : PlayerStates
{
    public Animator animator;
    public string normalAttack;
    public string skill;
    public string dead;
    public string getHit;

    private void Start()
    {
        InitData();
    }

    protected override void NormalAttackAnimation()
    {
        //animation
        if (normalAttack == "None") return;
        animator.Play(normalAttack);
    }
    protected override void SkillAttackAnimation()
    {
        if (skill == "None") return;
        animator.Play(skill);
    }
    protected override void DeadAnimation()
    {
        if (dead == "None") return;
        animator.Play(dead);
    }
    protected override  void GetHitAnimation()
    {
        if(getHit == "None") return;
        animator.Play(getHit);
    }
}
