using System;
using UnityEngine;

public class ThrowableScript : MonoBehaviour
{
    public Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InstantiateSpeed(float speed)
    {
        var gravity = Math.Abs(Physics2D.gravity.y);
        var rBody = GetComponent<Rigidbody2D>();
        rBody.velocity = new Vector2(speed, speed);
    }
}
