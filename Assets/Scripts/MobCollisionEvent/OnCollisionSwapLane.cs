using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionSwapLane : MonoBehaviour {

    // Use this for initialization
    public bool goRight;
    public int ind;

    private void OnTriggerEnter(Collider other) {
        this.GetComponent<BoxCollider>().isTrigger = false;
        int ind2 = ind;
        if (goRight) {
            ind2++;
        } else {
            ind2--;
        }
        OnCollisionColorChange.objsMoved[ind] = true;
        OnCollisionColorChange.objsMoved[ind2] = true;

        swapLanes(OnCollisionColorChange.mobs[ind], OnCollisionColorChange.mobs[ind2]);
        
        if (OnCollisionColorChange.mobs[ind2] == null) {
            OnCollisionColorChange.objsMoved[ind] = false;
        }

        Destroy(this.gameObject);

    }

    //obj1 is not going to be null
    private void swapLanes(GameObject obj1, GameObject obj2) {
        float move = 50;
        Vector3 pos1 = obj1.transform.position;
        Vector3 pos2 = pos1;
        if (obj2 != null) {
            pos2 = obj2.transform.position;

        } else if (goRight) {
            pos2.x++;
        } else {
            pos2.x--;
        }

        

        obj1.transform.position = Vector3.MoveTowards(obj1.transform.position, pos2, move * Time.deltaTime);
        if (obj2 != null) {
            obj2.transform.position = Vector3.MoveTowards(obj2.transform.position, pos1, move * Time.deltaTime);
        } 
    }
}
