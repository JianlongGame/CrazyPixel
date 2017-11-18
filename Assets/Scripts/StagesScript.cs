using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagesScript : MonoBehaviour {
	[SerializeField]
	private Text valueText;

	// Use this for initialization
	void Start () {
		if(LoadSceneOnClick.stageNum == 1)
		{
			valueText.text = "Stage  1";
		}
		if(LoadSceneOnClick.stageNum == 2)
		{
			valueText.text = "Stage  2";
		}
		if(LoadSceneOnClick.stageNum == 3)
		{
			valueText.text = "Stage  3";
		}if(LoadSceneOnClick.stageNum == 4)
		{
			valueText.text = "Stage  4";
		}
		if(LoadSceneOnClick.stageNum == 5)
		{
			valueText.text = "Stage  5";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
