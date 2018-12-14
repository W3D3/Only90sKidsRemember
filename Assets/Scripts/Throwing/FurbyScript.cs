using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FurbyScript : MonoBehaviour
{
    public bool MovementActivated = false;
    // set to 1 to face right
    public int FaceDirection = 1;
    public float Speed = 1.5f;

    public Rigidbody2D rigidBody;
    public SpriteRenderer renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var raycastDown = Physics2D.RaycastAll(transform.position, Vector2.down, renderer.bounds.size.y);
        
        if (raycastDown.Length > 1 && MovementActivated)
        {
            // colliding with object bottom
            // move left or right
            rigidBody.velocity = new Vector2(FaceDirection == 1 ? Speed : -Speed, rigidBody.velocity.y);
        }


        var raycastRight = Physics2D.RaycastAll(transform.position, Vector2.right, renderer.bounds.size.x);
        if (raycastRight.Length > 1)
        {
            FaceDirection = 0;
        }

        var raycastLeft = Physics2D.RaycastAll(transform.position, Vector2.left, renderer.bounds.size.y);
        if (raycastLeft.Length > 1)
        {
            FaceDirection = 1;
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, new Vector3(0, -renderer.bounds.size.y, 0));
        Gizmos.DrawRay(transform.position, new Vector3(-renderer.bounds.size.x, 0, 0));
        Gizmos.DrawRay(transform.position, new Vector3(renderer.bounds.size.x, 0, 0));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // activate movement
        MovementActivated = true;
    }
}
