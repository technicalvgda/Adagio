using UnityEngine;
using System.Collections;

public class PressurePlateFlash : MonoBehaviour {
    public bool stepped;
    public bool released;
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
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        stepped = true;
		Debug.Log("On");
		col.attachedRigidbody.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }
		
    void OnTriggerExit2D(Collider2D col)
    {
        stepped = false;
        released = true;
        Debug.Log("Off");
    }
}
