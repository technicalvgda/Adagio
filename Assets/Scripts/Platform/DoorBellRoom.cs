using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorBellRoom : MonoBehaviour {

	private List<string> blockList;
	private Transform platform;
	//After the player hits the platform, I want to be able to detect
	//which platform was hit.

	//Save the name of the platform that triggered the bar.
	//Put that in a list of blocks
	//After the player hits the last block, see if the order of the list
	//is == to the order of the givenList
	//1. If they are = then the door is destroyed.
	//2. else the room resets.


	void Awake() {
		//refer to the door
		//this.gameObject.SetActive (true);


	
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	enum Block { Blue, Purple, Red, Orange, Yellow };
}
