using UnityEngine;
using System.Collections;

public class FlashingButton : MonoBehaviour {
    SpriteRenderer sr;
    public GameObject chain;
	public GameObject chain2;
	public GameObject plate;

    public bool complete;
    public bool next;
	public float duration;

    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

	void Update()
    {
		if (plate.GetComponent<FlashPanButton> ().puzzleOver == false) {
			if (chain == null) {
				if (plate.GetComponent<PressurePlateFlash> ().stepped == true && Input.GetKey (KeyCode.E) && !complete) {
					sr.color = new Color (0, 0, 1);
				} else if (Input.GetKeyUp (KeyCode.E)) {
					sr.color = new Color (0, 0, 0);
					if ((plate.GetComponent<PressurePlateFlash> ().elapsedTime > duration - .1f && plate.GetComponent<PressurePlateFlash> ().elapsedTime < duration + .1f)) {
						complete = true;
						next = false;
					} else {
						plate.GetComponent<FlashPanButton> ().puzzleOver = true;
					}
				} 

			} else {
				if (chain.GetComponent<FlashingButton> ().complete && plate.GetComponent<PressurePlateFlash> ().stepped && Input.GetKey (KeyCode.E) && !complete) {
					sr.color = new Color (1, 0, 0);
	
					if (plate.GetComponent<PressurePlateFlash> ().elapsedTime > duration - .1f && plate.GetComponent<PressurePlateFlash> ().elapsedTime < duration + .1f) {
						next = true;
					}

				} else if (Input.GetKeyUp (KeyCode.E) && next && chain.GetComponent<FlashingButton>().complete) {
					sr.color = new Color (0, 0, 0);
					if (next == false && complete == true) {
						complete = false;
					}
					if (chain.GetComponent<FlashingButton> ().complete  && plate.GetComponent<PressurePlateFlash> ().elapsedTime > duration - .1f && plate.GetComponent<PressurePlateFlash> ().elapsedTime < duration + .1f) {
						complete = true;
					} 
					else
					{
						chain.GetComponent<FlashingButton> ().complete = false;
						chain.GetComponent<FlashingButton> ().next = false;
						Debug.Log ("Hello" + plate.GetComponent<PressurePlateFlash> ().elapsedTime);
						sr.color = new Color (0, 0, 0);
						next = false;
						plate.GetComponent<FlashPanButton> ().puzzleOver = true;
					}

				}



			}
		}

    }
    

}
