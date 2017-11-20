using UnityEngine;

public class GameController : MonoBehaviour
{
    public static Object thisLock = new Object();

    [SerializeField] GameObject[] obstacles;
    [SerializeField] DeathMenu deathMenu;
	[SerializeField] PauseMenu pauseMenu;
    [SerializeField] GameMenu gameMenu;

    public bool isGameOver;
	public bool isGamePaused;
    public bool isCountdownFinished;
    public float speed = 0.001f;
    public int stage;
    public int lives;
    public float time;
    public int score;

    [SerializeField] private Stat energy;
    private const float coef = 2.0f;

    private void Awake()
	{
		energy.Initialize();
	}

    // Use this for initialization
    void Start()
    {
        StartGame();
    }

	void Update()
	{
		if (isGamePaused == false && isCountdownFinished == true && isGameOver == false) {
			energy.CurrentVal -= coef * Time.deltaTime;
            time += Time.deltaTime;
            gameMenu.SetScore(score);
            gameMenu.SetTime((int)time);
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
		isGamePaused = false;
        isCountdownFinished = false;
        Scrolling.gameOver = false;
        GroundTrans.doSpawn = true;
        lives = 2;
        time = 0.0f;
        score = 0;
    }

    public void loseOneLife()
    {
        if (lives >= 0)
        {
            gameMenu.RemoveLife(lives--);
        }
        if (lives < 0)
        {
            OnGameOver();
        }   
    }

    public void winOneLife()
    {
        if (lives < 2)
        {
            gameMenu.AddLife(++lives);
        }
    }

    public void rightShape()
	{
		//checkShape = true;
		energy.CurrentVal += 10;
	}

    // Player has died
    void OnGameOver()
    {
        GroundTrans.doSpawn = false;
        Scrolling.gameOver = true;
        isGameOver = true;
        deathMenu.ToggleEndMenu();  //show the death button when the player died
    }
   
	public void PauseGame()
	{
		isGamePaused = true;
		pauseMenu.TogglePauseMenu();
        gameMenu.Pause(true);
	}

	public void ContinueGame()
	{
		isGamePaused = false;
		pauseMenu.ClosePauseMenu();
        gameMenu.Pause(false);
	}
}
