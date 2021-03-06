﻿using UnityEngine;
using System.Collections;
/* Select the receiving object in the button's inspection window. 
 * Can select multiple objects to be the receiver.
 * Decided to not use tags as this is a much simpler and quicker way to handmake puzzle rooms.
 */
public class Button : MonoBehaviour {
	private Rigidbody2D test;
	GameObject player;
	public ButtonResponse receiver,receiver2,receiver3; //can be set to activate multiple objects at once
	bool playerContact = false;
	// Use this for initialization
	void Start (){
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E)){
			Activate ();
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player") {
			playerContact = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
				if (col.tag == "Player") {
						playerContact = false;
				}
			}

	//Subscribe Activate method to the OnTap Event when object becomes active
	void OnEnable(){
		PlayerController.OnTap += Activate;
	}
	//Unsubscrite Activate method from the OnTap event when object becomes deactive
	void OnDisable(){
		PlayerController.OnTap -= Activate;
	}

	void Activate(){
		if(playerContact == true){
			receiver.Activate ();
			//Extra stuff to activate if desired
			receiver2.Activate ();
			receiver3.Activate ();
		}
	}
}
