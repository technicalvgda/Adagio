using UnityEngine;
using System.Collections;
/* Select the receiving object in the button's inspection window. 
 * Can select multiple objects to be the receiver.
 * Decided to not use tags as this is a much simpler and quicker way to handmake puzzle rooms.
 */
public class TrapDoorOpenTrigger : MonoBehaviour {
	private Rigidbody2D test;
	GameObject player;
	public TrapDoorOpenResponse receiver,receiver2,receiver3; //can be set to activate multiple objects at once
	bool playerContact = false;
	// Use this for initialization
	void Start (){
	}
	
	// Update is called once per frame
	void Update () {
		if(playerContact == true && Input.GetKeyDown(KeyCode.E)){
			receiver.Deactivate ();
			//Extra stuff to activate if desired
			receiver2.Deactivate ();
			receiver3.Deactivate ();
			gameObject.SetActive (false);

		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player") {
			playerContact = true;
		}
	}
}
