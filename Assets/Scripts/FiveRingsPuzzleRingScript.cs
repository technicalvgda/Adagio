using UnityEngine;
using System.Collections;

public class FiveRingsPuzzleRingScript : MonoBehaviour {
	//The manager for the puzzle
	private GameObject fiveRingsManager;
	//bools for pattern1, pattern 2, and if the ring is moving
	private bool pattern1Init,pattern2Init, isMoving;
	public GameObject[] locations;
	//the speed of the ring
	public float speed;
	//bool for if the ring ISerializationCallbackReceiver to travel the opposite way
	public bool reverseTraversal;
	//the counter to keep track of what location is next for the ring to move towards
	private int locationCounter;
	public float frequency;//Speed of sine movement
	public float magnitude;//Size of sine movement

	//some variables to make the ring move in a sine wave
	private Vector3 axis, pos;
	float fTime = 0;
	private Transform target;
	public float rotationSpeed;
	private Quaternion lookRotation;
	private Vector3 dir;

	//the initial location where the ring started
	private Vector3 beginningPos;
	// Use this for initialization
	void Start () {
		fiveRingsManager = GameObject.Find ("5StringManager");
		pattern1Init = false;
		pattern2Init = false;
		locationCounter = 0;
		reverseTraversal = false;
		pos = transform.position;
		beginningPos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		if (!isMoving) 
		{
			//initialize pattern 1 movement
			if (pattern1Init) {
				isMoving = true;
				StartCoroutine ("Pattern1Traversal");
			} 
			else if (pattern2Init) 
			{
				isMoving = true;
				StartCoroutine ("Pattern2Traversal");
			}
		}
	
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (gameObject.name == "StartRing") 
		{
			if (other.gameObject.tag == "Player") 
			{
				//if the player has collected all the other rings
				if (fiveRingsManager.GetComponent<FiveRingsPuzzleManager> ().getNumbRingsCollected() == 4) 
				{
					gameObject.SetActive (false);
					fiveRingsManager.GetComponent<FiveRingsPuzzleManager> ().collectedRing ();
				}
				else
					fiveRingsManager.GetComponent<FiveRingsPuzzleManager> ().startPuzzle ();
			}
		} 
		else 
		{
			gameObject.SetActive (false);
			fiveRingsManager.GetComponent<FiveRingsPuzzleManager> ().collectedRing ();
		}
	}

	public void pattern1Begin()
	{
		pattern1Init = true;
		locations = fiveRingsManager.GetComponent<FiveRingsPuzzleManager> ().getPatternLocations ();
	}

	public void pattern2Begin()
	{
		pattern2Init = true;
		locations = fiveRingsManager.GetComponent<FiveRingsPuzzleManager> ().getPatternLocations ();
	}
	//HAndles the movement, forward and reversed traversal of the rings
	IEnumerator Pattern1Traversal()
	{
		if (!reverseTraversal) {
			while (transform.position != locations [locationCounter].transform.position) {
				transform.position = Vector3.MoveTowards (transform.position, locations [locationCounter].transform.position, speed * Time.deltaTime);
				if (transform.position == locations [locationCounter].transform.position) {
					locationCounter++;
					if (locationCounter == locations.Length)
						locationCounter = 0;
					//break;
				}
				yield return null;
			}
		} 
		else 
		{
			locationCounter = 4;
			while (transform.position != locations [locationCounter].transform.position) {
				transform.position = Vector3.MoveTowards (transform.position, locations [locationCounter].transform.position, speed * Time.deltaTime);
				if (transform.position == locations [locationCounter].transform.position) {
					locationCounter--;
					if (locationCounter < 0) {
						locationCounter = locations.Length-1;
					}
					//break;
				}
				yield return null;
			}
		}
		yield return null;
	}

	//hamdles the sine wave movement for the rings
	IEnumerator Pattern2Traversal()
	{
		
		if (!reverseTraversal) 
		{
			locationCounter = 6;
			while (transform.position != locations [locationCounter].transform.position) 
			{
				//rotates the ring towards the next location
				Vector3 vectorToTarget = locations [locationCounter].transform.position - transform.position;
				float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
				Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
				transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * rotationSpeed);
				//handles the math for the sine wave
				pos += transform.right * Time.deltaTime * speed;
				axis = transform.up;
				transform.position = pos + axis * Mathf.Sin (Time.time * frequency) * magnitude;
				Debug.DrawLine (pos, transform.position, Color.green, 100);

				if (transform.position.x > (locations [locationCounter].transform.position.x - 0.5f) && transform.position.x < (locations [locationCounter].transform.position.x + 0.5f)) 
				{
					if (transform.position.y > (locations [locationCounter].transform.position.y - 0.25f) && transform.position.y < (locations [locationCounter].transform.position.y + 0.25f)) 
					{
						transform.position = locations [locationCounter].transform.position;
						locationCounter--;
						if (locationCounter < 0) {
							locationCounter = locations.Length - 1;
						}
					}
				}

				yield return null;
			}
		} 
		else 
		{
			locationCounter = 6;
			while (transform.position != locations [locationCounter].transform.position) 
			{
				Vector3 vectorToTarget = locations [locationCounter].transform.position - transform.position;
				float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
				Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
				transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * rotationSpeed);

				pos += transform.right * Time.deltaTime * speed;
				axis = transform.up;				
				transform.position = pos + axis * Mathf.Sin (Time.time * frequency) * magnitude;
				Debug.DrawLine (pos, transform.position, Color.red, 100);

				if (transform.position.x > (locations [locationCounter].transform.position.x - 0.25f) && transform.position.x < (locations [locationCounter].transform.position.x + 0.25f)) 
				{
					if (transform.position.y > (locations [locationCounter].transform.position.y - 0.25f) && transform.position.y < (locations [locationCounter].transform.position.y + 0.25f)) 
					{
						transform.position = locations [locationCounter].transform.position;
						locationCounter++;
						if (locationCounter == locations.Length) {
							locationCounter = 0;
						}
					}
				}

				yield return null;
			}
		}
		yield return null;
	}
	//function to reset values of the ring
	public void resetRing()
	{
		transform.position = beginningPos;
		pattern1Init = false;
		pattern2Init = false;
		locationCounter = 0;
		reverseTraversal = false;
		isMoving = false;
		pos = transform.position;
		StopAllCoroutines ();
	}
}
