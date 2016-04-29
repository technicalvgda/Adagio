using UnityEngine;
using System.Collections;

public class ScriptedEventTrigger : MonoBehaviour {
	public bool startedScriptedEvent = false;
	public int levelActivated = 0;
	public int currentLevel = 1;
	// Use this for initialization
	void Start () 
	{
		startedScriptedEvent = false;
		currentLevel = 1;
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
			if (levelActivated == currentLevel) 
			{
				startedScriptedEvent = true;
			}

		}
	}
	void OnDisable()
	{
		Teleporter.OnTeleport -= IncreaseLevels;
	}
	void OnEnable()
	{
		Teleporter.OnTeleport += IncreaseLevels;
	}
	void IncreaseLevels()
	{
		currentLevel++;
	}

	//void OnTriggerExit2D(Collider2D col)
	//{
	//	if (col.tag == "Player")
	//	{
	//		finishedScriptedEvent = false;
	//	}
	//}
}
