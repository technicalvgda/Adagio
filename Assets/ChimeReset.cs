using UnityEngine;
using System.Collections;

public class ChimeReset : MonoBehaviour {

    public bool stepped;
     //int state = 0;
     public bool pressed;
 
     void Start()
     {
 
         stepped = false;
         pressed = false;
 
     }
 
     void Update()
     {
         // If the player is colliding with the button, advance the state.
         // since Update() executes more frequently and is less likely to cause missed checks.
         if (stepped)
         {
             Debug.Log("ON");
             pressed = true;
         }
     }
 
     // NOTE: Set the Player object's RigidBody2D -> Sleeping Mode to Never Sleep, otherwise
     // the collision check will eventually stop due to the player object going to sleep.
     void OnTriggerStay2D(Collider2D other)
     {
         if (other.gameObject.tag == "Player")
             stepped = true;
     }
 
     void OnTriggerExit2D(Collider2D other)
     {
         if (other.gameObject.tag == "Player")
             stepped = false;
     }
}