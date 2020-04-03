using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSlerp : MonoBehaviour
{
    public Transform first;
    public Transform second;
    public float journeyLength;

    public void Forward() {
        StartCoroutine(FirstToSecond());
    }
    public void Backward() {
        StartCoroutine(SecondToFirst());
    }
    private IEnumerator FirstToSecond() {
        float journeyCovered = 0;
        while (journeyCovered < journeyLength)
        {
            journeyCovered += Time.deltaTime;
            float fractionOfJourney = journeyCovered / journeyLength;
            this.transform.position = Vector3.Lerp(first.position, second.position, fractionOfJourney);
            this.transform.rotation = Quaternion.Lerp(first.rotation, second.rotation, fractionOfJourney);
            yield return 0;
        }
    }
    private IEnumerator SecondToFirst()
    {
        float journeyCovered = 0;
        while (journeyCovered < journeyLength)
        {
            journeyCovered += Time.deltaTime;
            float fractionOfJourney = journeyCovered / journeyLength;
            this.transform.position = Vector3.Slerp( second.position, first.position, fractionOfJourney);
            this.transform.rotation = Quaternion.Slerp(second.rotation, first.rotation, fractionOfJourney);
            yield return 0;
        }
    }
}
