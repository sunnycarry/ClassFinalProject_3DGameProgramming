using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Start,
    SaveData,
    LoadData,
    LoadingEndless,
    NormalLevel,
    LoadingBoss,
    BossLevel,
    Close
}

public class GameFSM : MonoBehaviour
{
    public static GameFSM _instance;
    public StageManager stageMgr;
    public SceneController sceneController;
    //public GameObject rewardMgr;
    public int nextScene;

    GameState currentState;
    public bool boot; //the first time to open the game.
    
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            stageMgr = this.GetComponent<StageManager>();
            sceneController = this.GetComponent<SceneController>();
            currentState = GameState.Start;
            boot = true;
            nextScene = 2;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GameState.Start) {
            if (Input.anyKeyDown)
            {
                currentState = GameState.LoadData;
            }
            else {
                currentState = GameState.Start;
            }
        }
        else if (currentState == GameState.SaveData) {
            stageMgr.SaveProgress();
            currentState = GameState.Close;
        }
        else if (currentState == GameState.LoadData) {
            //Init stage manager to let GameMgr know which level should be loaded.
            stageMgr.Init();
            currentState = GameState.LoadingEndless;
        }
        else if (currentState == GameState.LoadingEndless) {
            //calculate the next scene ID according to stage ID.
            //ALL PASS
            if (stageMgr.GetStageID() >= 6)
            {
                sceneController.LoadSceneIndex(6);
            }
            else
            {
                nextScene = stageMgr.GetStageID() / 3 + 2;
                //enter the loading scene
                sceneController.LoadSceneIndex(1);
            }
            //
            currentState = GameState.NormalLevel;
        }
        else if (currentState == GameState.NormalLevel) {
            //do something
        }
        else if (currentState == GameState.LoadingBoss) {
            nextScene = stageMgr.GetStageID() / 3 + 4;
            sceneController.LoadSceneIndex(1);
            currentState = GameState.BossLevel;
        }
        else if (currentState == GameState.BossLevel) {
            //may do nothing
        }
        else if (currentState == GameState.Close) {
           Application.Quit();
        }
        
    }
    public void Transition(int nextState) {
        currentState = (GameState)nextState;
    }
}
