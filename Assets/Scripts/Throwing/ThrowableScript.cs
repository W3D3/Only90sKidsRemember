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

    public void InstantiateSpeed(Vector2 direction, float speed)
    {
        var rBody = GetComponent<Rigidbody2D>();
        rBody.velocity = direction * speed;
    }
}
