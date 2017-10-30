﻿using UnityEngine;

public enum TouchType
{
    None,
    Tap,
    SwipeLeft,
    SwipeRight,
    SwipeUp,
    SwipeDown,
    Pinch,
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
            CheckKeyInput();
        }
        // USE TOUCH SCREEN CONTROLS
        else if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            CheckTouchInput();
        }
    }

    void CheckMouseInput()
    {
        // MOUSE CLICK START
        if (Input.GetMouseButtonDown(0))
        {
            t.startTime = Time.time;
            t.startLoc = Input.mousePosition;
            t.type = TouchType.None;
        }
        // MOUSE CLICK END
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

    void CheckKeyInput()
    {
        // Fuse|unfuse left and middle
        if (Input.GetKeyDown(KeyCode.A))
        {
            t.startTime = Time.time;
            t.startLoc = new Vector3(0, 0, 0);
            t.endLoc = new Vector3(Screen.width / 2f, 0, 0);
            t.type = TouchType.Pinch;
            m_OnTouch(t);
        }
        // Fuse|unfuse right and middle
        else if (Input.GetKeyDown(KeyCode.D))
        {
            t.startTime = Time.time;
            t.startLoc = new Vector3(Screen.width / 2f, 0, 0);
            t.endLoc = new Vector3(Screen.width - 1, 0, 0);
            t.type = TouchType.Pinch;
            m_OnTouch(t);
        }
    }

    void TouchOne(Touch touch)
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
    }

    void TouchTwo(Touch t1, Touch t2)
    {
        Vector2 t1PrevPos, t2PrevPos;
        t.startTime = Time.time;
        t.startLoc = t1.position;
        t.endLoc = t2.position;
        t.type = TouchType.None;

        // Find the position in the previous frame of each touch.
        t1PrevPos = t1.position - t1.deltaPosition;
        t2PrevPos = t2.position - t2.deltaPosition;

        // Find the magnitude of the vector (the distance) between the touches in each frame.
        float prevTouchDeltaMag = (t1PrevPos - t2PrevPos).magnitude;
        float touchDeltaMag = (t1.position - t2.position).magnitude;

        // Find the difference in the distances between each frame.
        float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
        if (deltaMagnitudeDiff > minSwipeDist)
        {
            t.type = TouchType.Pinch;
        }
    }

    void CheckTouchInput()
    {
        // SINGLE FINGER
        if (Input.touchCount == 1)
        {
            TouchOne(Input.GetTouch(0));
        }
        // TWO FINGERS
        else if (Input.touchCount == 2)
        {
            Touch t1 = Input.GetTouch(0).position.x < Input.GetTouch(1).position.x ? Input.GetTouch(0) : Input.GetTouch(1);
            Touch t2 = Input.GetTouch(0).position.x > Input.GetTouch(1).position.x ? Input.GetTouch(0) : Input.GetTouch(1);
            TouchTwo(t1, t2);
        }

        if (t.type != TouchType.None && m_OnTouch != null)
            m_OnTouch(t);
    }
}