using UnityEngine;
using System.Collections;

public class FlashDemoBlock : MonoBehaviour {
	private SpriteRenderer sr;
	public float time = 5;
	public float elapsedTime = 0;

	void begin () 
	{
		sr = this.GetComponent<SpriteRenderer> ();
		Debug.Log ("hit");
		while (elapsedTime < time) 
		{
			sr.color = new Color(1f, 0f, 0f, 1f);
			Debug.Log (elapsedTime);
			elapsedTime += Time.deltaTime;
		}
		sr.color = new Color(1f, 1f, 1f, 1f);

	}

	void off()
	{
	}
}
