using System;
using System.Linq;
using UnityEngine;

public class GamepadInput : MonoBehaviour
{
    public bool EnablePlayerControls;

    /// <summary>
    /// Mapped via <see cref="ControllerType"/>
    /// </summary>
    public int ControllerNumber;

    public enum ControllerType
    {
        //Keyboard = 0,
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
    
    private const double Tolerance = .1;

    private static readonly string[] SwitchGamepadNames = { "Wireless Gamepad", "Unknown Pro Controller" };
    
    /// <summary>
    /// Returns the value from the left stick X axis.
    /// </summary>
    /// <returns></returns>
    public float LeftHorizontalValue()
    {
        ControllerType t = (ControllerType)ControllerNumber;
        var horVal = Input.GetAxis(LeftHorizontal + t.ToString());

        return EnablePlayerControls ? horVal : 0;
    }

    /// <summary>
    /// Returns the value from the right stick X axis.
    /// </summary>
    /// <returns></returns>
    public float RightHorizontalValue()
    {
        ControllerType t = (ControllerType)ControllerNumber;
        var horVal = Input.GetAxis(RightHorizontal + t.ToString());

        return EnablePlayerControls ? horVal : 0;
    }

    /// <summary>
    /// Returns the value from the right stick Y axis.
    /// </summary>
    /// <returns></returns>
    public float RightVerticalValue()
    {
        ControllerType t = (ControllerType)ControllerNumber;
        var horVal = Input.GetAxis(RightVertical + t.ToString());

        return EnablePlayerControls ? horVal : 0;
    }

    public bool Jump()
    {
        return Input.GetKeyDown(KeyCode.JoystickButton0);

        return  (Input.GetKeyDown(KeyCode.Space) || // keyboard
                                       Input.GetKeyDown(KeyCode.JoystickButton0) || // switch and xbox controller
                                       Input.GetKeyDown(KeyCode.JoystickButton16)); // macOS binding
    }

    public bool Dash()
    {
        return  (Input.GetKeyDown(KeyCode.LeftShift) || // keyboard
                                       Input.GetKeyDown(KeyCode.JoystickButton2) || // switch and xbox controller
                                       Input.GetKeyDown(KeyCode.JoystickButton18)); // macOS binding
    }

    //public bool Color1()
    //{
    //    return EnableColorControls &&
    //           Input.GetAxis(Color1Button) > Tolerance &&
    //           Input.GetAxis(Color2Button) < Tolerance;
    //}

    //public bool Color2()
    //{
    //    return EnableColorControls &&
    //           Input.GetAxis(Color1Button) < Tolerance &&
    //           Input.GetAxis(Color2Button) > Tolerance;
    //}

    //public bool ColorMixed()
    //{
    //    return EnableColorControls &&
    //           Input.GetAxis(Color1Button) > Tolerance &&
    //           Input.GetAxis(Color2Button) > Tolerance;
    //}

    public bool Pause()
    {
        return Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7);
    }
}
