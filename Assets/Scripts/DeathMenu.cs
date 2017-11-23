using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour {
	[SerializeField] private Text valueText;
	// Use this for initialization
	void Start () {
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToggleEndMenu(int score){
		gameObject.SetActive(true);
		valueText.text = "Your Score:  " + score;
	}

	public void CloseEndMenu(){
		gameObject.SetActive(false);
	}

	public void LoadsMainMenu(int sceneIndex){
		SceneManager.LoadScene(sceneIndex);
	}
}
