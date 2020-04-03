using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBrute : MonoBehaviour
{
    private List<string> animState = new List<string>();
    public Animator anim;
    private bool wPress;
    private bool qPress;
    private bool ePress;
    private bool rPress;
    private bool spacePress;
    // Start is called before the first frame update
    void Start()
    {
        animState.Add("Idle");
        animState.Add("AttackDownward");
        animState.Add("AttackHorizontal");
        animState.Add("ComboAttack");
    }

    // Update is called once per frame
    void Update()
    {
        wPress = Input.GetKeyDown(KeyCode.W);
        ePress = Input.GetKeyDown(KeyCode.E);
        qPress = Input.GetKeyDown(KeyCode.Q);
        rPress = Input.GetKeyDown(KeyCode.R);
        spacePress = Input.GetKeyDown(KeyCode.Space);
    }

    void LateUpdate()
    {
        if (wPress)
            anim.Play(animState[0]);
        else if (ePress)
            anim.Play(animState[1]);
        else if (qPress)
            anim.Play(animState[2]);
        else if (rPress)
            anim.Play(animState[3]);
        else if(spacePress)
            anim.Play("walk");
    }
}
