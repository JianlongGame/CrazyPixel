using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] Vector3[] m_Lanes;
    [SerializeField] float m_moveSpeed;
    [SerializeField] float m_jumpSpeed;
    int m_currentLane;
    private int m_targetLane;

    // Use this for initialization
    void Start() {
        m_currentLane = m_targetLane = 1;
        transform.position = m_Lanes[m_currentLane];
    }

    // Update is called once per frame
    void Update() {
        if (m_currentLane != m_targetLane) {
            Move();
        }
        else {
            CheckInput();
        }
    }

    // Collision detection
    void OnTriggerEnter(Collider other) {
        // Player collided with an object
        if (other.gameObject.tag == "Obstacle") {
            Destroy(gameObject);
        }
        // Player collided with the ground
        else if (other.gameObject.tag == "Ground") {
            transform.position = m_Lanes[m_currentLane];
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    // Move the player towards their target position
    void Move() {
        float move = m_moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, m_Lanes[m_targetLane], move);
        if (transform.position.Equals(m_Lanes[m_targetLane])) {
            m_currentLane = m_targetLane;
        }
    }

    // 
    void CheckInput() {
        // Move left
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (m_currentLane > 0) {
                m_targetLane = m_currentLane - 1;
            }
        }
        // Move right
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (m_currentLane < m_Lanes.Length - 1) {
                m_targetLane = m_currentLane + 1;
            }
        }
        // Jump
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (transform.position.y == 0.625) {
                GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 1.0f, 0.0f) * m_jumpSpeed, ForceMode.Impulse);
            }
        }
        // Attack
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            // Check for destroyable objects in player attack range
            Vector3 size = GetComponent<Renderer>().bounds.size;
            Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(size.x, size.y, 1.25f), Quaternion.identity, 1 << 11);
            foreach (Collider c in colliders) {
                Destroy(c.gameObject);
            }
        }
    }
}
