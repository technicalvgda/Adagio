using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TimerPlatform : MonoBehaviour {
	
	public bool playerStanding = false;


	public Transform platform;
	public AudioClip timeSound;
	public AudioClip wrongSound;
	public AudioClip resetSound;
	public float timeToReset = 10;

	//givenlist indicates blocks that player needs to step on.
	private List<string> givenList;

	private AudioSource[] sounds;
	//time sound
	private AudioSource sound1;
	//wrong sound
	private AudioSource sound2;
	//reset sound
	private AudioSource sound3;
	private PlayerController player;
	private BoxCollider2D bc2d;

	//to keep track of the time
	private static float time;

	//to keep track of the time started and ended;
	private bool timeEndTrigger = false;

	private bool reset = false;
	private bool barTrigger = false;



	void Awake() {
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();

		bc2d = GetComponent<BoxCollider2D> ();
		givenList = new List<string> {"TimerPlatformBlue", "TimerPlatformPurple",
			"TimerPlatformRed", "TimerPlatformOrange", "TimerPlatformYellow"};

		sounds = GetComponents<AudioSource> ();
		sound1 = sounds [0];
		sound2 = sounds [1];
		sound3 = sounds [2];
	
	
	}

	void Start () {

	}
	void Update() {
		if(playerStanding && player.downButton) {
			bc2d.isTrigger = true;
		}
		//current time
		time = Time.time;


		if (barTrigger) {
			DoorBell.timeStarted = time;
			if (!timeEndTrigger) {
				
				DoorBell.timeEnded = DoorBell.timeStarted + timeToReset;
				timeEndTrigger = true;
			}

			if ((DoorBell.timeStarted - DoorBell.timeEnded) > 0) {
				GameObject theDoorBell = GameObject.Find ("DoorBellRoom");
				if (theDoorBell != null) {
					sound3.PlayOneShot(resetSound);
					DoorBell doorBell = theDoorBell.GetComponent<DoorBell> ();
					doorBell.blockList.Clear ();
					barTrigger = false;
					DoorBell.timeEnded = 0;
					timeEndTrigger = false;
				}

			}
		}


	}
		
	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Player") {
			playerStanding = true;
			checkPlayer ();
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

	//check if the player steps on a platform and add to the list.
	void checkPlayer() {

		if (playerStanding) {
			//if the player is on the platform, trigger the sound
			GameObject rayDown = GameObject.Find ("Player");
			DirectionRaycasting2DCollider ray = rayDown.GetComponent<DirectionRaycasting2DCollider> ();
			if (ray.collisionDown && !ray.collisionLeft && !ray.collisionRight && !ray.collisionUp) {

				Debug.Log ("Time in checkPlayer" +time);
				GameObject theDoorBell = GameObject.Find ("DoorBellRoom");
				if (theDoorBell != null) {
					DoorBell doorBell = theDoorBell.GetComponent<DoorBell> ();
					doorBell.blockList.Add (platform.name);
					
					int j = 0;
					for (int i = 0; i < doorBell.blockList.Count; i++) {
						if (platform.name == "TimerPlatformBlue" && !barTrigger) {
							barTrigger = true;

						}
						if (doorBell.blockList [i].Equals (givenList [i])) {
							j++;
							reset = false;
							if (j == givenList.Count) {
								GameObject obj = GameObject.Find ("DoorBellRoom");
								Destroy (obj);
							}
						} else {
							reset = true;
							break;
						}
					}

					if (!reset) {
						sound2.PlayOneShot (timeSound);

					} else if (reset) {
						sound1.PlayOneShot (wrongSound);
						doorBell.blockList.Clear ();
						barTrigger = false;
						DoorBell.timeEnded = 0;


					} 
				} //end if theDoorBell is null
			}	 // end ray
		} //end player standing 


	}//end checkPlayer
		

}