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
    public static bool doSpawn = true;

    public List<Material> obsColor = new List<Material>();
    private List<Material> colTempList = new List<Material>();
    private Material m;

    public List<float> mergePos = new List<float>();
    public List<Material> merColor = new List<Material>();

    float t1, t2, t3;
    void Start() {
        Debug.Log("Start");
        Scrolling.movespeed = 0.08f;
        startTime = Time.time;
        initObs();
    }

    // Update is called once per frame
    void Update() {
        if (doSpawn && lastObs.transform.position.z < 40) {//Generate obstacles
            int levelCode = setSpeed();
            stageChoice(levelCode);
        }
    }

    void initObs() {
        prefab = obsShape[Random.Range(0, 3)];
        posX = lanePos[Random.Range(0, 3)];
        pos = new Vector3(posX, 0.13f, 0);
        lastObs = Instantiate(prefab, pos, prefab.transform.rotation);
        lastObs.GetComponent<Renderer>().material = obsColor[Random.Range(0, 3)];
    }

    void stageChoice(int levelCode){
    	if(LoadSceneOnClick.stageNum!=5 && levelCode >=5){
    		levelCode = Random.Range(0, 5);
    	}
    	switch(LoadSceneOnClick.stageNum){
    		case 1 : //matchColor
    		case 4 : //changeLane
    			obsNum(1,levelCode);
    			break;
    		case 2 :
    			mergeObstacle(1);
    			break;
    		case 3 : //matchShape
    		case 5 : //mixStage
    			obsNum(3,levelCode);
    			break;
    		default:
    			obsNum(1,levelCode);//matchColor
    			break;
    	}
    }

    int setSpeed(){
    	nowTime = Time.time - startTime;
        int levelCode = 0;
        if (nowTime >= 10 && nowTime < 30) {
            levelCode = Random.Range(0, 3);
        } else if (nowTime >= 30 && nowTime < 40) {
            if (Scrolling.movespeed <= 0.2f && nowTime - snapshot > 20) { //speed always goes up, cannot stop icreasing.
                Scrolling.movespeed += 0.01f;
                snapshot = nowTime;
            }
            levelCode = Random.Range(0, 5);
        } else if (nowTime >= 40) {
            levelCode = Random.Range(0, 7);
        }
        return levelCode;
    }

    void obsNum(int shape, int levelCode){
    	if (levelCode == 0) {
            spwanObstacle(shape,1);
        } else if (levelCode >= 1 && levelCode <= 2) {
            spwanObstacle(shape,2);
        } else if (levelCode >= 3 && levelCode <= 4) {
            spwanObstacle(shape,3);
        } else if (levelCode >= 5) {
            mergeObstacle(shape);
        }
    }

    void spwanObstacle(int shape,int num) {
    	xTempList.Clear();
        colTempList.Clear();//
        xTempList = new List<float>(lanePos);
        colTempList = new List<Material>(obsColor);//
        posZ = lastObs.transform.position.z + 20;
        for (int j = 0; j < num; j++) {
            posX = xTempList[Random.Range(0, xTempList.Count)];
            prefab = obsShape[Random.Range(3-shape, 3)];
            m = colTempList[Random.Range(0, colTempList.Count)];//
            pos = new Vector3(posX, 0.13f, posZ);
            lastObs = Instantiate(prefab, pos, prefab.transform.rotation);
            lastObs.GetComponent<Renderer>().material = m;//
            xTempList.Remove(posX);
            colTempList.Remove(m);//
        }
    }

    void mergeObstacle(int shape) {
    	int temp = 2;
    	if(shape != 1){
    		temp = Random.Range(0, 3);
    	}
        prefab = obsShape[temp];
        posX = mergePos[Random.Range(0, 2)];
        posZ = lastObs.transform.position.z + 20;
        if (temp == 0) {
            pos = new Vector3(posX, 0.13f * 2, posZ);
        } else {
            pos = new Vector3(posX, 0.13f * 4, posZ);
        }
        lastObs = Instantiate(prefab, pos, prefab.transform.rotation);
        lastObs.GetComponent<Renderer>().material = merColor[Random.Range(0, 3)];
        t1 = lastObs.transform.localScale.x;
        t2 = lastObs.transform.localScale.y;
        t3 = lastObs.transform.localScale.z;
        lastObs.transform.localScale = new Vector3(t1 * 4, t2 * 4, t3 * 4);
    }
}