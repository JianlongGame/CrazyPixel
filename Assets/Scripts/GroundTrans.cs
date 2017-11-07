using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrans : MonoBehaviour {
	public List<GameObject> obsShape = new List<GameObject>();
	public List<float> lanePos = new List<float>();
	private float startTime, nowTime, snapshot, posX, posZ;
	private Vector3 pos;
	private GameObject prefab, lastObs, tempObs;
	private List<float> xTempList = new List<float>();
	private List<GameObject> obsTempList = new List<GameObject>();
    public static bool doSpawn = true;

	public List<Material> obsColor = new List<Material>();
	private List<Material> colTempList = new List<Material>();
	private Material m;

	public List<float> mergePos = new List<float>();
	public List<Material> merColor = new List<Material>();

	float t1, t2, t3;

    void Start () {
        Debug.Log("Start");
        Scrolling.movespeed = 0.08f;
        startTime = Time.time;
        initObs ();
    }

    // Update is called once per frame
    void Update () {
		if (doSpawn && lastObs.transform.position.z < 40) {
			setObs ();
		}
    }

	void initObs(){

        prefab = obsShape[Random.Range(0, 3)];
        posX = lanePos[Random.Range(0, 3)];
        pos = new Vector3(posX, 0.13f, (posZ + 0 * 20f));
        lastObs = Instantiate(prefab, pos, prefab.transform.rotation);
		lastObs.GetComponent<Renderer> ().material = obsColor [Random.Range (0, 3)];
    }

	void setObs (){
		nowTime = Time.time - startTime;
        int levelCode = 0;
		if (nowTime >= 10 && nowTime < 30) {
			levelCode = (int)Random.Range (0, 3);
		} else if (nowTime >= 30 && nowTime < 40) {
			if (Scrolling.movespeed <= 0.2f && nowTime - snapshot > 20) {
				Scrolling.movespeed += 0.02f;
				snapshot = nowTime;
			} 
			levelCode = (int)Random.Range (0, 5);//
		} else if (nowTime >= 40) {
			levelCode = (int)Random.Range (0, 7);
		}
        
        setLevels(levelCode);
	}


    private void setLevels(int levelCode)
    {
		if (levelCode == 0) {
			firstLevel();
		} else if (levelCode >= 1 && levelCode <= 2) {
			secLevel ();
		} else if (levelCode >= 3 && levelCode <= 4) {
			thrLevel ();

		} else if (levelCode >= 5) {
			mergeColor ();
		}
    }

	// only one obstacle in one row
	void firstLevel(){
		prefab = obsShape [Random.Range (0, 3)];
		posX = lanePos [Random.Range (0, 3)];
		posZ = lastObs.transform.position.z + 20;
		pos = new Vector3 (posX, 0.13f, posZ);
		lastObs = Instantiate (prefab, pos, prefab.transform.rotation);
		lastObs.GetComponent<Renderer> ().material = obsColor [Random.Range (0, 3)];
    }

	// two different color obstacles in one row
	void secLevel(){
		xTempList.Clear ();
		obsTempList.Clear ();
		xTempList = new List<float> (lanePos);
		obsTempList = new List<GameObject> (obsShape);
		colTempList.Clear ();//
		colTempList = new List<Material> (obsColor);//
		posZ = lastObs.transform.position.z + 20;
		for (int j = 0; j < 2; j++) {
			posX = xTempList [Random.Range (0, xTempList.Count)];
			prefab = obsTempList [Random.Range (0, obsTempList.Count)];
			m = colTempList [Random.Range (0, colTempList.Count)];//
			pos = new Vector3 (posX, 0.13f, posZ);
			lastObs = Instantiate (prefab, pos, prefab.transform.rotation);
			lastObs.GetComponent<Renderer> ().material = m;//
			xTempList.Remove (posX);
			obsTempList.Remove (prefab);
			colTempList.Remove (m);//
		}
	}

	// three different color obstacles in one row
	void thrLevel(){
		xTempList.Clear ();
		obsTempList.Clear ();
		xTempList = new List<float> (lanePos);
		obsTempList = new List<GameObject> (obsShape);
		colTempList.Clear ();//
		colTempList = new List<Material> (obsColor);//
		for (int j = 0; j < 3; j++) {
			posX = xTempList [Random.Range (0, xTempList.Count)];
			prefab = obsTempList [j];
			m = colTempList [Random.Range (0, colTempList.Count)];//
			pos = new Vector3 (posX, 0.13f, posZ);
			lastObs = Instantiate (prefab, pos, prefab.transform.rotation);
			lastObs.GetComponent<Renderer> ().material = m;//
			xTempList.Remove (posX);
			colTempList.Remove (m);//
		}
	}

	void mergeColor(){
		int temp = Random.Range (0, 3);
		prefab = obsShape [temp];
		posX = mergePos [Random.Range (0, 2)];
		posZ = lastObs.transform.position.z + 20;
		if (temp == 0) {
			pos = new Vector3 (posX, 0.13f * 2, posZ);
		} else {
			pos = new Vector3 (posX, 0.13f * 4, posZ);
		}
		lastObs = Instantiate (prefab, pos, prefab.transform.rotation);
		lastObs.GetComponent<Renderer> ().material = merColor [Random.Range (0, 3)];
		t1 = lastObs.transform.localScale.x;
		t2 = lastObs.transform.localScale.y;
		t3 = lastObs.transform.localScale.z;
		lastObs.transform.localScale = new Vector3 (t1*4, t2*4, t3*4);
	}

}