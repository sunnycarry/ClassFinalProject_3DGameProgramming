using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [SerializeField]
    private Text progressText;

    [SerializeField]
    private Image progreeBar;

    // Start is called before the first frame update
    void Start()
    {
        progreeBar.fillAmount = 0;
        StartCoroutine("Loading");
    }


    private IEnumerator Loading()
    {
        int displayProgress = 0;
        int toProgress = 0;
        AsyncOperation op = SceneManager.LoadSceneAsync(GameFSM._instance.nextScene);
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            toProgress = (int)op.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();
            }
        }

        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            SetLoadingPercentage(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;

    }

    private void SetLoadingPercentage(int p)
    {
        progressText.text = p.ToString();
        progreeBar.fillAmount = p / 100.0f;
    }
}