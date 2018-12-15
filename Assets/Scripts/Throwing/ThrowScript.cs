using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    public bool CanCharge = true;
    public bool Charging;
    
    public float Speed = 0.5f;
    public float SpeedSpecialWeapon = 0.5f;

    public KeyCode ActiveKeyCode;
    public float MaxSpeed = 30f;

    public Vector3 ThrowOffset;
    private Transform Position;

    public GamepadInput input;
    public Player Player;

    public int PrimaryAmmo = 3;

    public bool CanUseSpecialWeapon;
    public bool ChargeSpecialWeapon;

    public ThrowableScript PrimaryWeapon;
    public ThrowableScript SpecialWeapon;


    // Start is called before the first frame update
    void Start()
    {
        Speed = 1f;
        Position = GetComponent<Transform>();
        input = GetComponent<GamepadInput>();
        Player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.IsRegularFirePressed() && CanCharge)
        {
            CanCharge = false;
            Charging = true;
            Speed = 0.5f;
        }

        if (Charging)
        {
            Speed += Time.deltaTime * PrimaryWeapon.SpeedStep;

            if (Speed > MaxSpeed)
            {
                Charging = false;
                Speed = MaxSpeed;
            }
        }

        if (input.IsRegularFireReleased())
        {
            Charging = false;
            PrimaryAmmo--;
            CanCharge = PrimaryAmmo > 0;

            if (PrimaryAmmo > 0)
            {

                var collider2d = GetComponent<Collider2D>();

                var direction = new Vector2(input.GetRightHorizontalValue(), input.GetRightVerticalValue());

                ThrowOffset = direction.normalized * collider2d.bounds.size / 1.5f;

                var throwable = Instantiate(PrimaryWeapon);
                throwable.gameObject.transform.position = Position.position + ThrowOffset;
                throwable.SetSpeed(direction, Speed);
                Debug.Log(direction * Speed);
            }
        }

        #region special weapon

        if (input.IsSpecialFirePressed() && CanUseSpecialWeapon)
        {
            CanUseSpecialWeapon = false;
            ChargeSpecialWeapon = true;
            Speed = 0.5f;
        }



        if (ChargeSpecialWeapon)
        {
            SpeedSpecialWeapon += Time.deltaTime * SpecialWeapon.SpeedStep;

            if (SpeedSpecialWeapon > MaxSpeed)
            {
                ChargeSpecialWeapon = false;
                SpeedSpecialWeapon = MaxSpeed;
            }
        }

        if (input.IsSpecialFireReleased())
        {
            CanUseSpecialWeapon = true;
            ChargeSpecialWeapon = false;

            if (SpecialWeapon != null)
            {
                var collider2d = GetComponent<Collider2D>();

                var direction = new Vector2(input.GetRightHorizontalValue(), input.GetRightVerticalValue());

                ThrowOffset = direction.normalized * collider2d.bounds.size / 1.5f;

                var throwable = Instantiate(SpecialWeapon);
                throwable.gameObject.transform.position = Position.position + ThrowOffset;
                throwable.Thrower = Player;
                throwable.SetSpeed(direction, SpeedSpecialWeapon);

                SpecialWeapon = null;
            }
        }

        #endregion
    }
}
