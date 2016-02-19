using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

    public float smoothTime = 0.5f;

	private Vector3 offset; //Set relative position to player object

    private Vector3 currentVelocity; //Velocity of the camera

	// Use this for initialization
	void Start () {
		//Calculates offset by subtracting player position from camera object position
		offset = transform.position;
	}
	
	// Update is called once per frame
    // Changed from LateUpdate to FixedUpdate so that Camera follows character more smoothly.
	void FixedUpdate () {
		
		if (player == null)
        {
            player = GameObject.Find("PlayerPlaceholder(Clone)");
        }
        else {
            //Camera's new position is the player's position offsetted
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z), ref currentVelocity, smoothTime);
		}
	}
}
