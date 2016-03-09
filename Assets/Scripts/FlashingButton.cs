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

	// Update is called once per frame
	void Update()
    {
        if (!chain)
        {
            if (plate.GetComponent<PressurePlateFlash>().stepped == true && !complete)
            {
                sr.color = new Color(3, 5, 1);
            }
            else if (plate.GetComponent<PressurePlateFlash>().released == true)
            {
                sr.color = new Color(0, 0, 0);
                complete = true;
                next = false;
            }
        }

        else
        {
            if (chain.GetComponent<FlashingButton>().complete == true && plate.GetComponent<PressurePlateFlash>().stepped == true && !complete)
            {
                sr.color = new Color(3, 5, 1);
                next = true;
            }
            else if (chain.GetComponent<FlashingButton>().complete && plate.GetComponent<PressurePlateFlash>().released && next)
            {
                sr.color = new Color(0, 0, 0);
                complete = true;
            }
        }
    }

}
