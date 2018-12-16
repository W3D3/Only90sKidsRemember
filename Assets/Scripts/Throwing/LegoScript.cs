using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LegoScript : ThrowableScript
{
    // Start is called before the first frame update
    public List<Color32> BrickColors;

    private bool isActive = true;

    void Start()
    {
        var sprite = GetComponent<SpriteRenderer>();
        sprite.color = BrickColors[Random.Range(0, BrickColors.Count)];
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isActive = RigidBody.velocity.magnitude > 0.08;
        if (isActive)
        {
            if (collision.gameObject.tag == "Player")
            {
                var hitPlayer = collision.gameObject.GetComponent<Player>();
                if (hitPlayer != Thrower)
                {
                    RigidBody.velocity = Vector2.zero;
                    hitPlayer.Damage(DamageType.Generic);
                }
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            var hitPlayer = collision.gameObject.GetComponent<Player>();
            hitPlayer.AddAmmo(1);
            Destroy(gameObject);
        }
    }
}