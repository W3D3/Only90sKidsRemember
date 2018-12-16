using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoJoScript : ThrowableScript
{
    public Color LineColor;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer.startWidth = 0.1f;
        LineRenderer.endWidth = 0.1f;
        LineRenderer.SetPositions(new Vector3[] { Thrower.transform.position, this.transform.position });
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall"
            || (collision.gameObject.tag == "Player" 
                && collision.gameObject.GetComponent<Player>() != Thrower))
        {
            RigidBody.velocity = Vector2.zero;
            Thrower.StartSpidermanMove(gameObject);
            
            Invoke("DestroyJojo", 0.4f);
        }

    }

    public void DestroyJojo()
    {
        Destroy(this.gameObject);
    }

    private LineRenderer LineRenderer
    {
        get { return GetComponent<LineRenderer>(); }
    }
}
