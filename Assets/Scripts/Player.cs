using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameController m_GameController;
    [SerializeField] float m_moveSpeed;
    Vector3 m_targetLoc;

    // Initialize the player
    void Start()
    {
        m_targetLoc = transform.position;
    }

    void Update()
    {
        // If the player is changing lanes, move the player
        if (!m_targetLoc.Equals(transform.position))
        {
            Move();
        }
    }

    public void MoveTo(int direction)
    {
        m_targetLoc = new Vector3(transform.position.x + direction, transform.position.y, transform.position.z);
    }

    // Move the player towards their target position
    void Move()
    {
        float move = m_moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, m_targetLoc, move);
    }

    // Player collided with an object (Jay)
    void OnTriggerEnter(Collider other)
    {
		if(other.gameObject.tag != gameObject.name){  //wrong color
			m_GameController.LoseOneLife ();
		}
    }
}
