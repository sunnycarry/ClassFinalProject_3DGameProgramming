using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 velocity = new Vector3(0, 0, -5);

    public float destroyZ = -30;
    public void Move(float time)
    {
        this.transform.Translate(velocity * time);
 
    }
    public bool ShouldDestroy() {
        return (this.transform.position.z < destroyZ);
    }
    public void CreateAndDestroy() {
        Debug.Log("Create new road.");
        EndlessSceneMangager.mManager.GenerateRoad();
        Destroy(this.gameObject);
    }
}
