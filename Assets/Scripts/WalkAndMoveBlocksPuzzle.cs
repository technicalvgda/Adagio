using UnityEngine;
using System.Collections;

public class WalkAndMoveBlocksPuzzle : MonoBehaviour {
	public GameObject ring,GoldBlocks,BlueBlocks,BrownBlocks;
	private bool finishedPuzzle;
	private bool doneExecutingCode;
	// Use this for initialization
	void Start () {
		doneExecutingCode = false;
	}
	
	// Update is called once per frame
	void Update () {
		//only want this to execute once so set doneExecutingCode to true inside
		if (!doneExecutingCode) {
			if (finishedPuzzle) {
				Debug.Log ("You have completed the Walk and Move Blocks puzzle!");
				Camera.main.GetComponent<OpenGate> ().doneCounter++;
				//GoldBlocks.SetActive (false);
				//BlueBlocks.SetActive (false);
				//BrownBlocks.SetActive (false);
				doneExecutingCode = true;
			}
		}
	}

	public void playerFinishedPuzzle()
	{
		finishedPuzzle = true;
	}
	public bool puzzleIsCompleted()
	{
		return finishedPuzzle;
	}
}