using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void ChangeGameState(int nextState) {
        GameFSM._instance.Transition(nextState);
    }

}