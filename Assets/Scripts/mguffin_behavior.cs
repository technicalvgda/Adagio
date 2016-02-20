using UnityEngine;
using System.Collections;

public class mguffin_behavior : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
			gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void Activate()
	{
		gameObject.SetActive (true);
	}
}
