using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] GroundTrans m_GroundPrefab;
    [SerializeField] Scrolling m_ArchPrefab;
    [SerializeField] Material[] m_Materials;
    [SerializeField] GameObject[] m_Obstacles;
    [SerializeField] Text m_GameOverText;

    public DeathMenu deathMenu;
    public List<GameObject> map;
    public float speed = 0.1f;
    public bool isGameOver;

    // Use this for initialization
    void Start()
    {
        StartGame();
    }

    // Delete the previous game
    void ClearGame()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        map.Clear();
    }

    // Start a new game
    void StartGame()
    {
        ClearGame();

        isGameOver = false;
        m_GameOverText.gameObject.SetActive(false);
        CreateMap();
        SetSpeed(speed);
    }

    // Player has died
    void OnGameOver()
    {
        SetSpeed(0.0f);
        m_GameOverText.gameObject.SetActive(true);
        isGameOver = true;
        deathMenu.ToggleEndMenu();  //show the death button when the player died
    }

    // Add objects to the game
    void CreateMap()
    {
        map = new List<GameObject>();
        AddGround(new Vector3(0, 0, 20), new Vector3(5, 1, 20), m_Materials[0], m_Obstacles);
        AddGround(new Vector3(0, 0, 0), new Vector3(5, 1, 20), m_Materials[1], m_Obstacles);
        AddGround(new Vector3(0, 0, -20), new Vector3(5, 1, 20), m_Materials[2], m_Obstacles);

        Scrolling arch = Instantiate<Scrolling>(m_ArchPrefab);
        arch.transform.parent = transform;
        arch.gameObject.SetActive(true);
        map.Add(arch.gameObject);
    }

    // Add a ground platform
    void AddGround(Vector3 pos, Vector3 scale, Material mat, GameObject[] obs)
    {
        GroundTrans g = Instantiate<GroundTrans>(m_GroundPrefab);
        g.SetPos(pos);
        g.SetScale(scale);
        g.SetMaterial(mat);
        g.SetObstacles(obs);
        g.gameObject.SetActive(true);
        g.transform.parent = transform;
        map.Add(g.gameObject);
    }

    // Set the speed of the game
    void SetSpeed(float spd)
    {
        foreach (Scrolling s in transform.GetComponentsInChildren<Scrolling>())
        {
            s.movespeed = spd;
        }
    }
}
