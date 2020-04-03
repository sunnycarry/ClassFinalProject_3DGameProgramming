using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public GameObject[] monsterType;
    public Transform[] respawnPos;

    private GameObject[] monsters;
    // Start is called before the first frame update
    public void DestroyAllMonster() {
        for (int i = 0; i < monsters.Length; ++i)
        {
            Destroy(monsters[i]);
        }
    }
    public void GenerateMonster(GameObject player)
    {
        PlayerStates playerStates = player.GetComponent<PlayerStates>();
        monsters = new GameObject[respawnPos.Length];
        for (int i = 0; i < monsters.Length; i++)
        {
            monsters[i] = (Instantiate(monsterType[i%monsterType.Length], respawnPos[i].position , respawnPos[i].rotation));
        }
        
    }
    public void SetTarget(GameObject player) {
        for (int i = 0; i < monsters.Length; i++) {
            PlayerStates monsterStates = monsters[i].GetComponent<PlayerStates>();
            GameObject[] players = new GameObject[1];
            players[0] = player;
            monsterStates.SetTarget(players);
        }
    }
    public GameObject[] GetAllMonster() {
        return monsters;
    }
    public void Fighting(float dt) {
        for(int i = 0; i < monsters.Length; i++)
        {
            if (monsters[i].activeSelf) //only alive monster should fight
            {
                PlayerStates monsterStates = monsters[i].GetComponent<PlayerStates>();
                monsterStates.Fighting(dt);
            }
        }
    }

    public void SetAllMonsterAttributes(Attributes attr)
    {
        foreach (GameObject monster in monsters)
        {
            monster.GetComponent<MonsterStates>().SetAttributes(attr);
        }
    }

    public void SetMonsterType(GameObject mType)
    {
        this.monsterType[0] = mType;
    }
}
