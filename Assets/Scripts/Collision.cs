﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//public GameObject menu;

	void OnTriggerEnter( Collider other ){
		if(other.gameObject.tag == "Obstacle"){
			Destroy (gameObject);
			SceneManager.LoadScene(0);
			//menu.SetActive (true);
		}
	}
}
