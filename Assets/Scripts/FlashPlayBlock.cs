using UnityEngine;
using System.Collections;

public class FlashPlayBlock : MonoBehaviour {

	private SpriteRenderer sr;

	void Start()
	{
		sr = this.GetComponent<SpriteRenderer> ();
	}

	void OnTriggerStay2D (Collider2D col)
	{
		if (Input.GetKey (KeyCode.E)) {
			sr.color = Color.blue;
		}
	}

}

