using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillOrder : MonoBehaviour
{
    public GameObject[] skills;
    public GameObject imgFollowTouch;
    public int skillIndex;
    public Attributes skillAttribute;
    public SkillManager skillMgr;

    private bool isDown;
    private Sprite curImg;
    private Sprite targetImg;

    // Start is called before the first frame update
    void Start()
    {
        isDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDown)
        {
            //imgFollowTouch.GetComponent<RectTransform>().transform.position = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            imgFollowTouch.GetComponent<RectTransform>().transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    public void pointDown()
    {
        isDown = true;
        imgFollowTouch.SetActive(true);
        // set image
        curImg = this.gameObject.GetComponent<Image>().sprite;
        imgFollowTouch.GetComponent<Image>().sprite = curImg;
    }

    public void pointUp()
    {
        imgFollowTouch.SetActive(false);
        if (!isDown)
        {
            isDown = false;
            return;
        }

        isDown = false;

        // find minimum dist
        float minDist = 1000;
        int targetIdx = 0;
        //Vector3 mousePos = new Vector3(Input.GetTouch(0).position.x,Input.GetTouch(0).position.y);
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        for (int i = 0; i < skills.Length; ++i)
        {
            float tmpDist = Vector3.Distance(skills[i].GetComponent<RectTransform>().transform.position, mousePos);
            if (tmpDist < minDist)
            {
                targetIdx = i;
                minDist = tmpDist;
            }
        }

        // swap picture
        skillMgr.SwapSkillImage(targetIdx, skillIndex);
        // record this operation
        skillMgr.SwapSkillOrderArray(targetIdx, skillIndex);
        // swap skills
        skillMgr.SwapPlayerSkill(targetIdx, skillIndex);
    }

}
