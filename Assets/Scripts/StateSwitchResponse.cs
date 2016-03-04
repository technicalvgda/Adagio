using UnityEngine;
using System.Collections;

public class StateSwitchResponse : MonoBehaviour {
	SpriteRenderer sr;


    void Awake()
    {
        // Get a reference to the SpriteRenderer so that we can change the button's color.
        sr = this.GetComponent<SpriteRenderer>();
        gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Switch(int state)
	{
		switch (state)
		{
		case 0:
			sr.color = new Color(1, 0.3f, 0.3f);

			break;
		case 1:
			sr.color = new Color(0.3f, 1, 0.3f);
			break;
		case 2:
			sr.color = new Color(0.3f, 0.3f, 1);
			break;
		default:
			break;
		}
	}

}
