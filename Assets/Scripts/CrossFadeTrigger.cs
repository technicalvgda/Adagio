using UnityEngine;
using System.Collections;

public class CrossFadeTrigger : MonoBehaviour {

    public GameObject audioHandler;
    CrossFadeControl handlerScript;
   
    [Header("Fade Type")]
    [Tooltip("Check the box for cross fading track one and two, uncheck for two and three")]
    public bool OneTwo = true;
	// Use this for initialization
	void Start ()
    {
        if(audioHandler == null)
        {
            Debug.Log("Place an audiohandler prefab into the scene and drag it to this object in the inspector");
        }
        handlerScript = audioHandler.GetComponent<CrossFadeControl>();
	}
	
	void OnTriggerEnter2D(Collider2D col)
    {
        //if the player collides with this object
        if(col.tag == "Player")
        {
            if (OneTwo)
            {
                //activate cross fade in audio handler
                handlerScript.CrossFadeOneTwo();
            }
            else
            {
                handlerScript.CrossFadeTwoThree();
            }
        }
    }
}
