using UnityEngine;
using System.Collections;

public class FlashPlayBlock : MonoBehaviour {

	GameObject pressurePlate;
	private SpriteRenderer sr;
	public float time = 3;
	public float elapsedTime = 0;

	bool keyTime ()
	{
		//Wait until E key is pushed.
		//Once pushed, start counting for how long
		elapsedTime = Input.GetKeyDown (KeyCode.E) ? 0 : elapsedTime;
		if (pressurePlate.GetComponent<PressurePlateFlash>().stepped == true && Input.GetKey (KeyCode.E)) {
			elapsedTime += Time.deltaTime;
			Debug.Log (elapsedTime);
		}
		return true;
		//Return true if pushed for +- 0.1 seconds around time
		//Return false otherwise.

	
	}
}
