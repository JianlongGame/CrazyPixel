using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour {
    public static float movespeed = 0.05f;
    public static bool gameOver = true;

	private GameController GC;

	void Start () {
		GC = GameObject.Find ("GameController").GetComponent<GameController> ();
    }

    // Update is called once per frame
    void Update()
	{
		if (GC.CounterDownDone == true&&GC.isGamePause==false) 
		{
			transform.Translate(0, 0, -movespeed);
			if (gameOver || transform.position.z <= -15f)
			{
				Destroy (gameObject);
			}
		}
        
    }
}
