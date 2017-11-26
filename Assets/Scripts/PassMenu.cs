using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TogglePassMenu(){
		gameObject.SetActive (true);
	}

	public void ClosePassMenu(){
		gameObject.SetActive (false);
	}
}
