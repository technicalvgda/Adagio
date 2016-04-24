using UnityEngine;
using System.Collections;

public class AutoMove : MonoBehaviour {

	public float offsetX;
	public float offsetY;
	public float delay = 5f;
	public float speed = .1f;

	private float clock = 0f;

	private Vector3 originPosition;
	private Vector3 destinationPosition;
	private bool atDestination = false;


	void Start () {
		originPosition = transform.position;
		destinationPosition.x = originPosition.x + offsetX;
		destinationPosition.y = originPosition.y + offsetY;
	}

	void FixedUpdate() {
		if (transform.position == destinationPosition && !atDestination) {
			atDestination = true;
			clock = 0f;
		}
		if (transform.position == originPosition && atDestination) {
			atDestination = false;
			clock = 0f;
		}

		if (atDestination) {
			if (clock >= delay) {
				transform.position = Vector3.MoveTowards (transform.position, originPosition, speed);
			} else {
				clock += Time.deltaTime;
			}

		} else if (!atDestination) {
			if (clock >= delay) {
				transform.position = Vector3.MoveTowards (transform.position, destinationPosition, speed);
			} else {
				clock += Time.deltaTime;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.transform.tag == "Player") {
			col.transform.parent = this.transform;

		}
	}

	void OnCollisionExit2D(Collision2D col) {
		if (col.transform.tag == "Player") {
			col.transform.parent = null;
		}
	}
		

}
