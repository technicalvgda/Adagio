using UnityEngine;
using System.Collections;

public class GlowScript : MonoBehaviour {

    Behaviour halo;         //Access the halo effect, AKA Glowing effect
    bool stepped = false;   //A boolean to know if we are in contact with the object

    // Use this for initialization
    void Start () {
	        halo = (Behaviour)GetComponent("Halo");
	        halo.enabled = true;
	    }

	// Update is called once per frame
	void Update () {
	        if (Input.GetKeyDown(KeyCode.E) && stepped) //If the player steps on the object and presses E, the glow will stop
		        {
		            halo.enabled = false;
		        }
	    }

    void OnTriggerEnter2D() { //When we collide with the object, stepped is true, if not false
	        stepped = true;
	    }
}