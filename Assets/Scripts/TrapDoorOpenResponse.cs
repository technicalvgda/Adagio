using UnityEngine;
using System.Collections;
/* Basic script to recieve a button press. 
 * Is attached to the button in the button's inspection window.
 */

public class TrapDoorOpenResponse : MonoBehaviour {
	
	public void Deactivate(){
		gameObject.SetActive (false);
	}
}	
