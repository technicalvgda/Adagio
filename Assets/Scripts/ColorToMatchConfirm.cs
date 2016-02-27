using UnityEngine;
using System.Collections;

public class ColorToMatchConfirm : MonoBehaviour {

	public ColorToMatchButton ctmb1, ctmb2, ctmb3;
	bool state = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (ctmb1.getMatch () && ctmb2.getMatch () && ctmb3.getMatch ()) {
			gameObject.transform.position = new Vector2 (gameObject.transform.position.x + 1, gameObject.transform.position.y + 1);
		}
	
	}
}
