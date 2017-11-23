using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	private float fillAmount;
	[SerializeField]
	private Image energy;
	public float MaxValue { get; set;}
    public static bool genArrow = false;
	public float Value
	{
		set
		{
            int amount = 1;
            if (LoadSceneOnClick.stageNum == 4) {
                amount = 0;
            }
            fillAmount = Map(value, 0, MaxValue, 0, amount);       
		}
	}

	// Use this for initialization
	void Start () {
		//Value = 100;
		if(LoadSceneOnClick.stageNum < 3)
		{
			gameObject.SetActive (false);
            
		} 
	}
	
	// Update is called once per frame
	void Update () {
		HandleBar ();
	}

	private void HandleBar()
	{
//		if(fillAmount != energy.fillAmount)
//		{
//			//energy.fillAmount = Map (50,0,100,0,1);
//			energy.fillAmount = fillAmount;	
//		}
		energy.fillAmount = fillAmount;
        if (fillAmount == 0 || LoadSceneOnClick.stageNum == 4) {
            genArrow = true;
        }
	}

	private float Map(float value, float inMin, float inMax, float outMin,float outMax)
	{
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
