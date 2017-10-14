using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour {
    public static float movespeed = 0.05f;
    public static bool gameOver = true;
	void Start () {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, -movespeed);
		if (gameOver || transform.position.z <= -15f)
		{
			Destroy (gameObject);
		}
    }
}
