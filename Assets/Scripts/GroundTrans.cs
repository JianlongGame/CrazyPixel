using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrans : MonoBehaviour {
    public GameObject curGnd;
    private float x;
    private float y;

	public List<GameObject> obstacles = new List<GameObject>();
	public List<Transform> spawnPos = new List<Transform>();
	public List<float> obstacleX = new List<float>();
	public List<float> spawnX = new List<float>();
	public int newPos, lastPos;

	// Use this for initialization
	void Start () {
        curGnd.GetComponent<GameObject>();
        x = curGnd.transform.position.x;
        y = curGnd.transform.position.y;

		obstacleX.Add (-1.5f);
		obstacleX.Add (1.5f);
		obstacleX.Add (0f);
		newPos = 0;
		lastPos = 0;
		setObstacle ();
    }
	
	// Update is called once per frame
	void Update () {
        if (curGnd.transform.position.z <= -20f)
        {
            curGnd.transform.position = new Vector3(x, y, 40f);
			newPos += 1;

        }

		if (lastPos != newPos)
		{
			lastPos = newPos;
			setObstacle ();
		}
    }

	void setObstacle() {
		foreach( Transform pos in spawnPos){
			spawnX.Clear ();
			int obsNumX = Random.Range (0, 2) + 1;
			while (obsNumX > 0) {
				float temp = obstacleX [Random.Range (0, 3)];
				if (!spawnX.Contains(temp)){
					spawnX.Add(temp);
					obsNumX--;
				}
			}
			foreach (float posX in spawnX) {
				GameObject prefab = obstacles [Random.Range (0, 2)];
				pos.position = new Vector3 (posX, pos.position.y, pos.position.z);
				Instantiate (prefab, pos.position, prefab.transform.rotation);
			}
		}
	}
}
