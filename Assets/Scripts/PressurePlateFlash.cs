using UnityEngine;
using System.Collections;

public class PressurePlateFlash : MonoBehaviour {
    public bool stepped;
    public bool released;

    void Start()
    {
        stepped = false;
        released = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        stepped = true;
        Debug.Log("On");
    }

    void OnTriggerExit2D(Collider2D col)
    {
        stepped = false;
        released = true;
        Debug.Log("Off");
    }
}
