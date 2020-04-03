using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public Attributes attributes;
    public GameObject[] target;

    public EquipmentManager equipMgr;
    public RewardManager rewardMgr;
    public AudioManager audioMgr;

    public int levelExp = 60;
    public int levelHp = 5;
    public int levelAttack = 5;
    public int levelArmor = 1;
    //maybe is dummy
    protected int level;
    protected float exp;

    //store the attributes to avoid modify the scriptable object.
    protected float maxHP;
    protected float maxMP;
    protected float attack;
    protected float armor;

    //the current states of fighting.
    protected float currentHP;
    protected float currentMP;
    protected float attackBuff; //the amount of the attack buff/debuff. 
    protected float armorBuff; // the amount of the armor buff/debuff.
    protected float fightingTime;
    protected int skillsCount;

    // Start is called before the first frame update
    void Start()
    {
        InitData();
    }

    // Update is called once per frame
    void Update()
    {

    }


    //Finite State Machine will call
    public void Fighting(float dt) {
        fightingTime += dt;
        if (fightingTime < 2.5) return;
        fightingTime = 0;

        if (currentMP < maxMP) {
            //normal attack
            NormalAttack();
        }
        else {
            //skill
            Skill();

        }

    }
    public void InitData() {
        //load attribute from scriptable object 
        maxHP = attributes.maxHP;
        maxMP = attributes.maxMP;
        attack = attributes.attack;
        armor = attributes.armor;
        level = attributes.level;
        exp = attributes.exp;
        // add attack and armor from equipment
        if (this.tag == "Player")
        {
            AddEquipmentPower();
            AddLevelPower();
        } 

        //load fighting info.
        currentHP = maxHP;
        currentMP = 0;
        attackBuff = 0;
        armorBuff = 0;
        fightingTime = 0;
        skillsCount = 0;
    }
    public void SetTarget(GameObject[] targets) {
        target = targets;
    }
    //Skill will call
    public void GetHit(float damage) {
        float currentArmor = armor + armorBuff;
        if (currentArmor < 0) currentArmor = 0;
        float amount = damage - currentArmor;
        if (amount < 0) amount = 0;
        currentHP -= amount;
        if (currentHP <= 0)
        {
            currentHP = 0;
            //do something when die
            Dead();
        }
        else
        {
            GetHitAnimation();
        }

    }
    public void RecoverHp(float ratio) {
        float amount = ratio * maxHP;
        currentHP += amount;
        if (currentHP > maxHP) currentHP = maxHP;
    }
    public void RecoverMp(float ratio) {
        float amount = ratio * maxMP;
        currentMP += amount;
        if (currentMP > maxMP) currentMP = maxMP;
    }
    public void AttackBuff(float amount, float time) {
        attackBuff += amount;
        StartCoroutine(DisableAttackBuff(amount, time));
    }
    public void ArmorBuff(float amount, float time) {
        armorBuff += amount;
        StartCoroutine(DisableArmorBuff(amount, time));
    }
    public float GetCurrentAttack() {
        return attack + attackBuff;
    }
    //Something can be overwritten when use different player or monster
    protected virtual void Dead() {
        DeadAnimation();
        this.gameObject.SetActive(false);
    }
    protected virtual void NormalAttack() {
        attributes.normalAttack.Used(this.gameObject, target);
        //animation
        NormalAttackAnimation();

    }
    protected virtual void Skill() {
        attributes.skills[skillsCount].Used(this.gameObject, target);
        skillsCount++;
        if (skillsCount >= attributes.skills.Length) skillsCount = 0;
        SkillAttackAnimation();
    }
    protected virtual void NormalAttackAnimation() {
        //animation
        Animation ani = GetComponent<Animation>();
        if (ani == null) return;
        if(audioMgr != null)audioMgr.normal.Play();
        ani.CrossFade("attack");
        ani.CrossFadeQueued("run");
    }
    protected virtual void SkillAttackAnimation() {
        Animation ani = GetComponent<Animation>();
        if (ani == null) return;
        if(audioMgr != null)audioMgr.skill.Play();
        ani.CrossFade("skill");
        ani.CrossFadeQueued("skill");
        ani.CrossFadeQueued("run");
    }
    protected virtual void DeadAnimation() {
        Debug.Log("Dead");
    }
    protected virtual void GetHitAnimation() {
        Debug.Log(this.gameObject.name + currentHP);
    }

    //Disable buff
    protected IEnumerator DisableAttackBuff(float amount , float time) {
        yield return new WaitForSeconds(time);
        attackBuff -= amount;
    }
    protected IEnumerator DisableArmorBuff(float amount, float time)
    {
        yield return new WaitForSeconds(time);
        armorBuff -= amount;
    }

    public void SetAttributes(Attributes attr)
    {
        this.attributes = attr;
        InitData();
    }

    public void AddEquipmentPower()
    {
        //0~2 is weapon, search for max level weapon
        for (int i = 2; i >=0; --i)
        {
            if (equipMgr.isLock[i] == false)
            {
                attack += equipMgr.equipments[i].GetComponent<Equipment>().attack;
                break;
            }    
        }

        //3~5 is Armor, search for max level armor
        for (int i = 5; i >= 3; --i)
        {
            if (equipMgr.isLock[i] == false)
            {
                armor += equipMgr.equipments[i].GetComponent<Equipment>().armor;
                break;
            }
        }
    }

    public void AddLevelPower()
    {
        int level = (rewardMgr.GetLevel());
        attack += level * levelAttack;
        armor += level * levelAttack;
        maxHP += level * levelHp;
    }

    public float GetHp()
    {
        return currentHP;
    }

}
