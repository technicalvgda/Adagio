using UnityEngine;
using System.Collections;

public class Flute : MonoBehaviour {
	public Color fluteCol;
	public FluteCover fluteCvr;
	public bool state; 
	int toMatch;
	private SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		state = false;
		sr = this.GetComponent<SpriteRenderer>();
	}
	void Update () {
		//Depending on the state of our cover button, our color will change.
		if(fluteCvr.pressed){
			state = true;
			sr.color = fluteCol;
		} else {
			state = false;
			sr.color = Color.black;
		}
	}
	public void setFalse(){
		state = false;
		Debug.Log("FALSED");
	}
}
