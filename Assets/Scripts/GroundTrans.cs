using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundTrans : MonoBehaviour {
	public List<GameObject> obstacles = new List<GameObject>();
	public List<GameObject> lanes = new List<GameObject>();
	private float nowTime, posX, posZ;
	private Vector3 pos;
	private GameObject prefab, lastObs, tempObs;
	private List<float> xTempList = new List<float>();
	private List<GameObject> obsTempList = new List<GameObject>();

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
			prefab = obstacles[Random.Range(0, obstacles.Count)];
            int lane = Random.Range(0, lanes.Count);
            posX = lanes[lane].transform.position.x;
			pos = new Vector3 (posX, 0.13f, (posZ + i*4f));
			lastObs = Instantiate (prefab, pos, prefab.transform.rotation);
            lastObs.transform.parent = lanes[lane].transform;
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
        prefab = obstacles[Random.Range(0, obstacles.Count)];
        int lane = Random.Range(0, lanes.Count);
        posX = lanes[lane].transform.position.x;
		posZ = lastObs.transform.position.z + 4;
		pos = new Vector3(posX, 0.13f, posZ);
		lastObs = Instantiate (prefab, pos, prefab.transform.rotation);
        lastObs.transform.parent = lanes[lane].transform;
	}

	// two different color obstacles in one row
	void secLevel(){
		xTempList.Clear ();
		obsTempList.Clear ();
		xTempList = new List<float> (lanes.Select(x => x.transform.position.x));
		obsTempList = new List<GameObject> (obstacles);
		posZ = lastObs.transform.position.z + 4;
		for (int j = 0; j < 2; j++) {
            int lane = Random.Range(0, xTempList.Count);
            posX = xTempList [lane];
			prefab = obsTempList [Random.Range (0, obsTempList.Count)];
			pos = new Vector3 (posX, 0.13f, posZ);
			lastObs = Instantiate (prefab, pos, prefab.transform.rotation);
            lastObs.transform.SetParent(lanes[lane].transform);
            xTempList.Remove (posX);
			obsTempList.Remove (prefab);
		}
	}

	// three different color obstacles in one row
	void thrLevel(){
		xTempList.Clear ();
		obsTempList.Clear ();
		xTempList = new List<float> (lanes.Select(x => x.transform.position.x));
		obsTempList = new List<GameObject> (obstacles);
		for (int j = 0; j < 3; j++) {
            int lane = Random.Range(0, xTempList.Count);
            posX = xTempList[lane];
            prefab = obsTempList [j];
			pos = new Vector3 (posX, 0.13f, posZ);
            lastObs = Instantiate(prefab, pos, prefab.transform.rotation);
            lastObs.transform.parent = lanes[lane].transform;
            xTempList.Remove (posX);
		}
	}
}