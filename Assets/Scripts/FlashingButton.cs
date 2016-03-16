using UnityEngine;
using System.Collections;

public class FlashingButton : MonoBehaviour {
    SpriteRenderer sr;
    public GameObject chain;
    public GameObject plate;
    public bool complete;
    public bool next;

    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        complete = false;
    }
	/*
	public void Flash()
	{
		sr.color = new Color(0, 0, 1);
	}
	public void NoFlash()
	{
		sr.color = new Color (0, 0, 0);
	}
	*/


	void Update()
    {
        if (!chain)
        {
			if (plate.GetComponent<PressurePlateFlash>().stepped == true && Input.GetKey (KeyCode.E) && !complete)
            {
                sr.color = new Color(0, 0, 1);
            }
			else if (Input.GetKeyUp (KeyCode.E))
            {
                sr.color = new Color(0, 0, 0);
                complete = true;
                next = false;
            }
        }
        else
        {
			if (chain.GetComponent<FlashingButton>().complete == true && plate.GetComponent<PressurePlateFlash>().stepped == true && Input.GetKey (KeyCode.E) && !complete)
            {
                sr.color = new Color(1, 0, 0);
                next = true;
            }
			else if (chain.GetComponent<FlashingButton>().complete && Input.GetKeyUp (KeyCode.E) && next)
            {
                sr.color = new Color(0, 0, 0);
                complete = true;
            }
        }
    }
    

}
