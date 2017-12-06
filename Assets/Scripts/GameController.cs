
﻿using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public static Object thisLock = new Object();

    [SerializeField] GameObject[] obstacles;
    [SerializeField] DeathMenu deathMenu;
	[SerializeField] PauseMenu pauseMenu;
    [SerializeField] GameMenu gameMenu;
	[SerializeField] PassMenu passMenu;
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
    private const float coef_3 = 0.9f;
	private const float coef_5 = 1.5f;


    private int[] scores = { 10, 10, 100, 10};
    public int curStage;
    private void Awake()
	{
		energy.Initialize();
	}

    // Use this for initialization
    void Start()
    {
        //curStage = PlayerPrefs.GetInt("stageNum");
		curStage = LoadSceneOnClick.stageNum;
        StartGame();
    }

	void Update()
	{
		if (isGamePaused == false && isCountdownFinished == true && isGameOver == false) {
			if(curStage==3)
				energy.CurrentVal -= coef_3 * Time.deltaTime;
			if(curStage==5)
				energy.CurrentVal -= coef_5 * Time.deltaTime;
            time += Time.deltaTime;
            gameMenu.SetScore(score);
            gameMenu.SetTime((int)time);
            scoreCheck(PlayerPrefs.GetInt("stageNum"), score);
        }
    }

    private void scoreCheck(int ind, int score) {			
        if(ind == 4 || ind > curStage-1) {
            return;
		} else
		{ 
			if (curStage == 3&&energy.CurrentVal>99) 
			{
				PlayerPrefs.SetInt ("stageNum", ind + 1);
				PassGame ();
			}
			if (score >= scores [ind]) {
				PlayerPrefs.SetInt ("stageNum", ind + 1);
				PassGame ();
			}

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
		if(curStage == 5)
		energy.CurrentVal = 0;
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
        deathMenu.ToggleEndMenu(score);  //show the death button when the player died
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
		passMenu.ClosePassMenu();
        gameMenu.Pause(false);
	}

	public void PassGame()
	{
		isGamePaused = true;
		passMenu.TogglePassMenu();
		gameMenu.Pause(true);
	}
}

