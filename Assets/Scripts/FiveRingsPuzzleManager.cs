using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FiveRingsPuzzleManager : MonoBehaviour {
	//The ring that the player collides to start the puzzle
	public GameObject startRing;
	//The rings that move
	public GameObject ring1, ring2, ring3,ring4;
	//The blocks prefabs that surround the staring ring
	public GameObject theBlockWithBanner,theBlock;
	//the list of blocks that surround the ring in the center
	public List<GameObject> surroundingBlocks;
	//Bool for if the puzzle has started
	private bool puzzleStarted;
	//Bool for if the puzzle is complete
	private bool complete;
	//bool needed to execute each case statement once
	private bool finishedCaseStatement;
	//The number of times the player has jumped midair
	private int jumpCounter;
	//The raycasting script on the player
	private DirectionRaycasting2DCollider rayCast;
	private GameObject player;
	//The array of locations where the rings will travel to
	public GameObject[] patternLocations;
	//Blocks that are to be activated when the puzzle starts
	public GameObject[] additionalBlocksToSpawnOnStart;
	//the number of rings collected
	private int numbRingsCollected;
	//bool for if the puzzle has been reset
	private bool puzzleHasReset;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		puzzleStarted = false;
		rayCast = player.GetComponent<DirectionRaycasting2DCollider> ();
		jumpCounter = 0;
		surroundingBlocks = new List<GameObject> ();
		finishedCaseStatement = false;
		ring1.SetActive (false);
		ring2.SetActive (false);
		ring3.SetActive (false);
		ring4.SetActive (false);
		complete = false;
		puzzleHasReset = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		//if the player has collected all the moving rings and the ring in the center, then the player has finished the puzzle
		if (numbRingsCollected == 5 && !complete) 
		{
			Debug.Log ("You have finished the 5 Ring Puzzle");
			complete = true;
			Camera.main.GetComponent<OpenGate>().doneCounter++;
		}
		else if (numbRingsCollected == 4) 
		{
			//collected all the rings
			//deactivate the surrounding blocks
			for (int i = 0; i < surroundingBlocks.Count; i++) 
			{
				surroundingBlocks [i].SetActive (false);
			}
			//reset the radius of the ring in the center
			startRing.GetComponent<CircleCollider2D>().radius = 5;
		}
		//Puzzle started but not complete
		if (puzzleStarted && !complete) 
		{
			//If the player is standing, then reset the puzzle
			if (rayCast.collisionDown && !puzzleHasReset) 
			{
				resetPuzzle ();
				puzzleHasReset = true;
			}

			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				//if the player does a jump midair
				if (!rayCast.collisionDown) 
				{
					jumpCounter++;	
					//Debug.Log (jumpCounter);
					//set to false so it can execute the next case statement
					finishedCaseStatement = false;
					puzzleHasReset = false;
				}
			}

			switch (jumpCounter) 
			{
			case 3:
				{	
					if (finishedCaseStatement == false) {
						//surround the ring with blocks, simultaneously spawn them and add them to a list for later deletion
						surroundingBlocks.Add ((GameObject)Instantiate (theBlockWithBanner, new Vector3 (transform.position.x + 1.5f, transform.position.y, transform.position.z), Quaternion.identity));
						surroundingBlocks.Add ((GameObject)Instantiate (theBlockWithBanner, new Vector3 (transform.position.x+0.5f, transform.position.y + 1, transform.position.z), Quaternion.identity));
						surroundingBlocks.Add ((GameObject)Instantiate (theBlockWithBanner, new Vector3 (transform.position.x-0.5f, transform.position.y, transform.position.z), Quaternion.identity));
						surroundingBlocks.Add ((GameObject)Instantiate (theBlock, new Vector3 (transform.position.x+0.5f, transform.position.y - 1, transform.position.z), Quaternion.identity));
						//set to true to avoid executing this case statement continuously
						finishedCaseStatement = true;
					}
					break;
				}
			case 6:
				{
					if (finishedCaseStatement == false) 
					{
						ring1.SetActive (true);
						finishedCaseStatement = true;
					}
					break;
				}
			case 9:
				{
					if (finishedCaseStatement == false) 
					{					
						ring1.GetComponent<FiveRingsPuzzleRingScript> ().pattern1Begin ();
						finishedCaseStatement = true;
					}
					break;
				}
			case 12:
				{
					if (finishedCaseStatement == false)
					{
						ring2.SetActive (true);
						ring2.GetComponent<FiveRingsPuzzleRingScript> ().reverseTraversal = true;
						ring2.GetComponent<FiveRingsPuzzleRingScript> ().pattern1Begin ();
						finishedCaseStatement = true;
					}
					break;
				}
			case 15:
				{	if (finishedCaseStatement == false) 
					{
						ring3.SetActive(true);
						ring4.SetActive(true);
						ring3.GetComponent<FiveRingsPuzzleRingScript> ().pattern2Begin ();
						ring4.GetComponent<FiveRingsPuzzleRingScript> ().reverseTraversal = true;
						ring4.GetComponent<FiveRingsPuzzleRingScript> ().pattern2Begin ();
						finishedCaseStatement = true;
					}
					break;
				}
			}		
		}	
	}
	//function that starts the puzzle
	public void startPuzzle()
	{
		puzzleStarted = true;
		startRing.transform.position = transform.position+new Vector3(0.5f,0,0);
		//reduce radius so that the player won't accidentaly restart the puzzle by touching the collider, NEED TO RESET BACK TO 5 UPON PUZZLE COMPLETION
		startRing.GetComponent<CircleCollider2D>().radius = 1;
		for (int i = 0; i < additionalBlocksToSpawnOnStart.Length; i++) 
		{
			additionalBlocksToSpawnOnStart [i].SetActive (true);
		}
	}
	//returns the pattern locations
	public GameObject[] getPatternLocations()
	{
		return patternLocations;
	}
	public void collectedRing()
	{
		numbRingsCollected++;
	}
	//returns the number of rings collected
	public int getNumbRingsCollected()
	{
		return numbRingsCollected;
	}
	//function to reset the puzzle
	private void resetPuzzle()
	{
		//reactivate the first ring
		ring1.SetActive (true);
		//reset positions of each ring and deactivate movement
		ring1.GetComponent<FiveRingsPuzzleRingScript>().resetRing();
		ring2.GetComponent<FiveRingsPuzzleRingScript>().resetRing();
		ring3.GetComponent<FiveRingsPuzzleRingScript>().resetRing();
		ring4.GetComponent<FiveRingsPuzzleRingScript>().resetRing();
		//deactivate all rings except for ring1
		ring2.SetActive(false);
		ring3.SetActive(false);
		ring4.SetActive(false);
		//reactivate the blocks surrounding the starting ring
		for (int i = 0; i < surroundingBlocks.Count; i++) {
			surroundingBlocks [i].SetActive (true);
		}
		jumpCounter = 2;
		finishedCaseStatement = false;
		numbRingsCollected = 0;
	}
}
