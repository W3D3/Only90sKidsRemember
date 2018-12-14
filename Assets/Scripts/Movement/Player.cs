using System;
using UnityEngine;

namespace Assets.Scripts.Movement
{
    [RequireComponent (typeof (Controller2D))]
    public class Player : MonoBehaviour {

        public float jumpHeight = 4;
        public float moveSpeed = 10;
        public float timeToJumpApex = .4f;
        public float dash = 10;
        float accelerationTimeAirborne = .2f;
        float accelerationTimeGrounded = .1f;

        private int currentColor;

        public Vector2 wallLeap;
        
        public float wallSlideSpeedMax = 3;
        public float wallStickTime = .25f;
        float timeToWallUnstick;

        float gravity;
        float jumpVelocity;
        Vector3 velocity;
        float velocityXSmoothing;

        Controller2D controller;
        //private LevelInit level;

        private bool canDash;

        public Vector3 InitialPosition;
        public bool InputEnabled;
        public bool ZeroGravity;
        public int Deaths;

        public GamepadInput gamepadInput;

        void Start() {
            controller = GetComponent<Controller2D> ();
            //level = GetComponent<LevelInit>();

            gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
            jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

            canDash = true;

            InitialPosition = transform.position;
            ZeroGravity = false;
            Deaths = 0;
        }



        void Update() {
            Vector2 input = new Vector2 (gamepadInput.LeftHorizontalValue(), 0);
            int wallDirX = (controller.collisions.left) ? -1 : 1;

            float targetVelocityX = input.x * moveSpeed;
            if (ZeroGravity)
            {
                if (Mathf.Abs(velocity.x) < 0.0001f)
                {
                    velocity.x = Mathf.Abs(1 * Mathf.Sign(velocity.x)) < 0.0001f ? 1 : Mathf.Sign(velocity.x);
                }
            }
            else
            {
                velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            }

            bool wallSliding = false;
            
            if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && !ZeroGravity) {
                wallSliding = true;

                if (velocity.y < -wallSlideSpeedMax) {
                    velocity.y = -wallSlideSpeedMax;
                }

                if (timeToWallUnstick > 0) {
                    velocityXSmoothing = 0;
                    velocity.x = 0;

                    if (input.x != wallDirX && input.x != 0) {
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

            // todo check zero gravity
            if ((controller.collisions.above || controller.collisions.below) /*&& !controller.collisions.zeroGravity*/) {
                velocity.y = 0;
            }

            if (controller.collisions.below)
            {
                canDash = true;
            }
       
            if (gamepadInput.Jump()) {
                if (wallSliding) {
                    velocity.x = -wallDirX * wallLeap.x;
                    velocity.y = wallLeap.y;
                }
                if (controller.collisions.below) {
                    //GameManager.instance.playJumpSound();
                    velocity.y = jumpVelocity;
                }

			
            }

            if (gamepadInput.Dash() && canDash && input.normalized.x != 0)
            {
                //GameManager.instance.playDashSound();
                velocity.x = input.normalized.x * dash;
                velocity.y = 7; //very importante
                canDash = false;
            }
        
            if (!ZeroGravity)
            {
                velocity.y += gravity * Time.deltaTime;
            }

            controller.Move (velocity * Time.deltaTime, input);
        }
    }
}
