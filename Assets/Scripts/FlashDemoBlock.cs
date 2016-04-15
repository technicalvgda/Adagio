using UnityEngine;
using System.Collections;

public class FlashDemoBlock : MonoBehaviour {
	private SpriteRenderer sr;
	public float time;

	void Start()
	{
		sr = this.GetComponent<SpriteRenderer> ();
	}

	public void beginDemo()
	{
		StartCoroutine(begin());
	}
	IEnumerator begin()
	{
		sr.color = Color.white;
		//Controls how long the flash stays on
		yield return new WaitForSeconds(time);
		sr.color = Color.black;
	}
}
