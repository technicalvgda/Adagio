using UnityEngine;
using System.Collections;

public class Metronome : MonoBehaviour {
    bool on;
    public GameObject plate;
    float timing;
    SpriteRenderer sr;

	void Start ()
    {
        sr = this.GetComponent<SpriteRenderer>();
	}

	void beginBlink()
	{
		StartCoroutine (Blink());
	}

    IEnumerator Blink()
    {
		while (!plate.GetComponent<FlashPanButton> ().puzzleOver)
		{
			sr.color = Color.white;
			yield return new WaitForSeconds(1);
			sr.color = Color.black;
			yield return new WaitForSeconds (1);
		}
        
    }
}
