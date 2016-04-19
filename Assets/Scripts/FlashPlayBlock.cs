using UnityEngine;
using System.Collections;

public class FlashPlayBlock : MonoBehaviour {

	public GameObject pressurePlate;
	private SpriteRenderer sr;
	public float time = 3;
	float min, max;
	float elapsedTime = 0;
	bool finish;
	void Start()
	{
		sr = this.GetComponent<SpriteRenderer> ();
		min = time - 0.1f;
		max = time + 0.1f;
		finish = false;
	}
	void Update()
	{
		elapsedTime = Input.GetKeyDown (KeyCode.E) ? 0 : elapsedTime;
		if (Input.GetKey (KeyCode.E)) {
			elapsedTime += Time.deltaTime;
			Debug.Log (elapsedTime);
		}
	}
	void restart()
	{
		sr.color = Color.black;
	}



	/*
    void keyTime ()
    {
		while (elapsedTime < time && !finish) 
        {
			if (Input.GetKey (KeyCode.E)) {
				elapsedTime += Time.deltaTime;
			}
			if (Input.GetKeyDown (KeyCode.E)) {
				elapsedTime = 0;
				finish = true;
			}
			Debug.Log (elapsedTime);
        }

    }
    */
    
	public float getElapsedTime()
	{
		return elapsedTime;
	}
	public float getMin()
	{
		return min;
	}
	public float getMax()
	{
		return max;
	}
}

