using UnityEngine;
using System.Collections;

public class PressurePlateFlash : MonoBehaviour {
	public GameObject DemoBlock1, DemoBlock2, DemoBlock3;
	public GameObject PlayBlock1, PlayBlock2, PlayBlock3;
    public bool stepped;
    public bool released;
	private bool db1 = true, db2 = true, db3 = true;
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
		elapsedTime = Input.GetKeyDown (KeyCode.E) ? 0 : elapsedTime;
		if (stepped == true && Input.GetKey (KeyCode.E)) {
			elapsedTime += Time.deltaTime;
			Debug.Log (elapsedTime);
		}
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
		StartCoroutine (DoSomething ());


    }

	IEnumerator DoSomething()
	{
		Debug.Log ("Here");
		DemoBlock1.GetComponent<FlashDemoBlock> ().Invoke ("begin", 1);
		yield return new WaitForSeconds(5);
		DemoBlock2.GetComponent<FlashDemoBlock> ().Invoke ("begin", 1);
		yield return new WaitForSeconds (5);
		DemoBlock3.GetComponent<FlashDemoBlock> ().Invoke ("begin", 1);
	}

	/*
	void OnTriggerStay2D(Collider2D col)
	{
		
	}
	*/
		
    void OnTriggerExit2D(Collider2D col)
    {
        stepped = false;
        released = true;
        Debug.Log("Off");
    }
}
