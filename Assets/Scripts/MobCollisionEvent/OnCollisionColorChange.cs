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
    public static GameObject[] mobs = new GameObject[3];

    private int ind = 0;//Left by default
    
    public static bool[] objsMoved = new bool[3];
	void Start () {
		if (gameObject.name == "ColorSensor-1")
        {
            ground = GameObject.Find("Ground/Left");
            x = -1;
            
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
            x = 1;
            ind = 2;
        }
        orginColor = colorMaterial.color;
        ground.GetComponent<Renderer>().material = colorMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        ground.GetComponent<Renderer>().material.color = other.gameObject.GetComponent<Renderer>().material.color;
        obsMesh = other.gameObject.GetComponent<MeshFilter>().mesh;
        mobs[ind] = other.gameObject;

        if (mobs[0] == mobs[1] && mobs[0] != null ||
            mobs[1] == mobs[2] && mobs[1] != null || LoadSceneOnClick.stageNum < 4) {//Sanity check for merged obstacles
            return;
        }

        //Arrow generating     
        if (!objsMoved[ind]) {          
            lock (arrowSpawnCheck) {

                if (BarScript.genArrow) {
                    //Spawn an arrow
                    spawnArrow(Random.Range((float)0.4, -6));
                    //Set it to be false
                    BarScript.genArrow = false;
                    
                }
            }
        } else {
            objsMoved[ind] = false;
        }
        
    }


    private void OnTriggerExit(Collider other)
    {
        ground.GetComponent<Renderer>().material.color = orginColor;
        
        mobs[ind] = null;
        obsMesh = null;
        
    }

    private void spawnArrow(double z) {
        int arrowDirection = 1;//1 is right, and 0 is pointing left
        if (x == 1) {
            arrowDirection = 0;
        } else if (x == 0) {
            arrowDirection = Random.Range(0, 2);
        }
        GameObject arrow = null;
        Vector3 pos = new Vector3((float)x, 0.1f, (float)z);
        bool goRight = true;
        if (arrowDirection == 0) {
            arrow = Instantiate(Resources.Load("ArrowCollider/ArrowColliderLeft", typeof(GameObject))) as GameObject;
            goRight = false;
        } else {
            arrow = Instantiate(Resources.Load("ArrowCollider/ArrowColliderRight", typeof(GameObject))) as GameObject;
        }
        arrow.GetComponent<OnCollisionSwapLane>().ind = ind;
        arrow.GetComponent<OnCollisionSwapLane>().goRight = goRight;
        arrow.transform.position = pos;

        
    }
}
