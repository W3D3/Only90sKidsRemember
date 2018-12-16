using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerScript: MonoBehaviour
{
    public List<ThrowableScript> Items;

    /// <summary>
    /// Time until the spawner respawns.
    /// </summary>
    public float RespawnTime;

    /// <summary>
    /// If true, an already collected special weapon is overwritten.
    /// </summary>
    public bool OverwriteOldWeapon;
    
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

            if (throwScript.SpecialWeapon == null || OverwriteOldWeapon)
            {
                var weapon = Create();
                throwScript.SpecialWeapon = weapon;
            }

            gameObject.SetActive(false);
            Invoke("Reactivate", RespawnTime);
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
