using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadSceneIndex(int stageId)
    {
        SceneManager.LoadScene(stageId, LoadSceneMode.Single);
    }
}
