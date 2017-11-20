using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	public static int stageNum;

	public void LoadsByIndex(int sceneIndex) {
		SceneManager.LoadScene (sceneIndex);
	}

	public void StageNum(int stageIndex) {
        stageNum = stageIndex;
    }
}
