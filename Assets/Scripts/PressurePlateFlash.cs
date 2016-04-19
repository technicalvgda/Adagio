using UnityEngine;
using System.Collections;

public class PressurePlateFlash : MonoBehaviour {
	public GameObject DemoBlock1, DemoBlock2, DemoBlock3;
	public GameObject PlayBlock1, PlayBlock2, PlayBlock3;
	public GameObject Metronome;
    public bool stepped;
    public bool released;
	bool pause = true;
	private bool pb1 = true, pb2 = true, pb3 = true;
	public float elapsedTime;

    void Start()
    {
		elapsedTime = 0f;
        stepped = false;
        released = false;
		PlayBlock1.GetComponent<FlashPlayBlock> ().enabled = false;
		PlayBlock2.GetComponent<FlashPlayBlock> ().enabled = false;
		PlayBlock3.GetComponent<FlashPlayBlock> ().enabled = false;

    }
	void Update()
	{
		if (!pb1 || !pb2 || !pb3) {
			GetComponent<FlashPanButton> ().puzzleOver = true;
		}
	}

    void OnTriggerEnter2D(Collider2D col)
    {
		pb1 = true; pb2 = true; pb3 = true;
        stepped = true;
		released = false;
		Debug.Log("On");
		col.attachedRigidbody.sleepMode = RigidbodySleepMode2D.NeverSleep;
		//Demo caller
		StartCoroutine (Demo ());
		//Metronome caller
		Metronome.GetComponent<Metronome>().Invoke("beginBlink", 0);
    }

	IEnumerator Demo()
	{
		//Time interval between flashing blocks
		yield return new WaitForSeconds (2);
		DemoBlock1.GetComponent<FlashDemoBlock> ().Invoke ("beginDemo", 0);
		yield return new WaitForSeconds(4);
		DemoBlock2.GetComponent<FlashDemoBlock> ().Invoke ("beginDemo", 0);
		yield return new WaitForSeconds (4);
		DemoBlock3.GetComponent<FlashDemoBlock> ().Invoke ("beginDemo", 0); 
		yield return new WaitForSeconds (4);
		DemoBlock1.GetComponent<FlashDemoBlock> ().Invoke ("onYourMarks", 0);
		DemoBlock2.GetComponent<FlashDemoBlock> ().Invoke ("onYourMarks", 0);
		DemoBlock3.GetComponent<FlashDemoBlock> ().Invoke ("onYourMarks", 0);
		yield return new WaitForSeconds (6);

		//Play caller
		StartCoroutine(Play());
	}

	IEnumerator Play()
	{
		
		Debug.Log ("Stop");
		//StartCoroutine(gamePause ());
		Debug.Log ("Go");
		PlayBlock1.GetComponent<FlashPlayBlock> ().enabled = true;
		yield return new WaitForSeconds (3);
		if (PlayBlock1.GetComponent<FlashPlayBlock> ().getElapsedTime () < PlayBlock1.GetComponent<FlashPlayBlock> ().getMin () || PlayBlock1.GetComponent<FlashPlayBlock> ().getElapsedTime () > PlayBlock1.GetComponent<FlashPlayBlock> ().getMax ()) {
			pb1 = false;
			Debug.Log ("Lose");
		} else
			Debug.Log ("Win");
		PlayBlock1.GetComponent<FlashPlayBlock> ().enabled = false;



		/*
		//StartCoroutine(gamePause ());
		PlayBlock2.GetComponent<FlashPlayBlock> ().Invoke ("keyTime", 0);
		if (PlayBlock2.GetComponent<FlashPlayBlock> ().getElapsedTime () < PlayBlock2.GetComponent<FlashPlayBlock> ().getMin () || PlayBlock2.GetComponent<FlashPlayBlock> ().getElapsedTime () > PlayBlock2.GetComponent<FlashPlayBlock> ().getMax ()) {
			pb2 = false;
		}

		//StartCoroutine (gamePause ());
		PlayBlock3.GetComponent<FlashPlayBlock> ().Invoke ("keyTime", 0);
		if (PlayBlock3.GetComponent<FlashPlayBlock> ().getElapsedTime () < PlayBlock3.GetComponent<FlashPlayBlock> ().getMin () || PlayBlock3.GetComponent<FlashPlayBlock> ().getElapsedTime () > PlayBlock3.GetComponent<FlashPlayBlock> ().getMax ()) {
			pb3 = false;
		}
		*/


	}

	IEnumerator gamePause()
	{
		while (pause)
		{
			if (Input.GetKeyDown (KeyCode.E)) 
			{
				pause = false;
			}
			yield return new WaitForSeconds (1);
		}
		pause = true;
		yield return null;
	}


    void OnTriggerExit2D(Collider2D col)
    {
        stepped = false;
        released = true;
		DemoBlock1.GetComponent<FlashDemoBlock> ().Invoke ("restart", 0);
		DemoBlock2.GetComponent<FlashDemoBlock> ().Invoke ("restart", 0);
		DemoBlock3.GetComponent<FlashDemoBlock> ().Invoke ("restart", 0);
		PlayBlock1.GetComponent<FlashPlayBlock> ().Invoke ("restart", 0);
		PlayBlock2.GetComponent<FlashPlayBlock> ().Invoke ("restart", 0);
		PlayBlock3.GetComponent<FlashPlayBlock> ().Invoke ("restart", 0);

		Debug.Log("Off");
    }


}
