using UnityEngine;
using System.Collections;

public class ThreeDrumControl : MonoBehaviour {
    public GameObject pp1, pp2, pp3, ring, door;
    public int drumNum = 1;
    

    // Use this for initialization
    void Start () {
        pp1.GetComponent<Renderer>().enabled = false;
        pp2.GetComponent<Renderer>().enabled = false;
        pp3.GetComponent<Renderer>().enabled = false;

        pp1.SetActive(false);
        pp2.SetActive(false);
        pp3.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (ring.GetComponent<FiveStringButton>().pressed && drumNum == 1)
        {
            pp1.GetComponent<Renderer>().enabled = true;
            pp1.SetActive(true);

            ring.GetComponent<FiveStringButton>().enabled = false;
            ring.SetActive(false);

            
        }

        if (ring.GetComponent<FiveStringButton>().pressed && drumNum == 2)
        {
            pp2.GetComponent<Renderer>().enabled = true;
            pp2.SetActive(true);

            ring.GetComponent<FiveStringButton>().enabled = false;
            ring.SetActive(false);


        }

        if (ring.GetComponent<FiveStringButton>().pressed && drumNum == 3)
        {
            pp3.GetComponent<Renderer>().enabled = true;
            pp3.SetActive(true);

            
        }

        ring.GetComponent<FiveStringButton>().pressed = false;

        if (pp1.GetComponent<RedDrum>().pressed) {
            ring.GetComponent<FiveStringButton>().enabled = true;
            ring.SetActive(true);

            drumNum = 2;
        }

        if (pp2.GetComponent<RedDrum>().pressed)
        {
            ring.GetComponent<FiveStringButton>().enabled = true;
            ring.SetActive(true);

            drumNum = 3;
        }


        if (pp3 .GetComponent<RedDrum>().pressed)
        {
            GameObject.Destroy(door);
        }
       
    }

    
}
