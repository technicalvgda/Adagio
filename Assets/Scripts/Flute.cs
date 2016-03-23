using UnityEngine;
using System.Collections;
using System;

public class Flute : MonoBehaviour {
	public Color fluteCol;
	public FluteCover fluteCvr;
    public FluteController control;
    public bool state; 
	private SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		state = false;
		sr = this.GetComponent<SpriteRenderer>();
	}
	void Update () {
		//Depending on the state of our cover button, our color will change.
        //Button is pressed and our progress array is not at the max size OR our progress array contains the button we are pushing
        //This will only allow for 3 flutes to be active at any given time.
		if(fluteCvr.pressed && (control.wipSolution.Count < 3 || control.wipSolution.Contains(this))){
			state = true;
			sr.color = fluteCol;
		} else if(fluteCvr.pressed && !control.wipSolution.Contains(this)){
            //flashes a color specified by holdcolor when you try to pick an extra color.
            Debug.Log("TOO MANY");
            //unpress it
            fluteCvr.pressed = false;
            StartCoroutine(holdColor());
        } else {
            state = false;
            sr.color = Color.black;
        }
	}
    //This is to allow our color to be flashable for 1 second, not sure if it works properly
    private IEnumerator holdColor()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(1f);
    }

    public void setFalse(){
		state = false;
		Debug.Log("FALSED");
	}
}
