using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyColor
{
    Red,
    Green,
    Gold
}

public class Key : PickUp
{
    public Material red;
    public Material green;
    public Material gold;

    void SetMyColor()
    {
        switch(color)
        {
            case KeyColor.Red:
                GetComponent<Renderer>().material = red;
                break;
            case KeyColor.Green:
                GetComponent<Renderer>().material = green;
                break;
            case KeyColor.Gold:
                GetComponent<Renderer>().material = gold;
                break;
        }
    }



    public KeyColor color;

    public override void Picked()
    {
        GameMenager.gameMenager.AddKey(color);
        Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        SetMyColor();
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }
}
