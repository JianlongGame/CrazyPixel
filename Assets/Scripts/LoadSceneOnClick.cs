using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	public void LoadsByIndex(int sceneIndex){
		SceneManager.LoadScene (sceneIndex);
	}
}
