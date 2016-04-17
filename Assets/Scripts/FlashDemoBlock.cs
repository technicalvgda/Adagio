using UnityEngine;
using System.Collections;

public class FlashDemoBlock : MonoBehaviour {
	private SpriteRenderer sr;
	public float time;

	void Start()
	{
		sr = this.GetComponent<SpriteRenderer> ();
	}

	public void beginDemo()
	{
		// Demo for how long each block should be held.
		StartCoroutine(begin());
	}
	IEnumerator begin()
	{
		sr.color = Color.white;
		//Controls how long the flash stays on
		yield return new WaitForSeconds(time);
		sr.color = Color.black;
	}
	public void onYourMarks()
	{
		// Marks the beginning of the puzzle
		StartCoroutine(go());
	}
	IEnumerator go()
	{
		sr.color = Color.red;
		yield return new WaitForSeconds (2);
		sr.color = Color.yellow;
		yield return new WaitForSeconds (2);
		sr.color = Color.green;
	}
}
