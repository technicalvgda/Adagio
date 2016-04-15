using UnityEngine;
using System.Collections;

public class FlashDemoBlock : MonoBehaviour {
	private SpriteRenderer sr;
	public float time;

	public void beginDemo()
	{
		StartCoroutine(begin());
	}
	IEnumerator begin()
	{
		sr = this.GetComponent<SpriteRenderer> ();
		Debug.Log ("Enter");
		sr.color = Color.white;
		//Controls how long the flash stays on
		yield return new WaitForSeconds(time);
		sr.color = Color.black;
	}
}
