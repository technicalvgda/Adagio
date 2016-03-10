using UnityEngine;
using System.Collections;

public class PuzzleRoomCollider : MonoBehaviour {
	public GameObject door;

	void Update(){
		if(Input.GetKeyDown("q")){
			Debug.Log("Door opens");
			door.SetActive (false);
		}
	}

	void OnTriggerEnter2D(Collider2D obj) {
		Debug.Log("Entered Puzzle Room");
		door.SetActive (true);

	}

}
