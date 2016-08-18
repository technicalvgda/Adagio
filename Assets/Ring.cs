using UnityEngine;
using System.Collections;

public class Ring : MonoBehaviour {
    public int drumNum = 1;
    public bool pressed = false;
    public bool collide = false;


    void Start() {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        pressed = true;

    }

    void OnTriggerExit2D(Collider2D col)
    {
        collide = false;
        pressed = false;
        Debug.Log("Exit");
    }
}
