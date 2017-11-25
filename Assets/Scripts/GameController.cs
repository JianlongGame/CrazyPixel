<<<<<<< HEAD
﻿using UnityEngine;

public class GameController : MonoBehaviour
{
    public static Object thisLock = new Object();

    [SerializeField] GameObject[] obstacles;
    [SerializeField] DeathMenu deathMenu;
	[SerializeField] PauseMenu pauseMenu;
    [SerializeField] GameMenu gameMenu;
    [SerializeField] AudioClip m_music;

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
    AudioSource scoreMusic;

    private void Awake()
	{
		energy.Initialize();
	}

    // Use this for initialization
    void Start()
    {
        StartGame();
        scoreMusic = GetComponent<AudioSource>();
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
        scoreMusic.clip = m_music;
        scoreMusic.Play();
        deathMenu.ToggleEndMenu(score);  //show the death button and pass the score when the player died
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
=======
﻿using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public static Object thisLock = new Object();

    [SerializeField] GameObject[] obstacles;
    [SerializeField] DeathMenu deathMenu;
	[SerializeField] PauseMenu pauseMenu;
    [SerializeField] GameMenu gameMenu;
    [SerializeField] SelectOnInput selectOnInput;
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

    private int[] scores = { 30, 15, 30, 50 };
    private int curStage;
    private void Awake()
	{
		energy.Initialize();
	}

    // Use this for initialization
    void Start()
    {
        curStage = PlayerPrefs.GetInt("stageNum");
        StartGame();
    }

	void Update()
	{
		if (isGamePaused == false && isCountdownFinished == true && isGameOver == false) {
			energy.CurrentVal -= coef * Time.deltaTime;
            time += Time.deltaTime;
            gameMenu.SetScore(score);
            gameMenu.SetTime((int)time);
            scoreCheck(PlayerPrefs.GetInt("stageNum"), score);
        }
    }

    private void scoreCheck(int ind, int score) {
        if(ind == 4 || ind > curStage) {
            return;
        } else if (score >= scores[ind]) {
            PlayerPrefs.SetInt("stageNum", ind + 1);
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
        score = 28;
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
>>>>>>> Stats update
