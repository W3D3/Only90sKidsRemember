using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// Gamepad Input for Keyboard and Xbox Controller.
/// </summary>
public class GamepadInput : MonoBehaviour
{
    public bool EnablePlayerControls;

    /// <summary>
    /// Mapped via <see cref="ControllerType"/>.
    /// </summary>
    public int ControllerNumber;

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
    public bool JumpPressed()
    {
        return Input.GetKeyDown(GetJumpKeyCodeForController());
    }

    /// <summary>
    /// Returns true if the jump button was released.
    /// </summary>
    /// <returns></returns>
    public bool JumpReleased()
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
}
