using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectOnInput : MonoBehaviour {

	public EventSystem eventSystem;
	public GameObject selectedObject;
	private bool buttonSelected;
    [SerializeField]
    public GameObject[] stages;
    private int curStage;
    private Color[] colors = new Color[5];
    //Debug
    public bool reset;
    private Color lockedColor = new Color(154F, 154F, 154F);
	// Use this for initialization
	void Start () {
        if (gameObject.name == "MainMenu") {
            for (int i = 0; i < 5; i++) {
                colors[i] = stages[i].GetComponent<Image>().color;
            }
            if (reset) {
                
                PlayerPrefs.SetInt("stageNum", 0);
            }
            curStage = PlayerPrefs.GetInt("stageNum");
            setStages(curStage);       
        }
	}

    private void setStages(int stageNum) {
        for (int i = 0; i <= stageNum; i++) {
            stages[i].GetComponent<Button>().enabled = true;
            stages[i].GetComponent<Image>().color = colors[i];

        }
        for (int i = stageNum + 1; i < 5; i++) {
            stages[i].GetComponent<Image>().color = lockedColor;
        }
        Debug.Log("Stage so far " + stageNum);
    }


	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw ("Vertical") != 0 && buttonSelected == false) {
			eventSystem.SetSelectedGameObject (selectedObject);
			buttonSelected = true;
		}

        if (gameObject.name == "MainMenu" && curStage < PlayerPrefs.GetInt("stageNum")) {
            Debug.Log("Update stageNum " + PlayerPrefs.GetInt("stageNum"));
            stages[++curStage].GetComponent<Button>().enabled = true;
        }
	}

	private void OnDisable(){
		buttonSelected = false;
	}
}
