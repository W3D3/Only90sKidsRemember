using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    public bool CanCharge = true;
    public bool Charging;
    public float Speed = 0.5f;
    public KeyCode ActiveKeyCode;

    public ThrowableScript ThrowablePrefab;


    // Start is called before the first frame update
    void Start()
    {
        Speed = 1f;
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
            throwable.gameObject.transform.position = transform.position;
            throwable.InstantiateSpeed(Speed);
        }
    }
}
