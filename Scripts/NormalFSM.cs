using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {
    Init,
    Walking,
    ReadyToFighting,
    Fighting,
    FinishFighting
}
public class NormalFSM : MonoBehaviour
{
    public GameObject player;
    public EndlessSceneMangager endlessSceneMgr;
    public MonsterManager monsterMgr;
    public StageManager stageMgr;
    public RewardManager rewardMgr;
    public CameraSlerp cameraSlerp;
    public AudioManager audioMgr;


    State currentState;
    PlayerStates playerStates;
   
    float walkingTime; 
    // Start is called before the first frame update
    private void Awake()
    {
        currentState = State.Init;
        playerStates = player.GetComponent<PlayerStates>();
        stageMgr = GameFSM._instance.stageMgr;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.Init)
        {
            // stage Info has been loaded in Start() by StageManager

            //load data from disk.
            //need to add...............
            walkingTime = 0;
            //state transition
            currentState = State.Walking;
        }
        else if (currentState == State.Walking)
        {
            //Move using endless scene manager
            endlessSceneMgr.Move(Time.deltaTime);
            walkingTime += Time.deltaTime;
            //animation
            playerStates.GetComponent<Animation>().Play("run");
            //audio
            if (!audioMgr.footstep.isPlaying)
            {
                audioMgr.footstep.Play();
                Debug.Log("play!");
            }
            //state transition
            if (walkingTime > 10.0)
            {
                walkingTime = 0;
                audioMgr.footstep.Pause();
                currentState = State.ReadyToFighting;
            }
            else {
                currentState = State.Walking;
            }
            
        }
        else if (currentState == State.ReadyToFighting)
        {
            //initial player state
            playerStates.InitData();
            //choose monster type by stageMgr
            GameObject monsterType = stageMgr.GetMonsterType();
            monsterMgr.SetMonsterType(monsterType);
            //Generate monster
            monsterMgr.GenerateMonster(player);
            //Set Monster attribute based on stage-id in stageMgr
            Attributes attr = stageMgr.GetMonsterAttributes();
            monsterMgr.SetAllMonsterAttributes(attr);
            //
            monsterMgr.SetTarget(player);
            playerStates.SetTarget(monsterMgr.GetAllMonster());
            //camera slerp
            cameraSlerp.Forward();
            //state transition
            currentState = State.Fighting;
        }
        else if (currentState == State.Fighting)
        {
            playerStates.Fighting(Time.deltaTime);
            monsterMgr.Fighting(Time.deltaTime);//make all monster keep fighting
            if (DetectFightingContinued()) currentState = State.Fighting;
            else currentState = State.FinishFighting;
            
        }
        else if (currentState == State.FinishFighting) {
            //compute reward
            rewardMgr.NormalVictory(20, 10);
            //sound
            audioMgr.pickup.Play();
            //destroy all dead monster
            monsterMgr.DestroyAllMonster();
            //camera slerp
            cameraSlerp.Backward();
            currentState = State.Walking;
            
        }
    }
    bool DetectFightingContinued() {
        bool flg = false; //to indicate if keep fighting
        for (int n = 0; n < playerStates.target.Length; n++)
        {
            if (playerStates.target[n].activeSelf) flg |= true;
        }
        flg &= this.gameObject.activeSelf;
        return flg;
    }
 
}
