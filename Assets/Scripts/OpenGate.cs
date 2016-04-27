using UnityEngine;
using System.Collections;

public class OpenGate : MonoBehaviour {
	public GameObject gate;
	public int doneCounter = 0;
	// Use this for initialization
	void Start () {
		gate.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gate == null)
			gate = GameObject.Find ("Gate(Clone)");
		else if(doneCounter == 2) 
		{
			gate.SetActive (false);
			doneCounter = 0;
		}
	}
}
