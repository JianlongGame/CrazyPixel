using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	public static int stageNum;
	private GameController GC;

	public void LoadsByIndex(int sceneIndex) {
		SceneManager.LoadScene (sceneIndex);
	}

	public void StageNum(int stageIndex) {
        stageNum = stageIndex;
		if(GC)
		GC.curStage = stageNum;
    }

	public void NextStage() {
		
		GC = GameObject.Find ("GameController").GetComponent<GameController> ();
		GC.curStage++;
		stageNum = GC.curStage;
	}
}
