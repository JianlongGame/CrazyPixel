using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] TouchListener m_TouchListener;
    [SerializeField] Vector3[] m_Lanes;
    [SerializeField] float m_moveSpeed;
    [SerializeField] float m_jumpSpeed;

    public delegate void OnGameOver();
    int m_currentLane;
    int m_targetLane;
    bool m_IsDead;
    OnGameOver m_OnGameOver;

    // Update is called once per frame
    void Update()
    {
        if (m_currentLane != m_targetLane && !m_IsDead)
        {
            Move();
        }
    }

    public void SetOnGameOver(OnGameOver cb)
    {
        m_OnGameOver = cb;
    }

    public void Init()
    {
        m_IsDead = false;
        m_currentLane = m_targetLane = 1;
        transform.position = m_Lanes[m_currentLane];
        m_TouchListener.AddOnTouchCallback(OnTouch);
    }

    // Collision detection
    void OnTriggerEnter(Collider other)
    {
        // Player collided with an object
        if (other.gameObject.tag == "Obstacle")
        {
            m_OnGameOver();
            m_IsDead = true;
        }
        // Player collided with the ground
        else if (other.gameObject.tag == "Ground")
        {
            transform.position = m_Lanes[m_currentLane];
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    // Move the player towards their target position
    void Move()
    {
        float move = m_moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, m_Lanes[m_targetLane], move);
        if (transform.position.Equals(m_Lanes[m_targetLane]))
        {
            m_currentLane = m_targetLane;
        }
    }

    void OnTouch(MyTouch touch)
    {
        if (m_currentLane == m_targetLane && !m_IsDead)
        {
            switch (touch.type)
            {
                // Attack
                case TouchType.Tap:
                    Vector3 size = GetComponent<Renderer>().bounds.size;
                    Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(size.x, size.y, 1.25f), Quaternion.identity, 1 << 11);
                    foreach (Collider c in colliders)
                        Destroy(c.gameObject);
                    break;
                // Move left
                case TouchType.SwipeLeft:
                    if (m_currentLane > 0)
                        m_targetLane = m_currentLane - 1;
                    break;
                // Move right
                case TouchType.SwipeRight:
                    if (m_currentLane < m_Lanes.Length - 1)
                        m_targetLane = m_currentLane + 1;
                    break;
                // Jump
                case TouchType.SwipeUp:
                    if (transform.position.y == 0.625)
                        GetComponent<Rigidbody>().AddForce(Vector3.up * m_jumpSpeed, ForceMode.Impulse);
                    break;
            }
        }
    }
}
