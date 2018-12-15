using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoJoScript : ThrowableScript
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // collided with wall
        RigidBody.velocity = Vector2.zero;

        Thrower.transform.position = transform.position;
    }
}
