using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavalampScript : ThrowableScript
{

    /// <summary>
    /// The explosion.
    /// </summary>
    public Explosion ExplosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.4f;
    }

    protected override void Awake()
    {
        base.Awake();
        Time.timeScale = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != Thrower)
        {
            RigidBody.velocity = Vector2.zero;
            Explode();
        }
    }

    public void Explode()
    {
        var explosion = Instantiate(ExplosionPrefab);
        explosion.transform.position = transform.position;
        SoundManager.instance.playLavaLamp();
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}
