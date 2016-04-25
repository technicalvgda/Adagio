using UnityEngine;
using System.Collections;

public class PressurePlateFlash : MonoBehaviour {
	public GameObject BeginButton;
	public GameObject PlayBlock;
	private FlashPlayBlock playBlock;
	private bool stepped;
	public float time = 3;
	float min, max;
	float elapsedTime = 0;
	void Start()
	{
		stepped = false;
		playBlock = PlayBlock.GetComponent<FlashPlayBlock> ();
		playBlock.enabled = false;
		min = time - 0.1f;
		max = time + 0.1f;
	}

	// Counts for how long the 'E' button is held for and stores it in elapsedTime.
	void Update()
	{
		elapsedTime = Input.GetKeyDown (KeyCode.E) ? 0 : elapsedTime;
		if (Input.GetKey (KeyCode.E) && stepped) {
			Debug.Log ("Blue");
			PlayBlock.GetComponent<SpriteRenderer> ().color = Color.blue;
			elapsedTime += Time.deltaTime;
			Debug.Log (elapsedTime);
		}
		PlayBlock.GetComponent<SpriteRenderer> ().color = Color.black;
		playBlock.enabled = false;
	}
	// Used to reset back to original settings
	void OnTriggerEnter2D()
	{
		stepped = true;
	}
	void OnTriggerExit2D()
	{
		stepped = false;
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
