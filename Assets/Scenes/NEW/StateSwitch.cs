using UnityEngine;
using System.Collections;

// USE: Press E while player is on top of this to cycle through its colors

public class StateSwitch : MonoBehaviour
{
    SpriteRenderer sr;
    bool stepped;
    int state;

	void Start ()
    {
        // Get a reference to the SpriteRenderer so that we can change the button's color.
        sr = this.GetComponent<SpriteRenderer>();
        stepped = false;
        Switch(state = 0);
    }

    void Update()
    {
        // If the player is colliding with the button and E is pressed, advance the state.
        // This check should be placed in Update() instead of one of the below functions
        // since Update() executes more frequently and is less likely to cause missed checks.
        if (stepped && Input.GetKeyDown(KeyCode.E))
        {
            // Cycles the state counter forward and loops
            Switch(state = (state + 1) % 3);
        }
    }

    // NOTE: Set the Player object's RigidBody2D -> Sleeping Mode to Never Sleep, otherwise
    // the collision check will eventually stop due to the player object going to sleep.
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            stepped = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            stepped = false;
    }

    void Switch(int state)
    {
        switch (state)
        {
            case 0:
                sr.color = new Color(1, 0.3f, 0.3f);
                break;
            case 1:
                sr.color = new Color(0.3f, 1, 0.3f);
                break;
            case 2:
                sr.color = new Color(0.3f, 0.3f, 1);
                break;
            default:
                break;
        }
    }
}
