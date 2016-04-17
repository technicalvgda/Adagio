using UnityEngine;
using System.Collections;

public class FlashPlayBlock : MonoBehaviour {

	GameObject pressurePlate;
	private SpriteRenderer sr;
	public float time = 3;
	float min, max;
	float elapsedTime = 0;
	void Start()
	{
		min = time - 0.1f;
		max = time + 0.1f;
	}
	/*
    void keyTime ()
    {
        while (!Input.GetButtonUp (KeyCode.E)) 
        {
                elapsedTime += Time.deltaTime;
                Debug.Log (elapsedTime);
        }
        //Return true if pushed for +- 0.1 seconds around time
        //Return false otherwise.

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

