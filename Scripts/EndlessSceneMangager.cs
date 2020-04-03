using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSceneMangager : MonoBehaviour
{
    public static EndlessSceneMangager mManager;
    public GameObject[] roads; //type of roads
    public Transform endlessPoint;

    private int count = 0 ;
    public List<GameObject> roadInstances;
    // Start is called before the first frame update
    void Awake()
    {
        if (mManager == null) {
            mManager = this;
        }
    }

    public void GenerateRoad() {
        int numOfRoads = roads.Length;
        count++;
        roadInstances.Add( Instantiate(roads[count % numOfRoads], endlessPoint.position , endlessPoint.rotation ));    
    }
    public void Move(float time) {
        for (int i = 0; i < roadInstances.Count; i++) {
            Road roadScript = roadInstances[i].GetComponent<Road>();
            roadScript.Move(time);
            if (roadScript.ShouldDestroy())
            {
                roadScript.CreateAndDestroy();
                roadInstances.Remove(roadInstances[i]);
            }

        }
    }

}
