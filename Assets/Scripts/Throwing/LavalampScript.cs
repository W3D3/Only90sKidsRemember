using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavalampScript : ThrowableScript
{
    /// <summary>
    /// The maximum speed to turn the lamp.
    /// </summary>
    public float MaxTurnSpeed;

    /// <summary>
    /// The deadzone around 0, which wont be used as turn factor.
    /// </summary>
    public float MaxTurnSpeedDeadzone;

    // Start is called before the first frame update
    void Start()
    {
        float turn = Random.Range(0, 2) == 0 ? Random.Range(-MaxTurnSpeed, -MaxTurnSpeedDeadzone) : Random.Range(MaxTurnSpeedDeadzone, MaxTurnSpeed);
        RigidBody.AddTorque(turn);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // todo explode
}
