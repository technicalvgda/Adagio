using UnityEngine;
using System.Collections;

public class ForegroundMovement : MonoBehaviour {

    float maxX = 200f;
    float minX = 0f;
    public GameObject player;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(player == null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            if(player.transform.position.x > 0 && player.transform.position.x <200)
            {
                float playerValue = Mathf.Lerp(0, 1, player.transform.position.x);

                transform.position = Vector2.Lerp(new Vector2(minX, -100), new Vector2(maxX, -100), playerValue);
            }
            
        }
	}
}
