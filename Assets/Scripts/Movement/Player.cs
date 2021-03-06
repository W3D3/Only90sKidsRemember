﻿using System;
using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	public Animator animator;

    float timeToWallUnstick;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller2D controller;

	Vector2 directionalInput;
	bool wallSliding;
	int wallDirX;

	private bool death = false;

    /// <summary>
    /// True if the looking direction is right.
    /// </summary>
    public bool LookingRight;

    /// <summary>
    /// The speed to move towards the jojo.
    /// </summary>
    public int JojoDragSpeed;

    /// <summary>
    /// The player name.
    /// </summary>
    public string Name;
    	
	//health and stuff
	public int health = 1;

    void Start() {
		controller = GetComponent<Controller2D> ();
		animator = GetComponent<Animator>();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
	}

	void Update() {
        
        CalculateVelocity();
		HandleWallSliding ();

		controller.Move (velocity * Time.deltaTime, directionalInput);

        if (velocity.x != 0)
            LookingRight = velocity.x > 0 ? true : false;

		if (controller.collisions.above || controller.collisions.below) {
			if (controller.collisions.slidingDownMaxSlope) {
				velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
			} else {
				velocity.y = 0;
			}
		}
	}

	public void Damage(DamageType type)
	{
		switch (type)
		{
			case DamageType.Generic:
			case DamageType.Fire:
                SoundManager.instance.playGeneralQuote();
				health--;
				break;
			case DamageType.Explosion:
				SoundManager.instance.playExpQuote();
				health--;
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(type), type, null);
		}

	    var components = GetComponentsInChildren<SpriteRenderer>();
        if (health <= 1)
        {
            components[2].enabled = true;
            components[3].enabled = false;
        }
		if(health <= 0 && !death)
		{
			animator.Play("death");
			GetComponent<GamepadInput>().EnablePlayerControls = false;
            death = true;

            // remove player indicator
            GetComponentsInChildren<SpriteRenderer>().Last().sprite = null;
		    
		    GetComponent<BoxCollider2D>().enabled = false;
		    GetComponent<Player>().enabled = false;
		    
		} 
			
	}

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public void StartSpidermanMove(GameObject targetObject)
    {
        Vector3 vector = (targetObject.transform.position - transform.position).normalized;
        var v = new Vector2(vector.x, vector.y) * JojoDragSpeed;
        velocity = v;
    }

    public void OnJumpInputDown() {
		if (wallSliding) {
			if (wallDirX == directionalInput.x) {
				velocity.x = -wallDirX * wallJumpClimb.x;
				velocity.y = wallJumpClimb.y;
			}
			else if (directionalInput.x == 0) {
				velocity.x = -wallDirX * wallJumpOff.x;
				velocity.y = wallJumpOff.y;
			}
			else {
				velocity.x = -wallDirX * wallLeap.x;
				velocity.y = wallLeap.y;
			}
		}
		if (controller.collisions.below) {
			if (controller.collisions.slidingDownMaxSlope) {
				if (directionalInput.x != -Mathf.Sign (controller.collisions.slopeNormal.x)) { // not jumping against max slope
					velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
					velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
				}
			} else {
				velocity.y = maxJumpVelocity;
			}
		}
	}

	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
		}
	}
		

	void HandleWallSliding() {
		wallDirX = (controller.collisions.left) ? -1 : 1;
		wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0) {
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (directionalInput.x != wallDirX && directionalInput.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				}
				else {
					timeToWallUnstick = wallStickTime;
				}
			}
			else {
				timeToWallUnstick = wallStickTime;
			}

		}

	}

	void CalculateVelocity() {
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
    }

    /// <summary>
    /// Adds ammo for primary weapon.
    /// </summary>
    /// <param name="amount"></param>
    public void AddAmmo(int amount)
    {
        GetComponent<ThrowScript>().PrimaryAmmo += amount;
    }
}