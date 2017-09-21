using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] float m_laneDistance;
    [SerializeField] float m_moveSpeed;
    [SerializeField] float m_attackSpeed;
    private Vector3 m_TargetPosition;

    // Use this for initialization
    void Start() {
        m_TargetPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        // Player is currently changing lanes
        if (!transform.position.Equals(m_TargetPosition)) {
            ChangeLanes();
        }
        else {
            CheckInput();
        }
    }

    // Move the player towards their target position
    void ChangeLanes() {
        float move = m_moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, m_TargetPosition, move);
    }

    // Check if the player should move
    void CheckInput() {
        // Move left
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (transform.position.x > -m_laneDistance) {
                m_TargetPosition = transform.position - new Vector3(m_laneDistance, 0, 0);
            }
        }
        // Move right
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (transform.position.x < m_laneDistance) {
                m_TargetPosition = transform.position + new Vector3(m_laneDistance, 0, 0);
            }
        }
        // Attack left
        else if (Input.GetKeyDown(KeyCode.A)) {

        }
        // Attack middle
        else if (Input.GetKeyDown(KeyCode.S)) {

        }
        // Attack right
        else if (Input.GetKeyDown(KeyCode.D)) {

        }
    }
}
