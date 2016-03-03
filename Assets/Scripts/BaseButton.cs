using UnityEngine;
using UnityEngine.Events;
using System.Collections;

// This button is "pressed" by placing the player object on it and pressing E
// When pressed, the button is able to call functions from any object

// Add this script to a button
// Under the area that says On Pressed(), drag the object whose function needs to be called into it
// In the drop down box that currently says No Function, look for the desired function
// Any public function with fundamental data type parameters can be selected

public class BaseButton : MonoBehaviour
{
    public UnityEvent onPressed;
    bool pressed;

    void Update ()
    {
        if (pressed && Input.GetKeyDown(KeyCode.E))
            onPressed.Invoke();
    }

    //public virtual void OnPressed() {}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            pressed = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            pressed = false;
    }
}
