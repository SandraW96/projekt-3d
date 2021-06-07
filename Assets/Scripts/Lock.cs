using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{

    bool iCanOpen = false;
    public Door[] doors;
    public KeyColor myColor;
    bool locked = false;
    Animator key;

    public Material red;
    public Material gold;
    public Material green;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            iCanOpen = true;
            Debug.Log("You can use key");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            iCanOpen = false;
            Debug.Log("You can't use key");
        }
    }

    public bool CheckTheKey()
    {
        if (GameMenager.gameMenager.redKey >0 && myColor == KeyColor.Red)
        {
            GameMenager.gameMenager.redKey--;
            locked = true;
            return true;
        }
        else if (GameMenager.gameMenager.greenKey>0 && myColor == KeyColor.Green)
        {
            GameMenager.gameMenager.greenKey--;
            locked = true;
            return true;
        }
        else if (GameMenager.gameMenager.goldkey > 0 && myColor == KeyColor.Gold)
        {
            GameMenager.gameMenager.goldkey--;
            locked = true;
            return true;
        }
        else
        {
            Debug.Log("Nie masz klucza");
            return false;
        }
    }

    public void UseKey()
    {
        foreach(Door door in doors)
        {
            door.OpenClose();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        key = GetComponent<Animator>();
    }

    public Renderer myLock;

    void SetMyColor()
    {
        switch (myColor)
        {
            case KeyColor.Red:
                GetComponent<Renderer>().material = red;
                myLock.material = red;
                break;
            case KeyColor.Gold:
                GetComponent<Renderer>().material = gold;
                myLock.material = gold;
                break;
            case KeyColor.Green:
                GetComponent<Renderer>().material = green;
                myLock.material = green;
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && iCanOpen && !locked)
        {
            key.SetBool("use key", CheckTheKey());
        }
    }
}
