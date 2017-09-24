using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] float m_laneDistance;
    [SerializeField] float m_jumpHeight;
    [SerializeField] float m_moveSpeed;
    private Vector3 m_targetPosition;

    // Use this for initialization
    void Start() {
        m_targetPosition = transform.position;
        Physics.gravity = new Vector3(0, -10.0f, 0);
    }

    // Update is called once per frame
    void Update() {
        Move();
        CheckInput();
    }

    // Collision detection
    void OnTriggerEnter(Collider other) {
        // Player collided with an object
        if (other.gameObject.tag == "Obstacle") {
            Destroy(gameObject);
        }
        // Player collided with the ground
        else if (other.gameObject.tag == "Ground") {
            transform.position = new Vector3(transform.position.x, 0.625f, transform.position.z);
            m_targetPosition = transform.position;
        }
    }

    // Move the player towards their target position
    void Move() {
        float move = m_moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, m_targetPosition, move);
    }

    // 
    void CheckInput() {
        // Move left
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (transform.position.x > -m_laneDistance) {
                m_targetPosition = transform.position - new Vector3(m_laneDistance, 0, 0);
            }
        }
        // Move right
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (transform.position.x < m_laneDistance) {
                m_targetPosition = transform.position + new Vector3(m_laneDistance, 0, 0);
            }
        }
        // Jump
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (transform.position.x < m_laneDistance) {
                m_targetPosition = transform.position + new Vector3(0, m_jumpHeight, 0);
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
