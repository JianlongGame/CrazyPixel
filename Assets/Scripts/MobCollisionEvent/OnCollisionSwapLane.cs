using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionSwapLane : MonoBehaviour {

    // Use this for initialization
    private bool goRight;
	void Start () {
        if (this.gameObject.name == "ArrowColliderLeft") {
            goRight = false;
        }
	}

    private void OnTriggerExit(Collider other) {
        Destroy(this.gameObject);
    }
}
