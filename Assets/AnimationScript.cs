using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private Animator animator;
    Player player;
    GamepadInput gamepadInput;
    private Controller2D controller;
    private Boolean isShooting;

    private bool isfacingRight = false;

    private SpriteRenderer[] renderers;

   

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        gamepadInput = GetComponent<GamepadInput>();
        controller = GetComponent<Controller2D>();
        renderers = GetComponentsInChildren<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //renderers[2].sprite = Resources.Load("capHead",typeof(Sprite)) as Sprite;
        
        Vector2 directonalInput = new Vector2(gamepadInput.GetLeftHorizontalValue(), 0);

        if (directonalInput.x > 0 && !isfacingRight)
        {
            changeDirection();
            isfacingRight = true;
        }

        if (directonalInput.x < 0 && isfacingRight)
        {
            changeDirection();
            isfacingRight = false;
        }
       
        if (directonalInput.x != 0 && controller.collisions.below)
        {
            animator.SetInteger("State", 1);
        }

        if (directonalInput.x == 0 && controller.collisions.below)
        {
            animator.SetInteger("State", 0);
        }

        if (!controller.collisions.below)
        {
            animator.SetInteger("State", 3);
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("death")
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            foreach (var t in renderers)
            {
                t.enabled = false;
            }
            renderers[renderers.Length-2].enabled = true;
        }



    }

    private void changeDirection()
    {
        Vector3 localScale = player.transform.localScale;
        localScale.x = -localScale.x;
        player.transform.localScale = localScale;
    }
}
