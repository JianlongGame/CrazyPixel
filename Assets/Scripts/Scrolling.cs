using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour {
    public GameObject curPrefab;
    public float movespeed = 0.1f;
	// Use this for initialization
	void Start () {
        curPrefab.GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, -movespeed);
    }
	
}
