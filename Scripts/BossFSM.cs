using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    Init,
    ReadyToFighting,
    Fighting,
    Win,
    Fail,
    FinishFighting
}
//Modified version: Wait dead animation finish in FinishingFighting. Then, go to Win or Fail state. Maybe can add a state to change scene (optional).
public class BossFSM : MonoBehaviour
{
    public GameObject player;
    public GameObject boss;
    public GameObject winUI;
    public GameObject loseUI;

    public RewardManager rewardMgr;
    public SkillManager skillMgr;
    public AudioManager audioMgr;

    public ShowHp playerHp;
    public ShowHp bossHp;

    BossState currentState;
    PlayerStates playerStates;
    MonsterStates bossStates;
    bool win;
    // Start is called before the first frame update
    private void Awake()
    {
        currentState = BossState.Init;
        playerStates = player.GetComponent<PlayerStates>();
        bossStates = boss.GetComponent<MonsterStates>();
        win = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == BossState.Init) {
            //load data from disk
            currentState = BossState.ReadyToFighting;
        }
        else if (currentState == BossState.ReadyToFighting) {
            //play the WARNING animation
            //init the states
            playerStates.InitData();
            Attributes bossattr = GameFSM._instance.stageMgr.GetBossAttributes();
            bossStates.SetAttributes(bossattr);
            currentState = BossState.Fighting;
            //add here to prevent from race condition
            playerHp.SetMaxHp(player.GetComponent<PlayerStates>().GetHp());
            bossHp.SetMaxHp(boss.GetComponent<PlayerStates>().GetHp());
        }
        else if (currentState == BossState.Fighting) {
            //start fighting
            if(player.activeSelf) playerStates.Fighting(Time.deltaTime);
            if(boss.activeSelf) bossStates.Fighting(Time.deltaTime);//make all monster keep fighting

            if (player.activeSelf && boss.activeSelf) currentState = BossState.Fighting;
            else if (player.activeSelf)
            {
                //player win
                currentState = BossState.Win;
                if (!audioMgr.win.isPlaying) audioMgr.win.Play();
            }
            else {
                //player lose
                currentState = BossState.Fail;
                if (!audioMgr.lose.isPlaying) audioMgr.lose.Play();
            }
        }
        else if (currentState == BossState.Win) {
            //win picture and reward compute including updating the level.
            winUI.SetActive(true);
            win = true;
        }
        else if (currentState == BossState.Fail) {
            //fail picture
            loseUI.SetActive(true);
            win = false;
        }
        else if (currentState == BossState.FinishFighting) {
            //wrtie data into disk and then change scene.
            GameFSM._instance.Transition(3);
        }

    }
    public void ToFinishState() {
        if (win)
        {   //reward 
            rewardMgr.NormalVictory(150, 20);
            //skill
            skillMgr.unlockSkill(GameFSM._instance.stageMgr.GetStageID()+4);
            GameFSM._instance.stageMgr.AddStageID();
            GameFSM._instance.stageMgr.SaveProgress();
        }
        currentState = BossState.FinishFighting;
    }
}
