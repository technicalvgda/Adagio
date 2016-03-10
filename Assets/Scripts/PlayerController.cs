using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpSpeed;
	public float blockJumpTimerDuration = 1.0f;

	private float blockJumpTimer = 0f;
	private Vector2 currentVelocity;
	private Rigidbody2D rb2d;

    private Animator anim;

	// Use this for initialization
	void Start () {
		//Reference so I don't have to type this long thing out repeatedly
		rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator>();
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
        //set the walking animation variable to the axis, 
        //use that to check if moving and in which direction
        anim.SetFloat("Walking", moveHorizontal);

        currentVelocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
       
		rb2d.velocity = currentVelocity;
	}

	//Jump restriction removed if player touches anything
	void OnCollisionEnter2D(Collision2D col) {
		blockJumpTimer = 0f;
	}
}
