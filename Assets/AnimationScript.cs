﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private Animator animator;
    Player player;
    GamepadInput gamepadInput;
    private Controller2D controller;
    private Boolean isShooting;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        gamepadInput = GetComponent<GamepadInput>();
        controller = GetComponent<Controller2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 directonalInput = new Vector2(gamepadInput.GetLeftHorizontalValue(),0);
        
        if(directonalInput.x != 0  && controller.collisions.below)
        {
            animator.SetInteger("State",1);
        }

        if (directonalInput.x == 0 && controller.collisions.below)
        {
            animator.SetInteger("State", 0);
        }
        
        if (!controller.collisions.below)
        {
            animator.SetInteger("State",3);
        }
       
        if (gamepadInput.IsRegularFirePressed() )
        {
            animator.SetInteger("State",2);
        }
    }
}