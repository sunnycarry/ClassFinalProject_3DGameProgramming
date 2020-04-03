using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInImage : MonoBehaviour
{
    public float time;
    public float targetAlpha;
    public GameObject panel;

    Image image;
    float initAlpha;
    float curTime;
    // Start is called before the first frame update
    void Start()
    {
        image = this.GetComponent<Image>();
        curTime = 0;
        initAlpha = image.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > time) {
            panel.SetActive(true);
            return;
        }
        Color curColor = image.color;
        curColor.a = Mathf.Lerp(initAlpha, targetAlpha, curTime / time);

        image.color = curColor;
    }

}
