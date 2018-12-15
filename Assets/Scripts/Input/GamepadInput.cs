﻿using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// Gamepad Input for Keyboard and Xbox Controller for Windows Platform.
/// </summary>
public class GamepadInput : MonoBehaviour
{
    public bool EnablePlayerControls;

    /// <summary>
    /// The controller number from 0-4. Mapped via <see cref="ControllerType"/>.
    /// </summary>
    public int ControllerNumber;

    /// <summary>
    /// The threshold for shooting buttons.
    /// </summary>
    public float ShootingButtonThreshold;

    public enum ControllerType
    {
        Keyboard = 0,
        Joystick1 = 1,
        Joystick2 = 2,
        Joystick3 = 3,
        Joystick4 = 4
    }

    private const string LeftHorizontal = "LeftHorizontal";
    private const string RightHorizontal = "RightHorizontal";
    private const string RightVertical = "RightVertical";
    private const string Shoot = "Shoot";

    // Use this for initialization
    // ReSharper disable once Unity.RedundantEventFunction
    void Start()
    {
    }

    // Update is called once per frame
    // ReSharper disable once Unity.RedundantEventFunction
    void Update()
    {
    }

    /// <summary>
    /// Returns the value from the left stick X axis.
    /// </summary>
    /// <returns></returns>
    public float GetLeftHorizontalValue()
    {
        ControllerType t = (ControllerType)ControllerNumber;
        var horVal = Input.GetAxis(LeftHorizontal + t.ToString());

        return EnablePlayerControls ? horVal : 0;
    }

    /// <summary>
    /// Returns the value from the right stick X axis.
    /// </summary>
    /// <returns></returns>
    public float GetRightHorizontalValue()
    {
        ControllerType t = (ControllerType)ControllerNumber;
        var horVal = Input.GetAxis(RightHorizontal + t.ToString());

        return EnablePlayerControls ? horVal : 0;
    }

    /// <summary>
    /// Returns the value from the right stick Y axis.
    /// </summary>
    /// <returns></returns>
    public float GetRightVerticalValue()
    {
        ControllerType t = (ControllerType)ControllerNumber;
        var horVal = Input.GetAxis(RightVertical + t.ToString());

        return EnablePlayerControls ? horVal : 0;
    }

    private KeyCode GetJumpKeyCodeForController()
    {
        KeyCode code = KeyCode.Space;
        switch ((ControllerType)ControllerNumber)
        {
            case ControllerType.Keyboard:
                code = KeyCode.Space;
                break;
            case ControllerType.Joystick1:
                code = KeyCode.Joystick1Button0;
                break;
            case ControllerType.Joystick2:
                code = KeyCode.Joystick2Button0;
                break;
            case ControllerType.Joystick3:
                code = KeyCode.Joystick3Button0;
                break;
            case ControllerType.Joystick4:
                code = KeyCode.Joystick4Button0;
                break;
            default:
                break;
        }

        return code;
    }

    /// <summary>
    /// Returns true if the jump button was pressed.
    /// </summary>
    /// <returns></returns>
    public bool IsJumpPressed()
    {
        return Input.GetKeyDown(GetJumpKeyCodeForController());
    }

    /// <summary>
    /// Returns true if the jump button was released.
    /// </summary>
    /// <returns></returns>
    public bool IsJumpReleased()
    {
        return Input.GetKeyUp(GetJumpKeyCodeForController());
    }

    /// <summary>
    /// Pauses the game for all controllers.
    /// </summary>
    /// <returns></returns>
    public bool Pause()
    {
        return Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7);
    }

    private bool regularFirePressed = false;
    /// <summary>
    /// Returns true if the fire button was pressed.
    /// </summary>
    /// <returns></returns>
    public bool IsRegularFirePressed()
    {
        ControllerType t = (ControllerType)ControllerNumber;
        float pressedDepth = Input.GetAxis(Shoot + t.ToString());

        if (pressedDepth > ShootingButtonThreshold)
        {
            regularFirePressed = true;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Returns true if the fire button was released after it was pressed.
    /// </summary>
    /// <returns></returns>
    public bool IsRegularFireReleased()
    {
        ControllerType t = (ControllerType)ControllerNumber;
        float pressedDepth = Input.GetAxis(Shoot + t.ToString());

        if (regularFirePressed && pressedDepth < ShootingButtonThreshold)
        {
            regularFirePressed = false;
            return true;
        }
        return false;
    }

    private bool specialFirePressed = false;
    /// <summary>
    /// Returns true if the special fire button was pressed.
    /// </summary>
    /// <returns></returns>
    public bool IsSpecialFirePressed()
    {
        ControllerType t = (ControllerType)ControllerNumber;
        float pressedDepth = Input.GetAxis(Shoot + t.ToString());

        if (pressedDepth < -ShootingButtonThreshold)
        {
            specialFirePressed = true;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Returns true if the special fire button was released after it was pressed.
    /// </summary>
    /// <returns></returns>
    public bool IsSpecialFireReleased()
    {
        ControllerType t = (ControllerType)ControllerNumber;
        float pressedDepth = Input.GetAxis(Shoot + t.ToString());

        if (specialFirePressed && pressedDepth > -ShootingButtonThreshold)
        {
            specialFirePressed = false;
            return true;
        }
        return false;
    }
}
