using UnityEngine;
using System.Collections;

public class ScriptedEventTrigger : MonoBehaviour {
	public bool startedScriptedEvent = false;
	// Use this for initialization
	void Start () 
	{
		startedScriptedEvent = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/*
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			finishedScriptedEvent = true;
		}
	}*/

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			startedScriptedEvent = true;
		}
	}

	//void OnTriggerExit2D(Collider2D col)
	//{
	//	if (col.tag == "Player")
	//	{
	//		finishedScriptedEvent = false;
	//	}
	//}
}
