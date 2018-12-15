using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movment: MonoBehaviour
{
    public List<ThrowableScript> Items;

    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            var player = c.gameObject.GetComponent<Player>();
            var throwScript = player.GetComponent<ThrowScript>();

            if (throwScript.SpecialWeapon == null)
            {
                throwScript.SpecialWeapon = Create();
            }
            gameObject.SetActive(false);
            Invoke("Reactivate", 1);
        }
    }

    public ThrowableScript Create()
    {
        var prefab = Items[Random.Range(0, Items.Count)];



        return prefab;
    }


    public void Reactivate()
    {
        gameObject.SetActive(true);
    }
}
