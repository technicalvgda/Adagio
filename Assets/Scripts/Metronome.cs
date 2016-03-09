using UnityEngine;
using System.Collections;

public class Metronome : MonoBehaviour {
    bool on;
    public GameObject plate;
    int timing;
    SpriteRenderer sr;

	void Start ()
    {
        sr = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(plate.GetComponent<PressurePlateFlash>().stepped)
        {
            on = true;
        }
        if(on)
        {
            this.Blink();
            timing = (int)Time.time;
        }
	}

    void Blink()
    {
        if (timing % 2 == 0)
        {
            sr.color = new Color(1, 1, 1);
        }
        else
        {
            sr.color = new Color(0, 0, 0);
        }
    }
}
