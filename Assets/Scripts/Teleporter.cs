using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

    public GameObject playerTeleportPlat;
    private GameObject player;

    public bool isInTeleporter;

    public delegate void TeleportAction ();
	public static event TeleportAction OnTeleport;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E)){
			Teleport();
						}

    }

	void OnTriggerEnter2D(Collider2D col)
    {

            isInTeleporter = true;
		    player = col.gameObject;
        
    }

	   void OnTriggerExit2D(Collider2D col){
		   		isInTeleporter = false;
		   }

	   
	//Subscribe Activate method to the OnTap Event when object becomes active
	void OnEnable(){
		PlayerController.OnTap += Teleport;
	}
	//Unsubscrite Activate method from the OnTap event when object becomes deactive
	void OnDisable(){
		PlayerController.OnTap -= Teleport;
	}
		

	void Teleport()
    {
		if (isInTeleporter)
		{
			if(OnTeleport != null){	
				OnTeleport();
				player.transform.position = playerTeleportPlat.transform.position;
            	Debug.Log("Teleport");
			}
        }
    }
}
