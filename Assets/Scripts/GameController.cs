using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    [SerializeField] Player m_PlayerPrefab;
    [SerializeField] GroundTrans m_GroundPrefab;
    [SerializeField] Scrolling m_ArchPrefab;
    [SerializeField] Material[] m_Materials;
    [SerializeField] GameObject[] m_Obstacles;
    [SerializeField] Text m_GameOverText;

    public List<GroundTrans> map;
    public float speed = 0.1f;

    // Use this for initialization
    void Start () {
        m_GameOverText.gameObject.SetActive(false);
        CreateMap();
        CreatePlayer();
        SetSpeed();
	}
	
	// Update is called once per frame
	void Update () {
        Touch[] touches = Input.touches;
	}

    void CreateMap()
    {
        map = new List<GroundTrans>();
        AddGround(new Vector3(0, 0, 20), new Vector3(5, 1, 20), m_Materials[0], m_Obstacles);
        AddGround(new Vector3(0, 0, 0), new Vector3(5, 1, 20), m_Materials[1], m_Obstacles);
        AddGround(new Vector3(0, 0, -20), new Vector3(5, 1, 20), m_Materials[2], m_Obstacles);
        m_ArchPrefab = Instantiate<Scrolling>(m_ArchPrefab);
        m_ArchPrefab.gameObject.SetActive(true);
    }

    void AddGround(Vector3 pos, Vector3 scale, Material mat, GameObject[] obs)
    {
        GroundTrans g = Instantiate<GroundTrans>(m_GroundPrefab);
        g.SetPos(pos);
        g.SetScale(scale);
        g.SetMaterial(mat);
        g.SetObstacles(obs);
        g.gameObject.SetActive(true);
        map.Add(g);
    }

    void CreatePlayer()
    {
        m_PlayerPrefab = Instantiate<Player>(m_PlayerPrefab);
        m_PlayerPrefab.SetOnGameOver(OnGameOver);
        m_PlayerPrefab.gameObject.SetActive(true);
    }

    void SetSpeed()
    {
        foreach (GroundTrans g in map)
        {
            g.SetSpeed(speed);
        }
        m_ArchPrefab.movespeed = speed;
    }

    void OnGameOver()
    {
        speed = 0.0f;
        SetSpeed();
        m_GameOverText.gameObject.SetActive(true);
    }
}
