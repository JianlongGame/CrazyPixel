using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCountDown : MonoBehaviour {
	private GameController GC;

	public void SetCountDownNow()
	{
		GC = GameObject.Find ("GameController").GetComponent<GameController> ();
		GC.CounterDownDone = true;
	}

}
