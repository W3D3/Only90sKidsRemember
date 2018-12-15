using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LegoScript : ThrowableScript
{
    // Start is called before the first frame update
    public List<Color32> BrickColors;

    void Start()
    {

        var sprite = GetComponent<SpriteRenderer>();
        sprite.color = BrickColors[Random.Range(0, BrickColors.Count)];

    }

    // Update is called once per frame
    void Update()
    {
    }
}
