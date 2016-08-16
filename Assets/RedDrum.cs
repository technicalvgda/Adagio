using UnityEngine;
using System.Collections;

public class RedDrum : MonoBehaviour {

    public bool pressed = false;
    public bool collide = false;


    void OnTriggerEnter2D(Collider2D col)
    {

        Debug.Log("Enter");

        pressed = true;
        Debug.Log(collide);


    }
    void OnTriggerExit2D(Collider2D col)
    {
        collide = false;
        pressed = false;
        Debug.Log("Exit");
    }
}
