using UnityEngine;
using System.Collections;

public class FlashBeginButton : MonoBehaviour {
	public GameObject DemoBlock1, DemoBlock2, DemoBlock3;
	FlashDemoBlock demoBlock1, demoBlock2, demoBlock3;
	public GameObject PlayBlock1, PlayBlock2, PlayBlock3;
	FlashPlayBlock playBlock1, playBlock2, playBlock3;
	public GameObject PressurePlate1, PressurePlate2, PressurePlate3;
	public GameObject Metronome;
	public GameObject Player;
	public bool puzzleOver;
	private bool pb1 = true, pb2 = true, pb3 = true;

	// All 'E' button presses are disabled at game start for each play block.
	void Start()
	{
		puzzleOver = true;
		demoBlock1 = DemoBlock1.GetComponent<FlashDemoBlock> ();
		demoBlock2 = DemoBlock2.GetComponent<FlashDemoBlock> ();
		demoBlock3 = DemoBlock3.GetComponent<FlashDemoBlock> ();
		playBlock1 = PlayBlock1.GetComponent<FlashPlayBlock> ();
		playBlock2 = PlayBlock1.GetComponent<FlashPlayBlock> ();
		playBlock3 = PlayBlock1.GetComponent<FlashPlayBlock> ();
		playBlock1.enabled = false;
		playBlock2.enabled = false;
		playBlock3.enabled = false;



	}

	// If a playblock has the incorrect timing, the puzzle ends.
	void Update()
	{
		if (!pb1 || !pb2 || !pb3) {
			puzzleOver = true;
			restart ();
			pb1 = pb2 = pb3 = true;
		}
	}

	// All play blocks start true when pressure plate is entered.
	void OnTriggerStay2D(Collider2D col)
	{

		col.attachedRigidbody.sleepMode = RigidbodySleepMode2D.NeverSleep;
		if (Input.GetKeyDown (KeyCode.E) && puzzleOver == true) {
			Debug.Log("On");

			//Stopping Motion
			Player.GetComponent<PlayerController>().enabled = false;
			Player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			Player.GetComponent<Rigidbody2D>().angularVelocity =0f;
			//Resetting booleans
			puzzleOver = false;
			pb1 = true; pb2 = true; pb3 = true;
			//Demo caller
			StartCoroutine (Demo());
			//Metronome caller
			Metronome.GetComponent<Metronome> ().StartCoroutine("Blink");
		}

	}

	IEnumerator Demo()
	{
		//Time interval between flashing blocks
		yield return new WaitForSeconds (2);
		demoBlock1.Invoke ("beginDemo", 0);
		yield return new WaitForSeconds(4);
		demoBlock2.Invoke ("beginDemo", 0);
		yield return new WaitForSeconds (4);
		demoBlock3.Invoke ("beginDemo", 0); 
		yield return new WaitForSeconds (4);
		Player.GetComponent<PlayerController>().enabled = true;
		demoBlock1.Invoke ("onYourMarks", 0);
		demoBlock2.Invoke ("onYourMarks", 0);
		demoBlock3.Invoke ("onYourMarks", 0);
		yield return new WaitForSeconds (4);

		//Play caller
		StartCoroutine(Play());
	}

	IEnumerator Play()
	{
		//Play Block 1
		Debug.Log ("Go");
		PressurePlate1.GetComponent<PressurePlateFlash> ().enabled = true;
		yield return new WaitForSeconds (4);
		if (PressurePlate1.GetComponent<PressurePlateFlash> ().getElapsedTime () < PressurePlate1.GetComponent<PressurePlateFlash> ().getMin () || PressurePlate1.GetComponent<PressurePlateFlash> ().getElapsedTime () > PressurePlate1.GetComponent<PressurePlateFlash> ().getMax ()) {
			pb1 = false;
			Debug.Log ("Lose");
		} else
			Debug.Log ("1");
		PressurePlate1.GetComponent<PressurePlateFlash> ().enabled = false;

		//Play Block 2
		if (pb1) {
			Debug.Log ("Go");
			PressurePlate2.GetComponent<PressurePlateFlash> ().enabled = true;
			yield return new WaitForSeconds (4);
			if (PressurePlate2.GetComponent<PressurePlateFlash> ().getElapsedTime () < PressurePlate2.GetComponent<PressurePlateFlash> ().getMin () || PressurePlate2.GetComponent<PressurePlateFlash> ().getElapsedTime () > PressurePlate2.GetComponent<PressurePlateFlash> ().getMax ()) {
				pb2 = false;
				Debug.Log ("Lose");
			} else
				Debug.Log ("2");
			PressurePlate2.GetComponent<PressurePlateFlash> ().enabled = false;
		}
			
		//Play Block 3
		if (pb1 && pb2) {
			Debug.Log ("Go");
			PressurePlate3.GetComponent<PressurePlateFlash> ().enabled = true;
			yield return new WaitForSeconds (4);
			if (PressurePlate3.GetComponent<PressurePlateFlash> ().getElapsedTime () < PressurePlate3.GetComponent<PressurePlateFlash> ().getMin () || PressurePlate3.GetComponent<PressurePlateFlash> ().getElapsedTime () > PressurePlate3.GetComponent<PressurePlateFlash> ().getMax ()) {
				pb3 = false;
				Debug.Log ("Lose");
			} else
				Debug.Log ("3");
			PressurePlate3.GetComponent<PressurePlateFlash> ().enabled = false;
		}

		// You win!!
		if (pb1 && pb2 && pb3) 
		{
			// Do something here for winning.
			restart();
		}
			
		puzzleOver = true;

	}

	void restart()
	{
		// All Blocks restart to original settings
		DemoBlock1.GetComponent<FlashDemoBlock> ().Invoke ("restart", 0);
		DemoBlock2.GetComponent<FlashDemoBlock> ().Invoke ("restart", 0);
		DemoBlock3.GetComponent<FlashDemoBlock> ().Invoke ("restart", 0);
		playBlock1.enabled = false;
		playBlock2.enabled = false;
		playBlock3.enabled = false;
		Debug.Log("Off");
	}

}
