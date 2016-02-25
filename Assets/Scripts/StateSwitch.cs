using UnityEngine;
using System.Collections;

public class StateSwitch : MonoBehaviour {

    public bool stateOfSwitch;
    bool playerContact;

	// Use this for initialization
	void Start ()
    {
        stateOfSwitch = false;
        playerContact = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playerContact)
        {
            //Debug.Log("Player contact");
            if (Input.GetKeyDown(KeyCode.E))
            {
                stateOfSwitch = !stateOfSwitch;
            }
        }

        if (stateOfSwitch)
        {
            //Debug.Log("Switch is on!!!");
        }
	}


    void OnCollisionEnter2D(Collision2D c)
    {
        playerContact = true;
        //Debug.Log("Activating");
    }
    void OnCollisionStay2D(Collision2D c)
    {
        playerContact = true;
        //Debug.Log("Activated");
    }
    void OnCollisionExit2D(Collision2D c)
    {
        playerContact = false;
        //Debug.Log("DeActivated");
    }


}
