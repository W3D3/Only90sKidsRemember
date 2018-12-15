using System;
using UnityEngine;
using Randomamama = UnityEngine.Random;

public class ThrowableScript : MonoBehaviour
{
    public Rigidbody2D RigidBody;

    public SpriteRenderer SpriteRenderer;

    public Player Thrower;

    /// <summary>
    /// The maximum speed to turn the throwable.
    /// </summary>
    public float MaxTurnSpeed;

    /// <summary>
    /// The minium speed to turn the throwable.
    /// </summary>
    public float MinTurnSpeed;

    /// <summary>
    /// The stepsize to raise the speed if charged.
    /// </summary>
    public float SpeedStep;

    /// <summary>
    /// The maximum throwing speed.
    /// </summary>
    public float MaxSpeed;

    public Sprite Thumbnail;

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
        
        float turn = Randomamama.Range(0, 2) == 0 ? Randomamama.Range(-MaxTurnSpeed, -MinTurnSpeed) : Randomamama.Range(MinTurnSpeed, MaxTurnSpeed);
        RigidBody.AddTorque(turn);
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
