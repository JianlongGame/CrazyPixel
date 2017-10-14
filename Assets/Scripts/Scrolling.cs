using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour {
    public float movespeed = 0.05f;

	void Start () {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, -movespeed);
		if (transform.position.z <= -15f)
		{
			Destroy (gameObject);
		}
    }
}
