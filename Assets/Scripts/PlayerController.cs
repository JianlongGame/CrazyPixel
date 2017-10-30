using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] TouchListener m_TouchListener;
    [SerializeField] Player[] m_PlayerObjects;
    float lastTouchTime;

    // Use this for initialization
    void Start()
    {
        lastTouchTime = Time.time;
        m_TouchListener.AddOnTouchCallback(OnTouch);
    }

    // Control player
    void OnTouch(MyTouch touch)
    {
        if (touch.startTime - lastTouchTime < 0.1)
            return;

        float laneWidth = Screen.width / m_PlayerObjects.Length;
        int player = (int)(touch.startLoc.x / laneWidth);
        lastTouchTime = touch.startTime;

        switch (touch.type)
        {
            // Move left
            case TouchType.SwipeLeft:
                SwapLeft(player);
                break;

            // Move right
            case TouchType.SwipeRight:
                SwapRight(player);
                break;

            // Fuse or unfuse colors
            case TouchType.Pinch:
                int player2 = (int)(touch.endLoc.x / laneWidth);
                if (player == 0 && player2 == 2 || player == 2 && player2 == 0)
                    return;

                Player p1 = m_PlayerObjects[player];
                Player p2 = m_PlayerObjects[player2];
                if (!p1.isFused && !p2.isFused)
                    FuseColors(p1, p2);
                else if (p1.isFused && p2.isFused)
                    UnFuseColors(p1, p2);
                break;
        }
    }

    // Fuse the two colors
    void FuseColors(Player pA, Player pB)
    {
        if (pA != pB)
        {
            string color = GetFusedColor(pA, pB);
            pA.MoveTo(.375f);
            pB.MoveTo(-.375f);
            pA.Fuse(true, color);
            pB.Fuse(true, color);
        }
    }

    // Unfuse the two colors
    void UnFuseColors(Player pA, Player pB)
    {
        if (pA != pB)
        {
            pA.MoveTo(-.375f);
            pB.MoveTo(.375f);
            pA.Fuse(false, pA.origColor);
            pB.Fuse(false, pB.origColor);
        }
    }

    string GetFusedColor(Player a, Player b)
    {
        if (a.GetColor() == "Red" && b.GetColor() == "Yellow" ||
            b.GetColor() == "Red" && a.GetColor() == "Yellow")
            return "Orange";

        if (a.GetColor() == "Red" && b.GetColor() == "Blue" ||
            b.GetColor() == "Red" && a.GetColor() == "Blue")
            return "Purple";

        if (a.GetColor() == "Blue" && b.GetColor() == "Yellow" ||
            b.GetColor() == "Blue" && a.GetColor() == "Yellow")
            return "Green";

        return "White";
    }

    // Swap the color with the one on the left
    void SwapLeft(int pos)
    {
        Player player = m_PlayerObjects[pos];
        if (pos == 0)
            return;
        if (pos == 1) {
            if (player.isFused) {
                if (m_PlayerObjects[pos - 1].isFused)       // |  o|x  | o |
                    return;
                m_PlayerObjects[pos].MoveTo(-1);            // | o |  x|o  |
                m_PlayerObjects[pos + 1].MoveTo(-1);
                m_PlayerObjects[pos - 1].MoveTo(2);
                SwapPlayers(pos, pos - 1);
                SwapPlayers(pos, pos + 1);
            }
            else {
                m_PlayerObjects[pos].MoveTo(-1);            // | o | x | o |
                m_PlayerObjects[pos - 1].MoveTo(1);
                SwapPlayers(pos, pos - 1);
            }
        }
        else if (pos == 2) {
            if (player.isFused) {                           // | o |  o|x  |
                m_PlayerObjects[pos].MoveTo(-1);
                m_PlayerObjects[pos - 1].MoveTo(-1);
                m_PlayerObjects[pos - 2].MoveTo(2);
                SwapPlayers(pos, pos - 1);
                SwapPlayers(pos, pos - 2);
            }
            else {
                if (m_PlayerObjects[pos - 1].isFused) {     // |  o|o  | x | 
                    m_PlayerObjects[pos].MoveTo(-2);
                    m_PlayerObjects[pos - 1].MoveTo(1);
                    m_PlayerObjects[pos - 2].MoveTo(1);
                    SwapPlayers(pos, pos - 2);
                    SwapPlayers(pos, pos - 1);
                }
                else {                                      // | o | o | x |
                    m_PlayerObjects[pos].MoveTo(-1);
                    m_PlayerObjects[pos - 1].MoveTo(1);
                    SwapPlayers(pos, pos - 1);
                }
            }
        }
    }

    // Swap the color with the one on the right
    void SwapRight(int pos)
    {
        Player player = m_PlayerObjects[pos];
        if (pos == 2)
            return;
        if (pos == 0) {
            if (player.isFused) {                           // |  x|o  | o |
                m_PlayerObjects[pos].MoveTo(1);
                m_PlayerObjects[pos + 1].MoveTo(1);
                m_PlayerObjects[pos + 2].MoveTo(-2);
                SwapPlayers(pos + 1, pos + 2);
                SwapPlayers(pos, pos + 1);
            }
            else {
                if (m_PlayerObjects[pos + 1].isFused) {     // | x |  o|o  |
                    m_PlayerObjects[pos].MoveTo(2);
                    m_PlayerObjects[pos + 1].MoveTo(-1);
                    m_PlayerObjects[pos + 2].MoveTo(-1);
                    SwapPlayers(pos, pos + 1);
                    SwapPlayers(pos + 1, pos + 2);
                }
                else {                                      // | x | o | o |
                    m_PlayerObjects[pos].MoveTo(1);
                    m_PlayerObjects[pos + 1].MoveTo(-1);
                    SwapPlayers(pos, pos + 1);
                }
            }
        }
        else if (pos == 1) {
            if (player.isFused) {
                if (m_PlayerObjects[pos + 1].isFused)       // | o |  x|o  |
                    return;
                m_PlayerObjects[pos].MoveTo(1);             // |  o|x  | o |
                m_PlayerObjects[pos - 1].MoveTo(1);
                m_PlayerObjects[pos + 1].MoveTo(-2);
                SwapPlayers(pos, pos + 1);
                SwapPlayers(pos - 1, pos);
            }
            else {                                          // | o | x | o |
                m_PlayerObjects[pos].MoveTo(1);
                m_PlayerObjects[pos + 1].MoveTo(-1);
                SwapPlayers(pos, pos + 1);
            }
        }
    }

    // Swap the two player objects
    void SwapPlayers(int a, int b)
    {
        Player temp = m_PlayerObjects[a];
        m_PlayerObjects[a] = m_PlayerObjects[b];
        m_PlayerObjects[b] = temp;
    }
}
