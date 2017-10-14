using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionColorChange : MonoBehaviour {

    // Use this for initialization
    private GameObject ground;
    private Color orginColor;
    public Material colorMaterial; 
   
	void Start () {
		if (gameObject.name == "ColorSensor-1")
        {
            ground = GameObject.Find("Ground/Left");
        }
        else if (gameObject.name == "ColorSensor0")
        {
            ground = GameObject.Find("Ground/Middle");
        }
        else
        {
            ground = GameObject.Find("Ground/Right");
        }
        orginColor = colorMaterial.color;
        ground.GetComponent<Renderer>().material = colorMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        ground.GetComponent<Renderer>().material.color = other.gameObject.GetComponent<Renderer>().material.color;
    }

    private void OnTriggerExit(Collider other)
    {
        ground.GetComponent<Renderer>().material.color = orginColor;
    }
}
