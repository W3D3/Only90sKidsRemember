using UnityEngine;

public class GettoblasterScript : ThrowableScript
{
    public Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter(Collision other)
    {


        int x = 5;
        x++;

    }
}