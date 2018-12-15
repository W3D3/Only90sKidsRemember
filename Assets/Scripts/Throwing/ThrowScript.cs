using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    public bool CanCharge = true;
    public bool Charging;
    public float Speed = 0.5f;
    public KeyCode ActiveKeyCode;
    public float MaxSpeed = 30f;

    public Vector3 ThrowOffset;
    private Transform Position;

    public GamepadInput input;

    public int PrimaryAmmo = 3;
    public ThrowableScript SpecialWeapon;


    public ThrowableScript PrimaryWeapon;
    public List<ThrowableScript> SpecialCandidates;


    // Start is called before the first frame update
    void Start()
    {
        Speed = 1f;
        Position = GetComponent<Transform>();
        SpecialCandidates = new List<ThrowableScript>();
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
        Debug.Log(Charging);
        if (Charging)
        {
            Speed += Time.deltaTime * 10f;

            if (Speed > MaxSpeed)
            {
                Charging = false;
                Speed = MaxSpeed;
            }
        }

        if (input.IsRegularFireReleased() && PrimaryAmmo > 0)
        {
            Charging = false;
            PrimaryAmmo--;
            CanCharge = PrimaryAmmo > 0;


            var direction = new Vector2(input.GetRightHorizontalValue(), input.GetRightVerticalValue());

            ThrowOffset = direction.normalized * 1f;

            var throwable = Instantiate(PrimaryWeapon);
            throwable.gameObject.transform.position = Position.position + ThrowOffset;

            // todo will be fixed
            throwable.InstantiateSpeed(direction, Speed);

            Debug.Log(direction);
        }
    }

    public void SetDirectionalInput(GamepadInput xxx)
    {
        this.input = xxx;
    }

    void OnDrawGizmos()
    {
        if (Position == null)
        {
            Position = GetComponent<Transform>();
        }


        var center = Position.position + ThrowOffset;

        Debug.Log(center);
        Gizmos.DrawLine(center - Vector3.down, center + Vector3.up);
        Gizmos.DrawLine(center - Vector3.right, center + Vector3.left);
    }
}
