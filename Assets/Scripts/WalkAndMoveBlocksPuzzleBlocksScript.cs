using UnityEngine;
using System.Collections;

public class WalkAndMoveBlocksPuzzleBlocksScript : MonoBehaviour {
	public WalkAndMoveBlocksPuzzle manager;
	public GameObject blocksToMove;
	public bool isTouchingBlocks,touchedWall,blocksTouchingRing,blocksTouchingBlocks;
	private GameObject player;
    private DirectionRaycasting2DCollider playerRaycast, blocksToMoveRayCast;//thisRayCast;
	public float speed;
	private float moveHorizontal;
    private Rigidbody2D blocksToMoveRB2D;//rb2D;
	private Vector2 currentVelocity;
	//private bool didCoroutineOnce;
	public GameObject theRing,otherBlocks;
	// Use this for initialization

	void Start () {
		isTouchingBlocks = false;
		player = GameObject.Find ("Player");
		playerRaycast = player.GetComponent<DirectionRaycasting2DCollider> ();
		blocksToMoveRB2D = blocksToMove.GetComponent<Rigidbody2D> ();
		blocksToMoveRayCast = blocksToMove.GetComponent<DirectionRaycasting2DCollider> ();
		//thisRayCast = GetComponent<DirectionRaycasting2DCollider> ();
		//rb2D = GetComponent<Rigidbody2D> ();
		//didCoroutineOnce = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (blocksToMove.GetComponent<WalkAndMoveBlocksPuzzleBlocksScript> ().isTouchingRing ()) 
		{
			//check to see where the ring is with respect to the blocks
			//if the ring is to the left of the block
			if (theRing.transform.position.x <= blocksToMove.transform.position.x) 
			{
				if ((moveHorizontal = Input.GetAxis ("Horizontal")) < 0) 
				{
					move ();
				} else
					blocksToMoveRB2D.velocity = Vector2.zero;
			} 
			else if (theRing.transform.position.x >= blocksToMove.transform.position.x) 
			{
				if ((moveHorizontal = Input.GetAxis ("Horizontal")) > 0) 
				{
					move ();
				} else
					blocksToMoveRB2D.velocity = Vector2.zero;
			}
		} /*
		else if (blocksToMove.GetComponent<WalkAndMoveBlocksPuzzleBlocksScript>().isTouchingOtherColoredBlocks())
		{
			if (otherBlocks != null) {
				if (blocksToMove.transform.position.x <= otherBlocks.transform.position.x) {
					if ((moveHorizontal = Input.GetAxis ("Horizontal")) < 0) {
						blocksToMove.GetComponent<WalkAndMoveBlocksPuzzleBlocksScript> ().move ();
					} else {
						blocksToMoveRB2D.velocity = Vector2.zero;
						otherBlocks.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
					}
				} else if (blocksToMove.transform.position.x >= otherBlocks.transform.position.x) {
					if ((moveHorizontal = Input.GetAxis ("Horizontal")) > 0) {
						blocksToMove.GetComponent<WalkAndMoveBlocksPuzzleBlocksScript> ().move ();
					} else {
						blocksToMoveRB2D.velocity = Vector2.zero;
						otherBlocks.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
					}
				}
			}

		}*/
		else if (blocksToMoveRayCast.collisionRight) 
		{
			if ((moveHorizontal = Input.GetAxis ("Horizontal")) > 0) 
			{
				move ();
			} 
			else
				blocksToMoveRB2D.velocity = Vector2.zero;
		} 
		else if (blocksToMoveRayCast.collisionLeft) 
		{
			if ((moveHorizontal = Input.GetAxis ("Horizontal")) < 0) 
			{
				move ();
			} 
			else
				blocksToMoveRB2D.velocity = Vector2.zero;
		} 
		else if (isTouchingBlocks && playerRaycast.collisionDown && !playerRaycast.collisionLeft && !playerRaycast.collisionRight) 
		{
			move ();
		} 
		else
			blocksToMoveRB2D.velocity = Vector2.zero;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player") {
			isTouchingBlocks = true;

		} 
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			isTouchingBlocks = false;
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "WalkAndMoveBlock") 
		{

			otherBlocks = other.gameObject;
			blocksTouchingBlocks = true;

			blocksToMoveRB2D.velocity = Vector2.zero;
			/*
			blocksToMoveRB2D.angularVelocity = 0f;
			otherBlocks.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			otherBlocks.GetComponent<Rigidbody2D> ().angularVelocity = 0f;*/
		}
		if (other.tag == "Ring") {
			blocksTouchingRing = true;

		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "WalkAndMoveBlock") 
		{
			blocksTouchingBlocks = false;
		}
		if (other.tag == "Ring") {
			blocksTouchingRing = false;

		}
	}
	public void move()
	{
		moveHorizontal = Input.GetAxis ("Horizontal");
		currentVelocity = new Vector2 (moveHorizontal * speed *-1, 0);
		blocksToMoveRB2D.velocity = currentVelocity;
	}
	public bool isTouchingRing()
	{
		return blocksTouchingRing;
	}
	public bool isTouchingOtherColoredBlocks()
	{
		return blocksTouchingBlocks;
	}
}
