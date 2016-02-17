using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset; //Set relative position to player object

	// Use this for initialization
	void Start () {
		//Calculates offset by subtracting player position from camera object position
		offset = transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		if (player == null) {
			player = GameObject.Find ("PlayerPlaceholder(Clone)");
		} else {
			//Camera's new position is the player's position offsetted
			transform.position = player.transform.position + offset;
		}
	
	}
}
