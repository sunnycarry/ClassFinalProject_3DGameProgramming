using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutText : MonoBehaviour
{
    public float time;
    public float targetAlpha;

    Text text;
    float initAlpha;
    float curTime;
    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<Text>();
        curTime = 0;
        initAlpha = text.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > time) return;
        Color curColor = text.color;
        curColor.a = Mathf.Lerp(initAlpha, targetAlpha, curTime / time);

        text.color = curColor;
    }
}
