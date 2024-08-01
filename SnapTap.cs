using System;
using UnityEngine;

/// <summary>
/// Simulate Razer Snap Tap feature using Unity Input handling.
/// A proof of concept and a slap in the face for lazy developer out there that would rather slap a ban hammer for people that uses null-bind script to achieve a perfect strafe.
/// </summary>
public class SnapTap : MonoBehaviour
{
    private int _horizontalRawAxisValue;
    private int _verticalRawAxisValue;
    private KeyCode _lastHorizontalKey;
    private KeyCode _lastVerticalKey;


    private void Update()
    {
        RazerSnapTapAxis();
    }
    
    private void RazerSnapTapAxis()
    {
        // Phase 1 of Input, checks for KeyDown and caches it
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            if(Input.GetKeyDown(KeyCode.A))
                _lastHorizontalKey = KeyCode.A;
            else if(Input.GetKeyDown(KeyCode.D))
                _lastHorizontalKey = KeyCode.D;
        }

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            if(Input.GetKeyDown(KeyCode.W))
                _lastVerticalKey = KeyCode.W;
            else if(Input.GetKeyDown(KeyCode.S))
                _lastVerticalKey = KeyCode.S;
        }

        // Phase 2 of Input, checks for KeyUp of the cached keydown from the last frame.
        // This phase will be naturally skipped if a new key is pressed on each axis during Phase 1.
        if(Input.GetKeyUp(_lastHorizontalKey))
        {
            if(_lastHorizontalKey == KeyCode.A && Input.GetKey(KeyCode.D))
                _lastHorizontalKey = KeyCode.D;
            else if(_lastHorizontalKey == KeyCode.D && Input.GetKey(KeyCode.A))
                _lastHorizontalKey = KeyCode.A;
            else
                _lastHorizontalKey = KeyCode.None;
        }

        if(Input.GetKeyUp(_lastVerticalKey))
        {
            if(_lastVerticalKey == KeyCode.W && Input.GetKey(KeyCode.S))
                _lastVerticalKey = KeyCode.S;
            else if(_lastVerticalKey == KeyCode.S && Input.GetKey(KeyCode.W))
                _lastVerticalKey = KeyCode.W;
            else
                _lastVerticalKey = KeyCode.None;
        }

        // Assign a rudimentary raw value to horizontal and vertical axis according to the cached value.
        if(_lastVerticalKey == KeyCode.W)
            _verticalRawAxisValue = 1;
        else if(_lastVerticalKey == KeyCode.S)
            _verticalRawAxisValue = -1;
        else if(_lastVerticalKey == KeyCode.None)
        {
            _verticalRawAxisValue = 0;
        }

        if(_lastHorizontalKey == KeyCode.A)
            _horizontalRawAxisValue = -1;
        else if(_lastHorizontalKey == KeyCode.D)
            _horizontalRawAxisValue = 1;
        else if(_lastHorizontalKey == KeyCode.None)
        {
            _horizontalRawAxisValue = 0;
        }
    }
}