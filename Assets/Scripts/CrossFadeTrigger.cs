using UnityEngine;
using System.Collections;

public class CrossFadeTrigger : MonoBehaviour {

    public GameObject audioHandler;
    CrossFadeControl handlerScript;
    public AudioClip clipToPlay;
	// Use this for initialization
	void Start ()
    {
        handlerScript = audioHandler.GetComponent<CrossFadeControl>();
	}
	
	void OnTriggerEnter2D(Collider2D col)
    {
        //if the player collides with this object
        if(col.tag == "Player")
        {
            //activate cross fade in audio handler
            handlerScript.CrossFade(clipToPlay);
        }
    }
}
