using UnityEngine;
using System.Collections;
/* Using the Basic Switch, create a new switch that when activated, depending on the number of times the player presses it,
 * will cycle through a specific number of states in an order. For now just have the switch print "Red", "Green", 
 * or "Blue" for the states. Once "Blue" has been printed, it should go to "Red" or "Green" after the player activates it a fourth time, 
 * depending on the order you set up. 
*/
public class StateSwitch : MonoBehaviour {
    private int currentState;

	// Use this for initialization
	void Start () {
        currentState = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    //Increments the current state when called and cycles. Behaves like ButtonResponse.
    public void Activate()
    {
        currentState++;

        if (currentState == 0){
            //Insert code here, activates only on the moment of the switch
            print("Red");
        }
        else if(currentState == 1) {
            print("Green");
        }
        else if(currentState == 2) {
            print("Blue");
            currentState = -1;
        }
            
    }
}
