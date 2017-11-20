﻿using UnityEngine;

public enum PlayerColors
{
    Red,        // 0
    Yellow,     // 1
    Blue,       // 2
    Orange,     // 3
    Purple,     // 4
    Green,      // 5
    White,      // 6
}

public enum PlayerShapes
{
    Cube,
    Sphere,
    Cylinder,
}

public class Player : MonoBehaviour
{
    [SerializeField] GameController m_GameController;
    [SerializeField] float m_moveSpeed;
    [SerializeField] Material[] m_colors;
    [SerializeField] Mesh[] m_shapes;

    public string origColor;
    public bool isFused;
    Vector3 m_targetLoc;
    Player fusedWith;
    int curShape;

    // Initialize the player
    void Start()
    {
        m_targetLoc = transform.position;
        curShape = 0;
        isFused = false;
    }

    void Update()
    {
        // If the player is changing lanes, move the player
        if (!m_targetLoc.Equals(transform.position))
        {
            Move();
        }
    }

    public string GetColor()
    {
        return LayerMask.LayerToName(gameObject.layer);
    }

    public void ChangeShape()
    {
        curShape = (curShape + 1) % 3;
        Vector3 scale = gameObject.transform.localScale;
        if (curShape == (int)PlayerShapes.Cylinder)
            gameObject.transform.localScale = new Vector3(scale.x, 0.125f, scale.z);
        else if (curShape == (int)PlayerShapes.Cube)
            gameObject.transform.localScale = new Vector3(scale.x, 0.25f, scale.z);
        gameObject.GetComponent<MeshFilter>().mesh = m_shapes[curShape];

        if (isFused && fusedWith.curShape != curShape)
            fusedWith.ChangeShape();
    }

    public void MoveTo(float direction)
    {
        m_targetLoc = new Vector3(transform.position.x + direction, transform.position.y, transform.position.z);
    }

    public void Fuse(bool fused, string color, Player other)
    {
        isFused = fused;
        fusedWith = other;
        gameObject.layer = LayerMask.NameToLayer(color);
        gameObject.GetComponent<MeshRenderer>().material = m_colors[(int)System.Enum.Parse(typeof(PlayerColors), color)];
        if (fused)
        {
            gameObject.transform.localScale = Vector3.Scale(gameObject.transform.localScale, new Vector3(4f, 1f, 1f));
            curShape = 0;
            Vector3 scale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(scale.x, 0.25f, scale.z);
            gameObject.GetComponent<MeshFilter>().mesh = m_shapes[curShape];
        }
        else
        {
            gameObject.transform.localScale = Vector3.Scale(gameObject.transform.localScale, new Vector3(0.25f, 1f, 1f));
        }
    }

    // Move the player towards their target position
    void Move()
    {
        float move = m_moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, m_targetLoc, move);
    }

    // Player collided with an object
    void OnTriggerEnter(Collider other)
    {
        Color objectColor = other.gameObject.GetComponent<Renderer>().material.color;

        // wrong colour
        if (objectColor != gameObject.GetComponent<Renderer>().material.color)
        {
			lock(GameController.thisLock)
            {
                m_GameController.loseOneLife();
            }
        }
        // right color
        else
        {
            // right shape
            if (other.gameObject.GetComponent<MeshFilter>().mesh.name == gameObject.GetComponent<MeshFilter>().mesh.name) {
				lock (GameController.thisLock)
				{
					m_GameController.rightShape();
				}
			}
            lock (GameController.thisLock)
            {
                m_GameController.score += 1;
                m_GameController.winOneLife();
            }
        }
    }
}
