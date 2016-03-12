using UnityEngine;
using System.Collections;

public class PressurePlateFlash : MonoBehaviour {
    public bool stepped;
    public bool released;
	float elapsedtime;

    void Start()
    {
		elapsedtime = 0f;
        stepped = false;
        released = false;
    }
	void Update()
	{
		elapsedtime = Input.GetKeyDown (KeyCode.E) ? 0 : elapsedtime;
		if (stepped == true && Input.GetKey(KeyCode.E)) {
			elapsedtime += Time.deltaTime;
			Debug.Log(elapsedtime);
		}

		if (elapsedtime > 0.9f && elapsedtime < 1.1f && stepped == true && Input.GetKeyUp (KeyCode.E)) {
			Debug.Log ("Correct");
			elapsedtime = 0;
		}
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        stepped = true;
		Debug.Log("On");
		col.attachedRigidbody.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }
		
    void OnTriggerExit2D(Collider2D col)
    {
		elapsedtime = 0;
        stepped = false;
        released = true;
        Debug.Log("Off");
    }
}
