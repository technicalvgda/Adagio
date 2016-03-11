using UnityEngine;
using System.Collections;

public class PressurePlateFlash : MonoBehaviour {
    public bool stepped;
    public bool released;
	float timing;

    void Start()
    {
		timing = 0f;
        stepped = false;
        released = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        stepped = true;
		Debug.Log("On");
		col.attachedRigidbody.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }
	void OnTriggerStay2D(Collider2D col)
	{
		Debug.Log(timing);
		timing+= Time.deltaTime;
	}

    void OnTriggerExit2D(Collider2D col)
    {
		if (timing > 0.9f && timing < 1.1f) {
			Debug.Log ("Correct");
		}
		timing = 0;
        stepped = false;
        released = true;
        Debug.Log("Off");
    }
}
