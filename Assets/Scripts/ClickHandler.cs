using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickHandler : MonoBehaviour {

	private GameController GC;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PauseOnClick(){
		GC = GameObject.Find ("GameController").GetComponent<GameController> ();
		GC.PauseGame();
	}

	public void BackOnClick(){
		GC = GameObject.Find ("GameController").GetComponent<GameController> ();
		GC.ContinueGame();
	}

	public void MainMenuOnClick(int sceneIndex){
		SceneManager.LoadScene (sceneIndex);
	}

	public void RestartOnClick(int sceneIndex){
		SceneManager.LoadScene (sceneIndex);
	}
}
