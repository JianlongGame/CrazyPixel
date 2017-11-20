using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToggleEndMenu(){
		gameObject.SetActive(true);
	}

	public void CloseEndMenu(){
		gameObject.SetActive(false);
	}

	public void LoadsMainMenu(int sceneIndex){
		SceneManager.LoadScene(sceneIndex);
	}
}
