using UnityEngine;
using System.Collections;

public class WalkAndMoveBlocksRingScript : MonoBehaviour 
{
	public WalkAndMoveBlocksPuzzle manager;
	private Animator anim;
	private AudioSource audioSource;
	private bool playOnce;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();
		playOnce = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!playOnce) 
		{
			if (manager.puzzleIsCompleted()) 
			{
				playOnce = true;
				anim.SetTrigger ("collect");
				audioSource.Play ();
			}
		}
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other != null) {
			if (other.gameObject.tag == "Player") {
				manager.playerFinishedPuzzle ();
			}
		}
	}
	public void setRingUnactive()
	{
		gameObject.SetActive (false);
	}
}
