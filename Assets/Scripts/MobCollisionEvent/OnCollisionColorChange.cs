using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Script for Color sensor,
 * Collision to match color and generate arrows if bar is low
 */
public class OnCollisionColorChange : MonoBehaviour {

    // Use this for initialization
    private GameObject ground;
    private double x = 0;
    private Color orginColor;
    public Material colorMaterial; 

	public Mesh obsMesh;
    
    private static object arrowSpawnCheck = new object();
    public static int[] mobs = new int[] { 0, 0, 0 };
    private int ind = 0;
	void Start () {
		if (gameObject.name == "ColorSensor-1")
        {
            ground = GameObject.Find("Ground/Left");
            x = -1.15;
            
        }
        else if (gameObject.name == "ColorSensor0")
        {
            ground = GameObject.Find("Ground/Middle");
            x = 0;
            ind = 1;
        }
        else
        {
            ground = GameObject.Find("Ground/Right");
            x = 1.15;
            ind = 2;
        }
        orginColor = colorMaterial.color;
        ground.GetComponent<Renderer>().material = colorMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        mobs[ind] = 1;
        ground.GetComponent<Renderer>().material.color = other.gameObject.GetComponent<Renderer>().material.color;
		obsMesh = other.gameObject.GetComponent<MeshFilter> ().mesh;
        lock(arrowSpawnCheck) {
            if (BarScript.genArrow) {
                //Spawn an arrow
                spawnArrow(Random.Range((float)0.4, -6));
                //Set it to be false
                BarScript.genArrow = false;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        ground.GetComponent<Renderer>().material.color = orginColor;
        mobs[ind] = 0;
		obsMesh = null;
    }

    private void spawnArrow(double z) {
        int arrowDirection = 1;//1 is right, and 0 is pointing left
        if (x == 1.15) {
            arrowDirection = 0;
        } else if (x == 0) {
            arrowDirection = Random.Range(0, 2);
        }
        GameObject arrow = null;
        if (arrowDirection == 0) {
            arrow = GameObject.Find("ArrowColliderLeft");
        } else {
            arrow = GameObject.Find("ArrowColliderRight");
        }
        Vector3 pos = new Vector3((float)x, 0, (float)z);
        Instantiate(arrow, pos, arrow.transform.rotation);
    }
}
