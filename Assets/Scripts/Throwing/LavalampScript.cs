using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavalampScript : ThrowableScript
{
    public Rigidbody2D Rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Rigidbody2D.AddTorque(-20f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
