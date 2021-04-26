using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : PickUp
{
    public int points = 5;
    public override void Picked()
    {
        GameMenager.gameMenager.AddPoints(points);
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }
}
