using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {

    public GameObject button;

    public bool pressed = false;
    public bool collide = false;
    int count = 0;
    

    void Start()
    {


    }
    void Update()
    {
        if (collide)
        {
            if (count == 0)
            {
                pressed = true;
                Debug.Log("ON");
                count++;
            }

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        Debug.Log("Enter");

        collide = true;
        Debug.Log(collide);


    }
    void OnTriggerExit2D(Collider2D col)
    {
        collide = false;
        pressed = false;
        Debug.Log("Exit");
        count = 0;
    }
}
