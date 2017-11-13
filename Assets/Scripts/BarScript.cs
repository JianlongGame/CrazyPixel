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
			//fillAmount = Map (value, 0, MaxValue, 0, 1);	
			fillAmount = Map (value, 0, MaxValue, 0, 0);
		}
	}

	// Use this for initialization
	void Start () {
		//Value = 100;
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
        if (fillAmount == 0) {
            genArrow = true;
        }
	}

	private float Map(float value, float inMin, float inMax, float outMin,float outMax)
	{
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
