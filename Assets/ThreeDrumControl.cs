using UnityEngine;
using System.Collections;
using System;

public class ThreeDrumControl : MonoBehaviour {
    public GameObject pp1, pp2, pp3, ring, door;
    public int drumNum = 1;
    
    Vector3 start, end;
    public float speed = 0.05f;
    bool drum1, drum2, drum3;// x = true, 
    //int counter = 0;


    // Use this for initialization
    void Start () {
        pp1.GetComponent<Renderer>().enabled = false;
        pp2.GetComponent<Renderer>().enabled = false;
        pp3.GetComponent<Renderer>().enabled = false;

        drum1 = drum2 = drum3 = false;

        pp1.SetActive(false);
        pp2.SetActive(false);
        pp3.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        // Checks if the player hit the ring and displays the correct drum
        if (ring.GetComponent<FiveStringButton>().pressed && drumNum == 1)
        {
            pp1.GetComponent<Renderer>().enabled = true;
            pp1.SetActive(true);

            drum1 = true;

            ring.GetComponent<FiveStringButton>().enabled = false;
            ring.SetActive(false);
        }

        if (ring.GetComponent<FiveStringButton>().pressed && drumNum == 2)
        {
            pp2.GetComponent<Renderer>().enabled = true;
            pp2.SetActive(true);

            drum2 = true;

            ring.GetComponent<FiveStringButton>().enabled = false;
            ring.SetActive(false);
        }

        if (ring.GetComponent<FiveStringButton>().pressed && drumNum == 3)
        {
            pp3.GetComponent<Renderer>().enabled = true;
            pp3.SetActive(true);

            drum3 = true;

            ring.GetComponent<FiveStringButton>().enabled = false;
            ring.SetActive(false);
        }

        // Resets the pressed variable in the Ring script
        ring.GetComponent<FiveStringButton>().pressed = false;


        // Checks to see if the correct drum was hit 

        if (pp1.GetComponent<RedDrum>().pressed && drum1) {
            ring.GetComponent<FiveStringButton>().enabled = true;
            ring.SetActive(true);

            drum1 = false;

            drumNum = 2;
           
        }

        if (pp2.GetComponent<RedDrum>().pressed && drum2)
        {
            ring.GetComponent<FiveStringButton>().enabled = true;
            ring.SetActive(true);

            drum2 = false;

            drumNum = 3;
        }


        if (pp3 .GetComponent<RedDrum>().pressed && drum3)
        {
            drum3 = false;

            GameObject.Destroy(door);
        }
       
    }

  
}
