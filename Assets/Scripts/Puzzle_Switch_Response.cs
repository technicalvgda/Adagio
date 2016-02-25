using UnityEngine;
using System.Collections;

public class Puzzle_Switch_Response : MonoBehaviour 
{


	// Use this for initialization
	void Start () 
	{
		gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void changeColor(int s)
	{
		if (s == 1) 
		{
			GetComponent<SpriteRenderer> ().color = Color.green;
		} 
		else if (s == 2) 
		{
			GetComponent<SpriteRenderer> ().color = Color.red;
		} 

		else
		{
			GetComponent<SpriteRenderer> ().color = Color.blue;
		}


	}
}
