using UnityEngine;
using System.Collections;



public class Puzzle_Switch : MonoBehaviour 
{

	public Puzzle_Switch_Response Puzzle;
	public mguffin_behavior mguffin;
	GameObject player;
	int state,desiredState;
	bool playerContact = false;


	// Use this for initialization
	void Start () 
	{
		state = 1;
		desiredState = 3;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Puzzle.changeColor(state);

		if(playerContact == true && Input.GetKeyDown(KeyCode.E))
		{
			if (state == 3) 
			{
				state = 0;
			}

			state++;

			Puzzle.changeColor(state);

			if (state == desiredState)
			{
				mguffin.Activate ();
			}


		}

	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player" ) 
		{
			playerContact = true;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Player") 
		{
			playerContact = false;
		}
	}
}
