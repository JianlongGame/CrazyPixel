using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrans : MonoBehaviour {
	public List<GameObject> obstacles = new List<GameObject>();
	public List<float> lanePos = new List<float>();
	private float nowTime, posX, posZ;
	private Vector3 pos;
	private GameObject prefab, lastObs, tempObs;
	private List<float> xTempList = new List<float>();
	private List<GameObject> obsTempList = new List<GameObject>();

    public void SetPos(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    public void SetMaterial(Material mat)
    {
        GetComponent<MeshRenderer>().material = mat;
    }

    public void SetObstacles(GameObject[] obs)
    {
        obstacles = new List<GameObject>(obs);
    }

    void Start () {
		initObs ();
    }

    // Update is called once per frame
    void Update () {
		if (lastObs.transform.position.z < 40) {
			setObs ();
		}
    }

	void initObs(){
		for (int i = 0; i < 15; i++) {
			prefab = obstacles [Random.Range (0, 3)];
			posX = lanePos [Random.Range (0, 3)];
			pos = new Vector3 (posX, 0.13f, (posZ + i*4f));
			lastObs = Instantiate (prefab, pos, prefab.transform.rotation);
		}
	}

	void setObs (){
		nowTime = Time.time;
		if (nowTime < 15) {
			firstLevel ();
		} else if (nowTime >= 15 && nowTime < 30) {
			secLevel ();
		} else {
			thrLevel ();
		}
	}

	// only one obstacle in one row
	void firstLevel(){
		prefab = obstacles [Random.Range (0, 3)];
		posX = lanePos [Random.Range (0, 3)];
		posZ = lastObs.transform.position.z + 4;
		pos = new Vector3 (posX, 0.13f, posZ);
		lastObs = Instantiate (prefab, pos, prefab.transform.rotation);
	}

	// two different color obstacles in one row
	void secLevel(){
		xTempList.Clear ();
		obsTempList.Clear ();
		xTempList = new List<float> (lanePos);
		obsTempList = new List<GameObject> (obstacles);
		posZ = lastObs.transform.position.z + 4;
		for (int j = 0; j < 2; j++) {
			posX = xTempList [Random.Range (0, xTempList.Count)];
			prefab = obsTempList [Random.Range (0, obsTempList.Count)];
			pos = new Vector3 (posX, 0.13f, posZ);
			lastObs = Instantiate (prefab, pos, prefab.transform.rotation);
			xTempList.Remove (posX);
			obsTempList.Remove (prefab);
		}
	}

	// three different color obstacles in one row
	void thrLevel(){
		xTempList.Clear ();
		obsTempList.Clear ();
		xTempList = new List<float> (lanePos);
		obsTempList = new List<GameObject> (obstacles);
		for (int j = 0; j < 3; j++) {
			posX = xTempList [Random.Range (0, xTempList.Count)];
			prefab = obsTempList [j];
			pos = new Vector3 (posX, 0.13f, posZ);
			lastObs = Instantiate (prefab, pos, prefab.transform.rotation);
			xTempList.Remove (posX);
		}
	}
}