using UnityEngine;
using System.Collections;
/* Basic script to recieve a button press. 
 * Is attached to the button in the button's inspection window.
 */

public class ButtonResponse : MonoBehaviour {
	public bool isActive = false;
	// Use this for initialization
	void Start () 
	{
		this.gameObject.SetActive (false);


	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void Activate()
	{
		this.gameObject.SetActive (true);
	}
		


}
