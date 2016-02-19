using UnityEngine;
using System.Collections;

/*
 * Creates a trigger space that the player can walk into.
 * The receiver attatched to the pressureplate will move when triggered and
 * move back when the trigger is exited.
 */
public class PressurePlate : MonoBehaviour {

	public GameObject receiver;

	void OnTriggerEnter2D(Collider2D col)
	{
		receiver.transform.position = new Vector2 (receiver.transform.position.x + 1, receiver.transform.position.y + 1);
	}
	void OnTriggerExit2D(Collider2D col)
	{
		receiver.transform.position = new Vector2 (receiver.transform.position.x - 1, receiver.transform.position.y - 1);
	}
}
