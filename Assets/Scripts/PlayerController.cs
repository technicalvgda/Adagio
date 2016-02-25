using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpSpeed;
	public float blockJumpTimerDuration = 1.0f;

	private float blockJumpTimer = 0f;
	private Vector2 currentVelocity;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		//Reference so I don't have to type this long thing out repeatedly
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
		//The jump mechanic
		if (blockJumpTimer > 0) 
		{
			blockJumpTimer -= Time.deltaTime;
		}   
		else if (Input.GetKeyDown(KeyCode.Space))
		{
			//Player can jump if they are falling or reached max height
			if (rb2d.velocity.y <= 0) {
				//Making it directly alter vertical velocity so jump is instantaneous as well
				//as not super powerful.
				rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
			} 
			//If player tries to jump before apex, they cannot jump for a set time
			else {
				blockJumpTimer = blockJumpTimerDuration;
			}
		}

		float moveHorizontal = Input.GetAxis("Horizontal");

		currentVelocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
		rb2d.velocity = currentVelocity;
	}

	//Jump restriction removed if player touches anything
	void OnCollisionEnter2D(Collision2D col) {
		blockJumpTimer = 0f;
	}
	void OnTriggerEnter2D(Collider2D other) // "collecting basic item" mechanic
	{
		int item_counter = 0; // declare item counter to increment
		if (other.gameObject.CompareTag ("Item")) // when colliding with item
		{
			other.gameObject.SetActive (false); // deactives item when collided
			item_counter = item_counter + 1; // increment item counter
		}
	}
}
