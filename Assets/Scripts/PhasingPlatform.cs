using UnityEngine;
using System.Collections;

public class PhasingPlatform : MonoBehaviour {
	private PlayerController player;
	private BoxCollider2D bc2d;
	public bool playerStanding = false;
	void Awake() {
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
		bc2d = GetComponent<BoxCollider2D> ();
	}

	void Update() {
		if(playerStanding && player.downButton) {
			bc2d.isTrigger = true;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Player") {
			playerStanding = true;
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		if(col.gameObject.tag == "Player") {
			playerStanding = false;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			playerStanding = false;
			bc2d.isTrigger = false;
		}
	}


}
