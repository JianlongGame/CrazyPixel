using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TouchType
{
    None,
    Tap,
    SwipeLeft,
    SwipeRight,
    SwipeUp,
    SwipeDown,
}

public class MyTouch
{
    public TouchType type = TouchType.None;
    public float startTime = 0.0f;             // Time touch began
    public float endTime = 0.0f;               // Time touch ended
    public Vector2 startLoc = Vector2.zero;    // Place touch started
    public Vector2 endLoc = Vector2.zero;      // Place touch ended

    public float time { get { return endTime - startTime; } }
    public float distance { get { return (endLoc - startLoc).magnitude; } }
    public Vector2 direction { get { return endLoc - startLoc; } }

    public MyTouch()
    {
        type = TouchType.None;
        startTime = endTime = 0.0f;
        startLoc = endLoc = Vector2.zero;
    }
}

public class TouchListener : MonoBehaviour
{
    public delegate void OnTouch(MyTouch touch);

    private float minSwipeDist = 100.0f;
    private float maxSwipeTime = 0.5f;
    private MyTouch t = new MyTouch();
    OnTouch m_OnTouch;

    public void AddOnTouchCallback(OnTouch cb)
    {
        m_OnTouch += cb;
    }

    void Update()
    {
        // USE MOUSE CONTROLS
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            CheckMouseInput();
        }
        // USE TOUCH SCREEN CONTROLS
        else if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            CheckTouchInput();
        }
    }

    void CheckMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            t.startTime = Time.time;
            t.startLoc = Input.mousePosition;
            t.type = TouchType.None;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            t.endTime = Time.time;
            t.endLoc = Input.mousePosition;

            // User swiped the screen
            if (t.time < maxSwipeTime && t.distance > minSwipeDist)
            {
                // Horizontal swipe
                if (Mathf.Abs(t.direction.x) > Mathf.Abs(t.direction.y))
                {
                    Vector2 swipeType = Vector2.right * Mathf.Sign(t.direction.x);
                    t.type = swipeType.x > 0.0f ? TouchType.SwipeRight : TouchType.SwipeLeft;
                }
                // Vertical swipe
                else
                {
                    Vector2 swipeType = Vector2.up * Mathf.Sign(t.direction.y);
                    t.type = swipeType.y > 0.0f ? TouchType.SwipeUp : TouchType.SwipeDown;
                }
            }
            // User tapped the screen
            else
            {
                t.type = TouchType.Tap;
            }

            if (t.type != TouchType.None && m_OnTouch != null)
                m_OnTouch(t);
        }
    }

    void CheckTouchInput()
    {
        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                // User touched the screen
                case TouchPhase.Began:
                    t.startTime = Time.time;
                    t.startLoc = touch.position;
                    t.type = TouchType.None;
                    break;

                // User lifted their finger off screen
                case TouchPhase.Ended:
                    t.endTime = Time.time;
                    t.endLoc = touch.position;

                    // User swiped the screen
                    if (t.time < maxSwipeTime && t.distance > minSwipeDist)
                    {
                        // Horizontal swipe
                        if (Mathf.Abs(t.direction.x) > Mathf.Abs(t.direction.y))
                        {
                            Vector2 swipeType = Vector2.right * Mathf.Sign(t.direction.x);
                            t.type = swipeType.x > 0.0f ? TouchType.SwipeRight : TouchType.SwipeLeft;
                        }
                        // Vertical swipe
                        else
                        {
                            Vector2 swipeType = Vector2.up * Mathf.Sign(t.direction.y);
                            t.type = swipeType.y > 0.0f ? TouchType.SwipeUp : TouchType.SwipeDown;
                        }
                    }
                    // User tapped the screen
                    else
                    {
                        t.type = TouchType.Tap;
                    }
                    break;
            }

            if (t.type != TouchType.None && m_OnTouch != null)
                m_OnTouch(t);
        }
    }
}
