using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

    public GameObject playerTeleportPlat;
    private PlayerController player;

    public bool isInTeleporter;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {


    }

    void OnCollisionEnter2D(Collision2D col)
    {

            Teleport();
            isInTeleporter = true;
        
    }

    void Teleport()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            player.transform.position = playerTeleportPlat.transform.position;
            Debug.Log("Teleport");
        }
    }
}
