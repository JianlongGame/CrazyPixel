using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static Object thisLock = new Object();

    [SerializeField] GameObject[] m_Obstacles;
    [SerializeField] Text m_GameOverText;
    [SerializeField] Image[] m_LifeImages;

    public DeathMenu deathMenu;
    public float speed = 0.001f;
    public bool isGameOver;
    public int lifeCount;
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
        
    }

    // Start a new game
    void StartGame()
    {
        ClearGame();
        isGameOver = false;
        Scrolling.gameOver = false;
        GroundTrans.doSpawn = true;
        m_GameOverText.gameObject.SetActive(false);
        //SetSpeed(speed);
        lifeCount = 2;
    }

    public void loseOneLife()
    {
        if (lifeCount >= 0)
        {
            m_LifeImages[lifeCount--].gameObject.SetActive(false);
        }
        if (lifeCount < 0)
        {
            OnGameOver();
        }   
    }

    public void winOneLife()
    {  
        if (lifeCount < 2)
        {
            m_LifeImages[++lifeCount].gameObject.SetActive(true);
        }  
    }

    // Player has died
    void OnGameOver()
    {
        //SetSpeed(0.0f);
        GroundTrans.doSpawn = false;
        Scrolling.gameOver = true;
        m_GameOverText.gameObject.SetActive(true);
        isGameOver = true;
        deathMenu.ToggleEndMenu();  //show the death button when the player died
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
