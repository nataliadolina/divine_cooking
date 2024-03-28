using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveTouchArgs
{
    public int TouchIndex;
    public Vector2 StartPosition;
    public Vector2 Offset;

    public MoveTouchArgs(int touchIndex, Vector2 startPosition, Vector2 offset)
    {
        TouchIndex = touchIndex;
        StartPosition = startPosition;
        Offset = offset;
    }

    public void ChangeOffsetAndStartPosition(Vector2 newOffset, Vector2 newStartPosition)
    {
        Offset = newOffset;
        StartPosition = newStartPosition;
    }
}


public class MobileTouchManager : MonoBehaviour
{
    public event Action<Dictionary<int, MoveTouchArgs>> onMovedFinger;
    public event Action<int> oneEndedFingerTouch;

    private Dictionary<int, MoveTouchArgs> _moveTouchArgsMap = new Dictionary<int, MoveTouchArgs>();
    private bool _movedFingerFlag = false;

    private void Update()
    {
        _movedFingerFlag = false;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            int fingerId = touch.fingerId;
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            if (touch.phase == TouchPhase.Began)
            {
                _moveTouchArgsMap.Add(fingerId, new MoveTouchArgs(fingerId, touchPosition, Vector2.zero));
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (_moveTouchArgsMap.ContainsKey(fingerId))
                {
                    MoveTouchArgs args = _moveTouchArgsMap[fingerId];
                    Vector2 newStartPosition = args.StartPosition + args.Offset;
                    _movedFingerFlag = true;
                    _moveTouchArgsMap[fingerId].ChangeOffsetAndStartPosition(touchPosition - newStartPosition, newStartPosition);
                }
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (_moveTouchArgsMap.ContainsKey(fingerId))
                {
                    oneEndedFingerTouch?.Invoke(fingerId);
                    _moveTouchArgsMap.Remove(fingerId);
                }
            }
        }

        if (_movedFingerFlag)
        {
            onMovedFinger?.Invoke(_moveTouchArgsMap);
        }
    }
}
