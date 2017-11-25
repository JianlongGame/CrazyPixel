using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

    [SerializeField] GameController controller;
    [SerializeField] Image[] m_LifeImages;
    [SerializeField] Text stageText;
    [SerializeField] Text timeText;
    [SerializeField] Text scoreText;
	[SerializeField] Button pauseButton;

    public bool CounterDownDone = false;
    public CountDown countDown;

    // Use this for initialization
    void Start()
    {
        SetStage(LoadSceneOnClick.stageNum);
        timeText.text = "Time: 0";
        scoreText.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isCountdownFinished == false && countDown.isFinished == true)
        {
            countDown.gameObject.SetActive(false);
            controller.isCountdownFinished = true;
        }
    }

    public void SetStage(int stageNum)
    {
        stageText.text = string.Format("Stage: {0}", stageNum);
        countDown.SetStage(stageNum);
    }

    public void SetTime(int secs)
    {
        timeText.text = string.Format("Time: {0}", secs);
    }

    public void SetScore(int score)
    {
        scoreText.text = string.Format("Score: {0}", score);
    }

    public void RemoveLife(int life)
    {
        m_LifeImages[life].gameObject.SetActive(false);
    }

    public void AddLife(int life)
    {
        m_LifeImages[life].gameObject.SetActive(true);
    }

    public void Pause(bool pause)
    {
        pauseButton.gameObject.SetActive(!pause);
    }
}
