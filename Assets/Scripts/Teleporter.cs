using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

    public GameObject playerTeleportPlat;
    private GameObject player;
    public bool isInTeleporter;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
			if(isInTeleporter && Input.GetKeyDown(KeyCode.E)){
				Teleport(player);
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

    void Teleport(GameObject player)
    {
        	player.transform.position = playerTeleportPlat.transform.position;
        	Debug.Log("Teleport");
    }
}
