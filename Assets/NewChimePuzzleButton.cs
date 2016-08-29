using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class NewChimePuzzleButton : MonoBehaviour {
    public GameObject button;
    public AudioSource audioSource;
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
        audioSource.Play();
        if (col.gameObject.tag == "Button")
        {
            audioSource.Play();
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
