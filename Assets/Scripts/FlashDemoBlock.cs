using UnityEngine;
using System.Collections;

public class FlashDemoBlock : MonoBehaviour {
	private SpriteRenderer sr;
	public float time; // Time needed for the demo to flash white.


	void Start()
	{
		sr = this.GetComponent<SpriteRenderer> ();
	}

	//Helper method for begin()
	public void beginDemo()
	{
		StartCoroutine(begin());
	}

	// Demo for how long each block should be held.
	IEnumerator begin()
	{
		sr.color = Color.white;
		//Controls how long the flash stays on
		yield return new WaitForSeconds(time);
		sr.color = Color.black;
	}

	//Helper method for go()
	public void onYourMarks()
	{
		StartCoroutine(go());
	}

	// Marks the beginning of the puzzle in a countdown style.
	IEnumerator go()
	{
		sr.color = Color.red;
		yield return new WaitForSeconds (2);
		sr.color = Color.yellow;
		yield return new WaitForSeconds (2);
		sr.color = Color.green;

	}
	//Resets all blocks to orignial standards.
	void restart()
	{
		sr.color = Color.black;
	}
}
