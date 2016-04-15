using UnityEngine;
using System.Collections;

public class PressurePlateFlash : MonoBehaviour {
	public GameObject DemoBlock1, DemoBlock2, DemoBlock3;
	public GameObject PlayBlock1, PlayBlock2, PlayBlock3;
	public GameObject Metronome;
    public bool stepped;
    public bool released;
	private bool pb1 = true, pb2 = true, pb3 = true;
	public float elapsedTime;

    void Start()
    {
		elapsedTime = 0f;
        stepped = false;
        released = false;
    }
	void Update()
	{
		if (!pb1 || !pb2 || !pb3) {
			GetComponent<FlashPanButton> ().puzzleOver = true;
		}
	}

    void OnTriggerEnter2D(Collider2D col)
    {
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
		Debug.Log ("Here");
		DemoBlock1.GetComponent<FlashDemoBlock> ().Invoke ("beginDemo", 0);
		//Time interval between flashing blocks
		yield return new WaitForSeconds(4);
		DemoBlock2.GetComponent<FlashDemoBlock> ().Invoke ("beginDemo", 0);
		//Time interval between flashing blocks
		yield return new WaitForSeconds (2);
		DemoBlock3.GetComponent<FlashDemoBlock> ().Invoke ("beginDemo", 0); 
		//Play caller
		//StartCoroutine(Play());
	}
	/*
	IEnumerator Play()
	{
		//pb1 = PlayBlock1.GetComponent<FlashPlayBlock> ().Invoke ("keyTime", 1);
		//pb2 = PlayBlock2.GetComponent<FlashPlayBlock> ().Invoke ("keyTime", 1);
		//pb3 = PlayBlock3.GetComponent<FlashPlayBlock> ().Invoke ("keyTime", 1);
	}
	*/
		
    void OnTriggerExit2D(Collider2D col)
    {
        stepped = false;
        released = true;
        Debug.Log("Off");
    }
}
