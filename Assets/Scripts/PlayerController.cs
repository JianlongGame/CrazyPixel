using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] TouchListener m_TouchListener;
    [SerializeField] Player[] m_PlayerObjects;
    [SerializeField] GameObject[] m_Lanes;

    // Use this for initialization
    void Start()
    {
        m_TouchListener.AddOnTouchCallback(OnTouch);
    }

    // Control player swap
    void OnTouch(MyTouch touch)
    {
        float laneWidth = Screen.width / 3f;
        int selectedLane = (int)(touch.startLoc.x / laneWidth);

        switch (touch.type)
        {
            // Move left
            case TouchType.SwipeLeft:
                if (selectedLane > 0)
                {
                    m_PlayerObjects[selectedLane].MoveTo(-1);
                    m_PlayerObjects[selectedLane - 1].MoveTo(1);
                    SwapPlayers(selectedLane, selectedLane - 1);
                }
                break;
            // Move right
            case TouchType.SwipeRight:
                if (selectedLane < m_Lanes.Length - 1)
                {
                    m_PlayerObjects[selectedLane].MoveTo(1);
                    m_PlayerObjects[selectedLane + 1].MoveTo(-1);
                    SwapPlayers(selectedLane, selectedLane + 1);
                }
                break;
        }
    }

    // Swap the two players
    void SwapPlayers(int a, int b)
    {
        Player temp = m_PlayerObjects[a];
        m_PlayerObjects[a] = m_PlayerObjects[b];
        m_PlayerObjects[b] = temp;
    }
}
