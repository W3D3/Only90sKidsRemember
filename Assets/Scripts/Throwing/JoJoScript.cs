using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoJoScript : ThrowableScript
{
    public Rigidbody2D RigidBody;

    // Start is called before the first frame update
    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // activate movement
        RigidBody.velocity = Vector2.zero;
    }
}
