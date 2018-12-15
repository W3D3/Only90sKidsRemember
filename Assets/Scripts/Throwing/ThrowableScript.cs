using System;
using UnityEngine;

public class ThrowableScript : MonoBehaviour
{
    public Rigidbody2D RigidBody;

    public SpriteRenderer SpriteRenderer;

    public Player Thrower;

    /// <summary>
    /// The stepsize to raise the speed if charged.
    /// </summary>
    public float SpeedStep;

    /// <summary>
    /// The maximum throwing speed.
    /// </summary>
    public float MaxSpeed;

    // Awake is called after instantiation.
    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetSpeed(Vector2 direction, float speed)
    {
        RigidBody.velocity = direction * speed;
    }

    protected Bounds SpriteBounds
    {
        get { return SpriteRenderer.bounds; }
    }
}
