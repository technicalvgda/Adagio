using UnityEngine;
using System.Collections;

public class RedDrum : MonoBehaviour {

    public bool pressed = false;
    public bool collide = false;


    public Transform point1;
    Vector3 start;
    Vector3 end;
    public float speed = .05f;
    //bool x = true;
    //int counter = 0;

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
