using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroy : MonoBehaviour {

	public float LifeTime = 10f;

	// Use this for initialization
	void Start () {
		Invoke ("DestroyObject", LifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void DestroyObject ()
	{
		Destroy (gameObject);
	}
}
