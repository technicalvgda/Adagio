using UnityEngine;
using System.Collections;

public class Metronome : MonoBehaviour {
    bool on; 
    public GameObject BeginButton;
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
		while (!BeginButton.GetComponent<FlashBeginButton> ().puzzleOver)
		{
			sr.color = Color.white;
			yield return new WaitForSeconds(1);
			sr.color = Color.black;
			yield return new WaitForSeconds (1);
		} 
    }
}
