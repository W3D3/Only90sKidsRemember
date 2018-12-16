using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPlayerIndicatorHackScript : MonoBehaviour
{
    public Player Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.LookingRight)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
             transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
