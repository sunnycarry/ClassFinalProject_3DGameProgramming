using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : MonoBehaviour
{
    public Skill[] skills;
    public GameObject[] target;
    int index;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 3.0)
        {
            time = 0;
            skills[index].Used(this.gameObject, target);
        }
    }
}
