using UnityEngine;
using System.Collections;

public class item_collision_script : MonoBehaviour {
	
	private float itemCounter = 0f;

	void Start () {

	}
	

	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.name == "item") {
			itemCounter += 1;
			Destroy(col.gameObject);
		}
	}
}