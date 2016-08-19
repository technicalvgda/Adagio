using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class NewChimePuzzleButton : MonoBehaviour {
    public GameObject button;
    public AudioSource audio;
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
        audio.Play();
        if (col.gameObject.tag == "Button")
        {
            audio.Play();
        }

    }
    void OnTriggerExit2D(Collider2D col)
    {
        collide = false;
        pressed = false;

        Debug.Log("Exit");
        count = 0;
    }
}
