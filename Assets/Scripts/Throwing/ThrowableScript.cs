using System;
using UnityEngine;

public class ThrowableScript : MonoBehaviour
{
    public Rigidbody2D RigidBody;
    public SpriteRenderer SpriteRenderer;

    // Awake is called after instantiation.
    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetSpeed(Vector2 direction, float speed)
    {
        RigidBody.velocity = direction * speed;
    }

    protected Bounds SpriteBounds { get { return SpriteRenderer.bounds; } }
}
