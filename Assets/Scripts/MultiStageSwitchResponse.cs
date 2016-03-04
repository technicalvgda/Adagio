using UnityEngine;
using System.Collections;

public class MultiStageSwitchResponse : MonoBehaviour {
	SpriteRenderer sr;
	public bool stateOfSwitch;
    bool playerContact;

	// Use this for initialization
	void Start ()
    {
			sr = this.GetComponent<SpriteRenderer>();
	        stateOfSwitch = false;
	        playerContact = false;
		}

	// Update is called once per frame
	void Update ()
    {
		       if (playerContact)
		        {
		            if (Input.GetKeyDown(KeyCode.E))
			            {
			                stateOfSwitch = !stateOfSwitch;
							
			            }
		        }
	
		if (stateOfSwitch) {
			//Debug.Log("Switch is on!!!");
			sr.color = new Color (0.3f, 1, 0.3f);
		} else {
			sr.color = new Color(1, 0.3f, 0.3f);
		}
			}
	

	    void OnTriggerEnter2D(Collider2D c)
	    {
		        playerContact = true;
		        //Debug.Log("Activating");
		    }

	    void OnTriggerExit2D(Collider2D c)
	    {
		        playerContact = false;
		        //Debug.Log("DeActivated");
		    }
	

	}