using UnityEngine;
using System.Collections;

public class Codex : MonoBehaviour {
	public bool onCodex;
	public float travelDistance = 1f;
	public float speed = 1;
	private Vector3 codexPosition;
	private Vector3 startingPosition;

	private Vector3 newPosition;
	private bool goingUp;
	// Use this for initialization
	void Start () 
	{
		goingUp = true;
		onCodex = false;	
		startingPosition = this.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{		
		if (onCodex == true && Input.GetKeyDown (KeyCode.E)) 
		{
			Destroy (this.gameObject);
		}
		transform.position = new Vector3 (transform.position.x, Mathf.PingPong (Time.time*speed, travelDistance)+startingPosition.y, transform.position.z);

	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			onCodex = true;
		}
			
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			onCodex = false;
		}
	}
}
