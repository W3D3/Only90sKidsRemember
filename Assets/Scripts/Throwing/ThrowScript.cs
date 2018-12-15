using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    public bool CanCharge = true;
    public bool ChargeNormalWeapon;

    public float Speed = 0.5f;
    public float SpeedSpecialWeapon = 0.5f;

    public KeyCode ActiveKeyCode;

    public Vector3 ThrowOffset;
    private Transform Position;

    public GamepadInput input;
    public Player Player;

    public int PrimaryAmmo = 3;

    public bool CanUseSpecialWeapon;
    public bool ChargeSpecialWeapon;

    public ThrowableScript PrimaryWeapon;
    public ThrowableScript SpecialWeapon;

    public Animator animator;

    public SpriteRenderer SpriteCharge;
    
    // Start is called before the first frame update
    void Start()
    {
        Speed = 1f;
        Position = GetComponent<Transform>();
        input = GetComponent<GamepadInput>();
        Player = GetComponent<Player>();
        animator = GetComponent<Animator>();

        SpriteCharge = GetComponentsInChildren<SpriteRenderer>()[1];
        SpriteCharge.size = new Vector2(0, SpriteCharge.size.y);
    }

    private void HandleShootPrimaryWeapon()
    {
        var maxSpeed = PrimaryWeapon.MaxSpeed;

        if (input.IsRegularFirePressed() && CanCharge && !ChargeSpecialWeapon)
        {
            CanCharge = false;
            ChargeNormalWeapon = true;
            Speed = 10f;
        }

        if (ChargeNormalWeapon)
        {
            Speed += Time.deltaTime * PrimaryWeapon.SpeedStep;
            
            var width = Speed / maxSpeed * 0.6f;
            SpriteCharge.size = new Vector2(width, SpriteCharge.size.y);
            if (Speed > maxSpeed)
            {
                ChargeNormalWeapon = false;
                Speed = maxSpeed;
            }
        }

        if (input.IsRegularFireReleased())
        {
            ChargeNormalWeapon = false;
            PrimaryAmmo--;
            CanCharge = PrimaryAmmo > 0;

            if (PrimaryAmmo > 0)
            {
                var collider2d = GetComponent<Collider2D>();

                // if the user isnt moving, shoot in the looking direction
                var horiVal = input.GetRightHorizontalValue() != 0
                    ? input.GetRightHorizontalValue()
                    : Player.LookingRight ? 1 : -1;

                var direction = new Vector2(horiVal, input.GetRightVerticalValue());

                ThrowOffset = direction.normalized * collider2d.bounds.size / 1.5f;
            var throwable = Instantiate(PrimaryWeapon);
            throwable.gameObject.transform.position = Position.position + ThrowOffset;
            throwable.Thrower = Player;
            throwable.SetSpeed(direction, Speed);
            animator.Play("throw");
            }
        }
    }

    private void HandleShootSpecialWeapon()
    {
        if (input.IsSpecialFirePressed() && CanUseSpecialWeapon && !ChargeNormalWeapon)
        {
            SpeedSpecialWeapon = 0.5f;
            CanUseSpecialWeapon = false;
            ChargeSpecialWeapon = true;
        }

        if (ChargeSpecialWeapon)
        {
            if (SpecialWeapon != null)
            {
                SpeedSpecialWeapon += Time.deltaTime * SpecialWeapon.SpeedStep;
                var width = SpeedSpecialWeapon / SpecialWeapon.MaxSpeed * 0.6f;
                SpriteCharge.size = new Vector2(width, SpriteCharge.size.y);

                if (SpeedSpecialWeapon > SpecialWeapon.MaxSpeed)
                {
                    ChargeSpecialWeapon = false;
                    SpeedSpecialWeapon = SpecialWeapon.MaxSpeed;
                }
            }
        }

        if (input.IsSpecialFireReleased())
        {
            ChargeSpecialWeapon = false;
            CanUseSpecialWeapon = true;

            SpriteCharge.size = new Vector2(0, SpriteCharge.size.y);

            if (SpecialWeapon != null)
            {
                CanUseSpecialWeapon = false;
                var collider2d = GetComponent<Collider2D>();

                var direction = new Vector2(input.GetRightHorizontalValue(), input.GetRightVerticalValue());

                ThrowOffset = direction.normalized * collider2d.bounds.size / 1.5f;

                var throwable = Instantiate(SpecialWeapon);
                throwable.gameObject.transform.position = Position.position + ThrowOffset;
                throwable.Thrower = Player;
                throwable.SetSpeed(direction, SpeedSpecialWeapon);

                Player.GetComponentInChildren<SpriteRenderer>().sprite = null;

                SpecialWeapon = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleShootPrimaryWeapon();

        HandleShootSpecialWeapon();
    }
}
