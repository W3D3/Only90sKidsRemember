using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    public bool PrimaryWeaponCharging;

    public float SpeedPrimaryWeapon = 0.5f;
    public float SpeedSpecialWeapon = 0.5f;

    public KeyCode ActiveKeyCode;
    
    private Transform Position;

    public GamepadInput input;
    public Player Player;

    public int PrimaryAmmo = 3;
    
    public bool SpecialWeaponCharging;

    public ThrowableScript PrimaryWeapon;
    public ThrowableScript SpecialWeapon;

    public Animator animator;

    public SpriteRenderer SpriteCharge;
    
    // Start is called before the first frame update
    void Start()
    {
        SpeedPrimaryWeapon = 1f;
        Position = GetComponent<Transform>();
        input = GetComponent<GamepadInput>();
        Player = GetComponent<Player>();
        animator = GetComponent<Animator>();

        SpriteCharge = GetComponentsInChildren<SpriteRenderer>().Last();
        SpriteCharge.size = new Vector2(0, SpriteCharge.size.y);
    }

    private void HandleShootPrimaryWeapon()
    {
        if (input.IsRegularFirePressed() && !PrimaryWeaponCharging && !SpecialWeaponCharging && PrimaryAmmo > 0)
        {
            PrimaryWeaponCharging = true;
            SpeedPrimaryWeapon = 10f;
        }


        if (PrimaryWeaponCharging)
        {
            CrossHair.enabled = true;
            CrossHair.startWidth = 0.1f;
            CrossHair.endWidth = 0.6f;
            // HACK this is only needed because assets are fucking flipped
            Vector3 shootingDirectionAbsolute =
                new Vector3(Math.Abs(ShootingDirection.x)*-1, ShootingDirection.y, 0);
            Vector3 ch = (shootingDirectionAbsolute * (SpeedPrimaryWeapon / PrimaryWeapon.MaxSpeed) * 0.6f * 10); // vector for crosshair
            ch.z = 1;
            CrossHair.SetPositions(new Vector3[] { Vector3.zero, ch });

            var maxSpeed = PrimaryWeapon.MaxSpeed;
            if (SpeedPrimaryWeapon != maxSpeed)
            {
                SpeedPrimaryWeapon += Time.deltaTime * PrimaryWeapon.SpeedStep;
                
                // apply charging animation
                var width = SpeedPrimaryWeapon / maxSpeed * 0.6f;
                SpriteCharge.size = new Vector2(width, SpriteCharge.size.y);

                if (SpeedPrimaryWeapon > maxSpeed)
                    SpeedPrimaryWeapon = maxSpeed;
                
                
            }
        }
        else
        {
            CrossHair.enabled = false;
        }

        if (input.IsRegularFireReleased() && PrimaryWeaponCharging)
        {
            PrimaryWeaponCharging = false;
            SpriteCharge.size = new Vector2(0, SpriteCharge.size.y);
            
            // throw primary weapon
            var throwable = Instantiate(PrimaryWeapon);
            throwable.gameObject.transform.position = Position.position + ThrowOffset;
            throwable.Thrower = Player;
            throwable.SetSpeed(ShootingDirection, SpeedPrimaryWeapon);
            animator.Play("throw");
            SoundManager.instance.playThrowIt();
            CrossHair.SetPositions(new Vector3[] { Vector3.zero });

            --PrimaryAmmo;
        }
    }

    /// <summary>
    /// If the user isnt moving, shoot in the looking direction
    /// </summary>
    private Vector2 ShootingDirection
    {
        get
        {
            float horizontalVal = input.GetRightHorizontalValue() != 0
                   ? input.GetRightHorizontalValue()
                   : Player.LookingRight ? 1 : -1;

            return new Vector3(horizontalVal, input.GetRightVerticalValue());
        }
    }

    /// <summary>
    /// The offset to start the Throwable.
    /// </summary>
    private Vector3 ThrowOffset
    {
        get { return ShootingDirection.normalized * GetComponent<Collider2D>().bounds.size / 1.5f; }
    }

    private void HandleShootSpecialWeapon()
    {
        if (input.IsSpecialFirePressed() && !SpecialWeaponCharging && !PrimaryWeaponCharging && SpecialWeapon != null)
        {
            SpecialWeaponCharging = true;
            SpeedSpecialWeapon = 0.5f;
        }

        if (SpecialWeaponCharging)
        {
            var maxSpeed = SpecialWeapon.MaxSpeed;
            if (SpeedSpecialWeapon != maxSpeed)
            {
                SpeedSpecialWeapon += Time.deltaTime * SpecialWeapon.SpeedStep;

                // apply charging animation
                var width = SpeedSpecialWeapon / maxSpeed * 0.6f;
                SpriteCharge.size = new Vector2(width, SpriteCharge.size.y);

                if (SpeedSpecialWeapon > maxSpeed)
                    SpeedSpecialWeapon = maxSpeed;
            }
        }

        if (input.IsSpecialFireReleased() && SpecialWeaponCharging)
        {
            SpecialWeaponCharging = false;
            SpriteCharge.size = new Vector2(0, SpriteCharge.size.y);
            
            // throw special weapon
            var throwable = Instantiate(SpecialWeapon);
            throwable.gameObject.transform.position = Position.position + ThrowOffset;
            throwable.Thrower = Player;
            throwable.SetSpeed(ShootingDirection, SpeedSpecialWeapon);
            animator.Play("throw");
            SoundManager.instance.playThrowIt();
                var cmps = Player.GetComponentsInChildren<SpriteRenderer>();
                cmps[cmps.Length - 2].sprite = null;
            
            SpecialWeapon = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleShootPrimaryWeapon();

        HandleShootSpecialWeapon();
    }
    
    private LineRenderer CrossHair
    {
        get { return GetComponent<LineRenderer>(); }
    }
}
