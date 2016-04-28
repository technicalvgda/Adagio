using UnityEngine;
using System.Collections;

public class OpenGate : MonoBehaviour {
	public GameObject gate;
	public GameObject gate2;
	public int doneCounter = 0;
	// Use this for initialization
	void Start () {
		
		//gate.SetActive (true);
		//gate2.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gate == null) {
			gate = GameObject.Find ("Gate(Clone)");
			//gate.SetActive (true);
		}
		if (gate2 == null) {
			gate2 = GameObject.Find ("Gate2(Clone)");
			//gate2.SetActive (true);
		}
		if(doneCounter == 1) 
			gate.SetActive (false);
		
		if (doneCounter == 2) 
		{
			gate2.SetActive (false);
		}
	}
}
