using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static Object thisLock = new Object();

    [SerializeField] GameObject[] m_Obstacles;
    [SerializeField] Text m_GameOverText;
    [SerializeField] Image[] m_LifeImages;
	[SerializeField] Image[] m_CountDownImages;

	public bool CounterDownDone = false;
    public DeathMenu deathMenu;
	public PauseMenu pauseMenu;
	public Button pauseButton;
    public float speed = 0.001f;
    public bool isGameOver;
	public bool isGamePause;
    public int lifeCount;

	[SerializeField] private Stat energy;
	private const float coef = 1.0f;

	private void Awake()
	{
		energy.Initialize ();
	}

    // Use this for initialization
    void Start()
    {
        StartGame();
    }

	void Update()
	{
		if (CounterDownDone == true) 
		{
			m_CountDownImages[0].gameObject.SetActive(false);
			m_CountDownImages[1].gameObject.SetActive(false);
			m_CountDownImages[2].gameObject.SetActive(false);
			m_CountDownImages[3].gameObject.SetActive(false);
		}

		if (isGamePause == false && CounterDownDone ==true ) {
			energy.CurrentVal -= coef * Time.deltaTime;
		}

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
		isGamePause = false;
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

	public void rightShape()
	{
		//checkShape = true;
		energy.CurrentVal += 10;
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
   
	public void PauseGame()
	{
		isGamePause = true;
		pauseMenu.TogglePauseMenu(); 
		pauseButton.gameObject.SetActive (false);  //hide pause button
		//deathMenu.ToggleEndMenu();
	}

	public void ContinueGame()
	{
		isGamePause = false;
		pauseMenu.ClosePauseMenu(); 
		pauseButton.gameObject.SetActive (true); //show pause button
		//deathMenu.CloseEndMenu();
	}
}
