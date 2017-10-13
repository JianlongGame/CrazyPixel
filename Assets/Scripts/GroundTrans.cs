using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrans : MonoBehaviour {
    private float x;
    private float y;

	//public List<GameObject> obstacles = new List<GameObject>();
	public List<Transform> spawnPos = new List<Transform>();
	//public List<float> obstacleX = new List<float>();
	//public List<float> spawnX = new List<float>();
	public int newPos, lastPos;


	public List<GameObject> obstacles = new List<GameObject>();
	public List<float> lanePos = new List<float>();
	private float nowTime, posX, posZ;
	private Vector3 pos;
	private GameObject prefab, lastObs, tempObs;
	private List<float> xTempList = new List<float>();
	private List<GameObject> obsTempList = new List<GameObject>();


	//public Transform spawnPos;
	//public GameObject obstacle1;

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
        x = transform.position.x;
        y = transform.position.y;
		//obstacleX.Add (-1.5f);
		//obstacleX.Add (1.5f);
		//obstacleX.Add (0f);
		newPos = 0;
		lastPos = 0;
		//setObstacle ();
		//initObstacle();

		initObs ();

    }

    // Update is called once per frame
    void Update () {
        /*if (transform.position.z <= -20f)
        {
            transform.position = new Vector3(x, y, 40f);
			newPos += 1;
        }

		if (lastPos != newPos)
		{
			lastPos = newPos;
			//setObstacle ();
		}*/

		if (lastObs.transform.position.z < 40) {
			setObs ();
		}
    }

	/*void setObstacle() {
		foreach( Transform pos in spawnPos){
			spawnX.Clear ();
			int obsNumX = Random.Range (0, 2) + 1;
			while (obsNumX > 0) {
				float temp = obstacleX [Random.Range (0, 3)];
				if (!spawnX.Contains(temp)) {
					spawnX.Add(temp);
					obsNumX--;
				}
			}
			foreach (float posX in spawnX) {
				GameObject prefab = obstacles [Random.Range (0, 2)];
				pos.position = new Vector3 (posX, pos.position.y, pos.position.z);
				GameObject newObstacle = Instantiate (prefab, pos.position, prefab.transform.rotation);
                newObstacle.transform.parent = transform;
			}
		}
	}*/

	/*void initObstacle(){
		/*int numOfObstacles = (int)(Time.time/10)+1;
		if (numOfObstacles > 3) {
			numOfObstacles = 3;
		}*/

		//spawnPos.position = new 
		/*Vector3 pos = new Vector3(0,0.75f,0);
		for(int i=0; i*2<40; i++){
			pos = new Vector3(0,0.13f,(i*2f));
			GameObject newObstacle = Instantiate (obstacles[2], pos, obstacles[2].transform.rotation);
			newObstacle.transform.parent = transform;
		}

	}*/

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

	void firstLevel(){
		prefab = obstacles [Random.Range (0, 3)];
		posX = lanePos [Random.Range (0, 3)];
		posZ = lastObs.transform.position.z + 4;
		pos = new Vector3 (posX, 0.13f, posZ);
		lastObs = Instantiate (prefab, pos, prefab.transform.rotation);
	}

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
