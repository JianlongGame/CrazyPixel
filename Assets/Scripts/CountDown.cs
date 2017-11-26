using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] Text countdownText;
    [SerializeField] Text stageText;
	[SerializeField] Image mergeHintImage;
    public bool isFinished = false;
    float timer = 4;

    // Use this for initialization
    void Start()
    {
        stageText.text = "";
        countdownText.text = "3";
		mergeHintImage.gameObject.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFinished)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isFinished = true;
            }
            else if (timer <= 1)
            {
                countdownText.text = "GO";
            }
            else
            {
                countdownText.text = string.Format("{0}", (int)timer);
            }
        }
    }

    public void SetStage(int stage)
    {
		if (stage == 1)
			stageText.text = string.Format ("Stage 1:\nMatch Colors");
		else if (stage == 2) 
		{
			stageText.text = string.Format ("Stage 2:\nPinch to Merge Colors");
			mergeHintImage.gameObject.SetActive (true);
		}
		else if (stage == 3) 
		{
			stageText.text = string.Format ("Stage 3:\nTap to Match Shape");


		}
        else if (stage == 4)
            stageText.text = string.Format("Stage 4:\nMoving Arrows");
        else if (stage == 5)
            stageText.text = string.Format("Stage 5:\nInfinite Mode");
    }
}
