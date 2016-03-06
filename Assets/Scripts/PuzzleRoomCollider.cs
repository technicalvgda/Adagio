using UnityEngine;
using System.Collections;

public class PuzzleRoomCollider : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D obj) {
		Debug.Log("Entered Puzzle Room");
	}

}
