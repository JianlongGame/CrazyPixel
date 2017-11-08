using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeChangeNotice : MonoBehaviour {
	[SerializeField] OnCollisionColorChange[] m_GoundCollisions;
	[SerializeField] PlayerController m_PlayerController;
	[SerializeField] GameObject[] m_ShapeDisplay;

	// Use this for initialization
	void Start () {
		for(int i=0;i<3;i++){
			m_ShapeDisplay [i].gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for(int i=0;i<3;i++){
			if (m_GoundCollisions [i].obsMesh) {
				string curObs = m_GoundCollisions [i].obsMesh.name;
				string curPlayer = m_PlayerController.m_PlayerObjects [i].GetComponent<MeshFilter> ().mesh.name;
				if (curObs != curPlayer) {
					m_ShapeDisplay [i].gameObject.SetActive (true);
					m_ShapeDisplay [i].GetComponent<MeshFilter> ().mesh = m_GoundCollisions [i].obsMesh;
				} else {
					m_ShapeDisplay [i].gameObject.SetActive (false);
				}
			} else {
				m_ShapeDisplay [i].gameObject.SetActive (false);
			}
		}
	}
}
