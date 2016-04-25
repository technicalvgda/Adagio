using UnityEngine;
using System.Collections;

public class FadeAway : MonoBehaviour {
	private SpriteRenderer rend;
	private ParticleSystem jumpParticle;
	private Color fadeCol;
	private Color startCol;
	public float fadeTime;
	private float t = 0;
	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer>();
		jumpParticle = GetComponent<ParticleSystem>();
		startCol = rend.material.color;
		fadeCol = new Color(startCol.r, startCol.g, startCol.b, 0);
		jumpParticle.Emit(6);
	}
	
	// Update is called once per frame
	void Update () {
		//fades the object over fadeTime seconds
		if(t < fadeTime){
			t += Time.deltaTime;
			rend.material.color = Color.Lerp(startCol, fadeCol, t/fadeTime);
			Debug.Log(rend.material.color.a);
			if(t > fadeTime){
				Destroy(this.gameObject);
			}
		} 
	}
}
