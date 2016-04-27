﻿using UnityEngine;
using System.Collections;

public class GongSwitch : MonoBehaviour {

    // Use this for initialization
    public bool stepped;
    int state = 0;
    public bool pressed;
    public bool cheat;

    void Start()
    {
        cheat = false;
        stepped = false;
        pressed = false;

    }

    void Update()
    {
        // If the player is colliding with the button and E is pressed, advance the state.
        // This check should be placed in Update() instead of one of the below functions
        // since Update() executes more frequently and is less likely to cause missed checks.
        if (stepped && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("ON");
            pressed = true;
        }
        if (stepped && Input.GetKeyDown(KeyCode.K))
        {
            cheat = true;
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
}
