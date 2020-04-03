using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampOnTerrain : MonoBehaviour
{
    // Start is called before the first frame update
    float InitOffset;
    private void Awake()
    {
        Ray mRay = new Ray(this.transform.position, Vector3.down);
        Ray upRay = new Ray(this.transform.position, Vector3.up);
        RaycastHit hit;
        if (Physics.Raycast(mRay, out hit))
        {
            InitOffset = (transform.position - hit.point).y;
        }
        else if (Physics.Raycast(upRay, out hit)) {
            InitOffset = (transform.position - hit.point).y;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray mRay = new Ray(this.transform.position, Vector3.down);
        Ray upRay = new Ray(this.transform.position, Vector3.up);
        RaycastHit hit;
        if (Physics.Raycast(mRay, out hit))
        {
            Vector3 newPos = transform.position;
            newPos.y = hit.point.y + InitOffset;
            transform.position = newPos;

        }
        else if (Physics.Raycast(upRay, out hit))
        {
            Vector3 newPos = transform.position;
            newPos.y = hit.point.y + InitOffset;
            transform.position = newPos;
        }
        else {
            Debug.Log("No!");
        }
    }
}
