using UnityEngine;
using System.Collections;

// USE: Press E while player is on top of this to cycle through its colors

public class ColorToMatchButton : MonoBehaviour
{
	public ColorToSwitch colS;
	bool stepped;
	int state;
	public bool match;
	public ColorToMatch colM;

	void Start ()
	{
		stepped = false;
		colS.Cswitch(state = -1);
	}

	void Update()
	{
		// If the player is colliding with the button and E is pressed, advance the state.
		// This check should be placed in Update() instead of one of the below functions
		// since Update() executes more frequently and is less likely to cause missed checks.
		if (stepped && Input.GetKeyDown (KeyCode.E)) {
			// Cycles the state counter forward and loops
			colS.Cswitch (state = (state + 1) % 3);
			//Compares colors instead of index to allow more "random" color cycling in the ColorToSwitch.
			if (colM.getColor() == colS.getColor()) {
				match = true;
				Debug.Log ("TRUE");
			} else {
				match = false;
			}
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
	public bool getMatch()
	{
		return match;
	}


}