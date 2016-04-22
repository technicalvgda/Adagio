using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject player;
	private Rigidbody2D playerRB2D;
	public float damping = 5; //higher values for more snapping normally
	public float catchupDivisor = 2f; //Lower values for more snapping when falling
	private Vector3 offset; //Set relative position to player object
	private float cameraCatchup;
	public float playerCameraOffset;
	void Awake() {

#if UNITY_IPHONE
   Screen.sleepTimeout = SleepTimeout.NeverSleep;
#endif

#if UNITY_ANDROID
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
#endif
        playerRB2D = GameObject.FindWithTag ("Player").GetComponent<Rigidbody2D> ();
	}

	    // Use this for initialization
	void Start() {
		        //Calculates offset by subtracting player position from camera object position
		offset = transform.position;
	}

	    // Update is called once per frame
	    // Changed from LateUpdate to FixedUpdate so that Camera follows character more smoothly.
	void FixedUpdate() {
		
		if (player == null) {
			//player = GameObject.Find ("Player");
            player = GameObject.FindGameObjectWithTag("Player");
		} else {
			//if player if falling then set the camera catchup to the absolute of the player velocity divided by catchupDivisor
			if (playerRB2D.velocity.y < 0.0f) {
				cameraCatchup = Mathf.Abs (playerRB2D.velocity.y / catchupDivisor);
			} else {
				cameraCatchup = 0.0f;
			}
			//Follow the player code
			transform.position = Vector3.Lerp (transform.position, new Vector3 (player.transform.position.x, player.transform.position.y+playerCameraOffset, transform.position.z),  Time.deltaTime*(damping+cameraCatchup));
		}

	}


		
}
