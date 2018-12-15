using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FurbyScript : ThrowableScript
{
    public bool MovementActivated = false;
    // set to 1 to face right
    public int FaceDirection = 1;
    public float Speed = 1.5f;

    public int MinTime = 3;
    public int MaxTime = 7;
            
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", Random.Range(MinTime, MaxTime));
    }

    // Update is called once per frame
    void Update()
    {
        var raycastDown = Physics2D.RaycastAll(transform.position, transform.TransformDirection(Vector3.down), SpriteBounds.size.y / 2f + 0.1f);
        
        if (raycastDown.Length > 1 && MovementActivated)
        {
            // colliding with object bottom
            // move left or right
            RigidBody.velocity = new Vector2(FaceDirection == 1 ? Speed : -Speed, RigidBody.velocity.y);
        }
        
        var raycastRight = Physics2D.RaycastAll(transform.position, transform.TransformDirection(Vector3.right), SpriteBounds.size.x / 2f + 0.1f);
        if (raycastRight.Length > 1)
        {
            FaceDirection = 0;
        }

        var raycastLeft = Physics2D.RaycastAll(transform.position, transform.TransformDirection(Vector3.left), SpriteBounds.size.y / 2f + 0.1f);
        if (raycastLeft.Length > 1)
        {
            FaceDirection = 1;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * (SpriteBounds.size.y / 2f + 0.1f));
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * (SpriteBounds.size.y / 2f + 0.1f));
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * (SpriteBounds.size.y / 2f + 0.1f));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // activate movement
        MovementActivated = true;
    }

    public void Explode()
    {
        Debug.Log("Explode");
        // todo instantiate explosion
        Destroy(gameObject);
    }
}
