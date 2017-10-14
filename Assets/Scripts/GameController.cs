using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] m_Obstacles;
    [SerializeField] Text m_GameOverText;

    public DeathMenu deathMenu;
    public float speed = 0.001f;
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
    }

    // Start a new game
    void StartGame()
    {
        ClearGame();
        isGameOver = false;
        m_GameOverText.gameObject.SetActive(false);
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

    // Set the speed of the game
    void SetSpeed(float spd)
    {
        foreach (Scrolling s in transform.GetComponentsInChildren<Scrolling>())
        {
            s.movespeed = spd;
        }
    }
}
