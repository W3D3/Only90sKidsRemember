using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerScript : MonoBehaviour
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var player = collision.gameObject.GetComponent<Player>();
             var throwable = Create();
        }
    }

    public ThrowableScript Create()
    {
        var prefab = Items[Random.Range(0, Items.Count)];



        return prefab;
    }

}
