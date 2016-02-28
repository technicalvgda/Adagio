using UnityEngine;
using System.Collections;

public class ColorToMatchConfirm : MonoBehaviour {

	public ColorToMatchButton ctmb1, ctmb2, ctmb3;
	// Use this for initialization
	int moveDistance = 15; //how far we move
	Vector3 homeposition; //where we start
	Vector3 movedtoposition; //where we move to
	Vector3 returntoposition; //where we move back to
	void Start () {
		homeposition = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y);
		movedtoposition = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y - moveDistance);
		returntoposition = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + moveDistance);
	}

	// Update is called once per frame
	void Update () {
		//all three colors match
		if (ctmb1.getMatch () && ctmb2.getMatch () && ctmb3.getMatch ()) {
			//different options go here
			gameObject.transform.position = movedtoposition;
			//gameObject.SetActive (false);
		//all three colors don't match
		} else {
			//this if/else is only used if you're moving the block
			if (gameObject.transform.position == movedtoposition) {
				gameObject.transform.position = returntoposition;
			} else{
				gameObject.transform.position = homeposition;
			}
			//gameObject.SetActive (true);
		}
	}
}
