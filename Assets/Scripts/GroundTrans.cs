using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrans : MonoBehaviour {
    public GameObject curGnd;
    private float x;
    private float y;

	// Use this for initialization
	void Start () {
        curGnd.GetComponent<GameObject>();
        x = curGnd.transform.position.x;
        y = curGnd.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        if (curGnd.transform.position.z <= -20f)
        {
            curGnd.transform.position = new Vector3(x, y, 40f);
        }
    }
}
