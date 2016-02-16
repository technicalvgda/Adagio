using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpForce;

	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		//Reference so I don't have to type this long thing out repeatedly
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Use FixedUpdate with Rigidbody
	void FixedUpdate () {
		//The "jump" mechanic
		if (Input.GetKey (KeyCode.W)) {
			rb2d.AddForce ( new Vector2(rb2d.velocity.x, jumpForce), ForceMode2D.Impulse);
		}
			
		float moveHorizontal = Input.GetAxis ("Horizontal");

		rb2d.velocity = new Vector2 (moveHorizontal * speed, rb2d.velocity.y);
	}
}
