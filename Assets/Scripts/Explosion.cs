using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Explosion : MonoBehaviour
{
    public float radius = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        var animation = this.GetComponent<Animator>();
        animation.Play("Explosion_Anim");
        foreach (var affected in Physics2D.OverlapCircleAll(transform.position, radius))
        {
            var playerComponent = affected.GetComponent<Player>();
            if (playerComponent != null)
            {
                playerComponent.Damage(DamageType.Explosion);
            }
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        //Start();
    }
}
