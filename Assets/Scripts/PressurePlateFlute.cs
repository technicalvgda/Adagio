using UnityEngine;
using System.Collections;

public class PressurePlateFlute : MonoBehaviour
{
	public GameObject pressureplate;
	public GameObject mNote;
	public GameObject slider;
	public bool collide = false;
	//GameObject pressureplate1 = GameObject.FindGameObjectWithTag("pressureplate1");
	void Start()
	{


	}
	void OnTriggerEnter2D(Collider2D col)
	{
		pressureplate.transform.position = new Vector2(pressureplate.transform.position.x , pressureplate.transform.position.y );
		Debug.Log("Enter");
		if(slider.transform.position.x > mNote.transform.position.x - 1 && slider.transform.position.x < mNote.transform.position.x + 1)
		{
			collide = true;
			Debug.Log(collide);
		}

	}
	void OnTriggerExit2D(Collider2D col)
	{
		pressureplate.transform.position = new Vector2(pressureplate.transform.position.x , pressureplate.transform.position.y );
		Debug.Log("Exit");
	}
}