using UnityEngine;
using System.Collections;

public class Saving : MonoBehaviour {
	[SerializeField]
	bool playerContact = false;
	bool isSaved;

	SpriteRenderer sr;
	void Awake () {
		sr = GetComponent<SpriteRenderer> ();
	}
	void Start () {
		//Debug.Log (PlayerPrefs.GetInt ("Save"));
		//Gets the value of "Save" 
		if (PlayerPrefs.GetInt ("Save") == 1){
			isSaved = true;
		} else {
			isSaved = false;
		}
		if(isSaved) {
			//Sets the color to blue if saved
			sr.color = new Color(0,0,1);
		} else {
			//Sets the color to red if not
			sr.color = new Color(1,0,0);
		}
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.E) && playerContact){
			Save ();
		} 

		if(Input.GetKeyDown(KeyCode.Q) && playerContact){
			ClearSave ();
		} 
	}

	//Tests if the player is contacting with block
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

	//Saves a int value in the registry on windows
	void Save () {
		//Since there are no "SetBoolean", SetInt is used instead 
		//Stores 1 in the variable "Save"
		if (!isSaved) {
			PlayerPrefs.SetInt ("Save", 1);
			sr.color = new Color (0, 0, 1);
			Debug.Log ("Saving");
			isSaved = true;
		} else {
			Debug.Log ("Already Saved");
		}
	}

	void ClearSave() {
		PlayerPrefs.SetInt ("Save", 0);
		sr.color = new Color(1,0,0);
		Debug.Log ("Clearing Save");
		isSaved = false;
	}
}
