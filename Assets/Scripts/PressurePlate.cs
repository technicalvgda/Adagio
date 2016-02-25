using UnityEngine;
using System.Collections;

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