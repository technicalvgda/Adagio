using UnityEngine;
using System.Collections;

public class FlashPlayBlock : MonoBehaviour {

	public GameObject pressurePlate;
	private SpriteRenderer sr;
	public float time = 3;
	float min, max;
	float elapsedTime = 0;
	bool finish;
	// Initializes the sprite renderer and the minimum and maximum time thresholds
	// for the player pressing the 'E' button.
	void Start()
	{
		sr = this.GetComponent<SpriteRenderer> ();
		min = time - 0.1f;
		max = time + 0.1f;
		finish = false;
	}
	// Counts for how long the 'E' button is held for and stores it in elapsedTime.
	void Update()
	{
		elapsedTime = Input.GetKeyDown (KeyCode.E) ? 0 : elapsedTime;
		if (Input.GetKey (KeyCode.E)) {
			sr.color = Color.blue;
			elapsedTime += Time.deltaTime;
			Debug.Log (elapsedTime);
		}
	}
	// Used to reset back to original settings
	void restart()
	{
		sr.color = Color.black;
	}
    
	public float getElapsedTime()
	{
		return elapsedTime;
	}
	public float getMin()
	{
		return min;
	}
	public float getMax()
	{
		return max;
	}
}

