using UnityEngine;
using System.Collections;

public class FlashPanButton : MonoBehaviour
{

	//Taken from the older panning class.
	//Added the puzzleOver boolean that acts in the panwait stage.
	//If the game ends, the boolean is true and the camera pans back.

	public enum state
	{
		pantoward,
		panback,
		panwait,
		notpanning
	}
	public float panTime = 0.75f;
	public float stayTime = 2f;
	public bool puzzleOver;
	public GameObject target;
	state panState = state.notpanning;
	Transform player;
	Vector3 camPos;
	Vector3 currentVelocity = Vector3.zero;

	void OnTriggerEnter2D(Collider2D c)
	{
			if (panState == state.notpanning)
			{
				if (target != null)
				{
				puzzleOver = false;
					panState = state.pantoward;
					player = c.transform;
					camPos = Camera.main.transform.position;
					player.GetComponent<PlayerController>().enabled = false;
					Camera.main.GetComponent<CameraController>().enabled = false;
				}
			}
	}
	void LateUpdate()
	{
		if (target != null)
		{
			if (panState == state.pantoward) {
				player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				camPos = Vector3.SmoothDamp (camPos, new Vector3 (target.transform.position.x, target.transform.position.y, camPos.z), ref currentVelocity, panTime);
				Camera.main.transform.position = camPos;
				if (Vector2.Distance (camPos, target.transform.position) < 0.1f) {
					panState = state.panwait;
				}
			} 
			if (panState == state.panwait) 
			{
				
				if (puzzleOver)
					Invoke ("startPanBack", stayTime);
			}
			if (panState == state.panback)
			{
				camPos = Vector3.SmoothDamp(camPos, new Vector3(player.position.x, player.position.y, camPos.z), ref currentVelocity, panTime);
				Camera.main.transform.position = camPos;
				if (Vector2.Distance(camPos, player.position) < 0.1f)
				{
					Camera.main.GetComponent<CameraController>().enabled = true;
					panState = state.notpanning;
				}
			}
		}
	}
	void startPanBack()
	{
		player.GetComponent<PlayerController>().enabled = true;
		panState = state.panback;
	}
}
