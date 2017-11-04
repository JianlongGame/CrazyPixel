using UnityEngine;

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
    }

    public void MoveTo(float direction)
    {
        m_targetLoc = new Vector3(transform.position.x + direction, transform.position.y, transform.position.z);
    }

    public void Fuse(bool fused, string color)
    {
        isFused = fused;
        gameObject.layer = LayerMask.NameToLayer(color);
        gameObject.GetComponent<MeshRenderer>().material = m_colors[(int)System.Enum.Parse(typeof(PlayerColors), color)];
        if (fused)
            gameObject.transform.localScale = Vector3.Scale(gameObject.transform.localScale, new Vector3(2f, 1f, 1f));
        else
            gameObject.transform.localScale = Vector3.Scale(gameObject.transform.localScale, new Vector3(0.5f, 1f, 1f));
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
        if (other.gameObject.GetComponent<Renderer>().material.color != gameObject.GetComponent<Renderer>().material.color) //wrong colour
        {
            lock(GameController.thisLock)
            {
                m_GameController.loseOneLife();
            }
        }
        else
        {
            lock (GameController.thisLock)
            {
                m_GameController.winOneLife();
            }
        }
    }
}
