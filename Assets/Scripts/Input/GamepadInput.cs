using System;
using System.Linq;
using UnityEngine;

public class GamepadInput : MonoBehaviour
{
    private static GamepadInput _instance;

    private static bool EnablePlayerControls = true;
    private static bool EnableColorControls = true;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    // Use this for initialization
    // ReSharper disable once Unity.RedundantEventFunction
    void Start()
    {
        EnableColorControls = true;
        EnablePlayerControls = true;
    }

    // Update is called once per frame
    // ReSharper disable once Unity.RedundantEventFunction
    void Update()
    {
        //foreach (string s in Input.GetJoystickNames())
        //    Debug.Log(s);

        //int i = 0;
        //while (i < 4)
        //{
        //    if (Mathf.Abs(Input.GetAxis("Joy" + (i+1) + "X")) > 0.2F || Mathf.Abs(Input.GetAxis("Joy" + (i + 1) + "Y")) > 0.2F)
        //        Debug.Log(Input.GetJoystickNames()[i] + " is moved");

        //    i++;
        //}
        
    /*
        if (Jump())
            Debug.Log("jump pressed");

        if (Dash())
            Debug.Log("dash pressed");

        if (Color1())
            Debug.Log("left trigger pressed");

        if (Color2())
            Debug.Log("right trigger pressed");

        if (ColorMixed())
            Debug.Log("both trigger pressed");
        */

    }

    private const string Color1Button = "Color1";
    private const string Color2Button = "Color2";
    private const string Horizontal = "Horizontal";
    private const string HorizontalSwitch = "HorizontalSwitch";
    private const string PauseAxis = "Pause";
    private const double Tolerance = .1;

    private static readonly string[] SwitchGamepadNames = { "Wireless Gamepad", "Unknown Pro Controller" };

    private static bool IsNintendoSwitchProController(string controllerName)
    {
        return SwitchGamepadNames.Contains(controllerName);
    }
    
    public static float HorizontalVal()
    {
        var horVal = Input.GetAxis(Horizontal);

        foreach (var controllerName in Input.GetJoystickNames())
        {
            var horValSwitch = Input.GetAxis(HorizontalSwitch);

            if (!IsNintendoSwitchProController(controllerName) || Math.Abs(horValSwitch) < Tolerance) continue;
            
            horVal = horValSwitch;
            break;
        }

        return EnablePlayerControls ? horVal : 0;
    }


    public static bool Jump()
    {
        return EnableColorControls && (Input.GetKeyDown(KeyCode.Space) || // keyboard
                                       Input.GetKeyDown(KeyCode.JoystickButton0) || // switch and xbox controller
                                       Input.GetKeyDown(KeyCode.JoystickButton16)); // macOS binding
    }

    public static bool Dash()
    {
        return EnableColorControls && (Input.GetKeyDown(KeyCode.LeftShift) || // keyboard
                                       Input.GetKeyDown(KeyCode.JoystickButton2) || // switch and xbox controller
                                       Input.GetKeyDown(KeyCode.JoystickButton18)); // macOS binding
    }

    public static bool Color1()
    {
        return EnableColorControls &&
               Input.GetAxis(Color1Button) > Tolerance &&
               Input.GetAxis(Color2Button) < Tolerance;
    }

    public static bool Color2()
    {
        return EnableColorControls &&
               Input.GetAxis(Color1Button) < Tolerance &&
               Input.GetAxis(Color2Button) > Tolerance;
    }

    public static bool ColorMixed()
    {
        return EnableColorControls &&
               Input.GetAxis(Color1Button) > Tolerance &&
               Input.GetAxis(Color2Button) > Tolerance;
    }

    public static bool Pause()
    {
        return Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7);
    }
}
