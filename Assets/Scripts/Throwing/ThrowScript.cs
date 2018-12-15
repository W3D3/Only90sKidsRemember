using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    public bool CanCharge = true;
    public bool Charging;
    public float Speed = 0.5f;
    public KeyCode ActiveKeyCode;

    public Vector3 ThrowOffset;
    private Player Player;
    public ThrowableScript ThrowablePrefab;


    // Start is called before the first frame update
    void Start()
    {
        Speed = 1f;
        Player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(ActiveKeyCode) && CanCharge)
        {
            CanCharge = false;
            Charging = true;
            Speed = 0.5f;
        }

        if (Charging)
        {
            // todo: add velocity
            Speed += Time.deltaTime * 20f;
        }

        if (Input.GetKeyUp(ActiveKeyCode))
        {
            CanCharge = true;
            Charging = false;

            // todo: check if the player can throw item
            // todo: determine item

            var throwable = Instantiate(ThrowablePrefab);
            throwable.gameObject.transform.position = Player.transform.position + ThrowOffset;
            throwable.InstantiateSpeed(Speed);
        }
    }

    void OnDrawGizmos()
    {
        var center = Player.transform.position + ThrowOffset;
        Gizmos.DrawLine(center - Vector3.down, center + Vector3.up);
        Gizmos.DrawLine(center - Vector3.right, center + Vector3.left);
    }
}
