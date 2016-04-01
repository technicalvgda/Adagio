using UnityEngine;
using System.Collections;

public class ForegroundMovement : MonoBehaviour
{

	float maxX = 200f;
	float minX = 0f;
	float foregroundHeight = 100f;
	public GameObject player;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (player == null)
		{
			player = GameObject.Find("Player(Clone)");
		}
		else
		{
			if (player.GetComponent<PlayerController> ().moveHorizontal > -0.875 && player.GetComponent<PlayerController> ().moveHorizontal < 0.875) 
			{
				//Do nothing
			}
			else if (player.transform.position.x > 0 && player.transform.position.x < 200) 
			{
				float playerValue = player.transform.position.x / 200;

				transform.position = Vector2.Lerp (new Vector2 (maxX, foregroundHeight), new Vector2 (minX, foregroundHeight), playerValue / 5);
			}

		}
	}
}
