using UnityEngine;
using System.Collections;

public class FIveStringPressurePlate : MonoBehaviour {
    public GameObject pp1, pp2, pp3, pp4, pp5, door;

	// Use this for initialization
	void Start () {
       
        pp1.GetComponent<Renderer>().enabled = true;
        pp2.GetComponent<Renderer>().enabled = false;
        pp3.GetComponent<Renderer>().enabled = false;
        pp4.GetComponent<Renderer>().enabled = false;
        pp5.GetComponent<Renderer>().enabled = false;



        pp2.SetActive(false);
        pp3.SetActive(false);
        pp4.SetActive(false);
        pp5.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (pp1.GetComponent<FiveStringButton>().pressed) {
            pp1.GetComponent<Renderer>().enabled = false;
            pp1.SetActive(false);

            pp2.GetComponent<Renderer>().enabled = true;
            pp2.SetActive(true);
        }

        if (pp2.GetComponent<FiveStringButton>().pressed)
        {
            pp2.GetComponent<Renderer>().enabled = false;
            pp2.SetActive(false);

            pp3.GetComponent<Renderer>().enabled = true;
            pp3.SetActive(true);
        }

        if (pp3.GetComponent<FiveStringButton>().pressed)
        {
            pp3.GetComponent<Renderer>().enabled = false;
            pp3.SetActive(false);

            pp4.GetComponent<Renderer>().enabled = true;
            pp4.SetActive(true);
        }

        if (pp4.GetComponent<FiveStringButton>().pressed)
        {
            pp4.GetComponent<Renderer>().enabled = false;
            pp4.SetActive(false);
      
            pp5.GetComponent<Renderer>().enabled = true;
            pp5.SetActive(true);
        }

        if (pp5.GetComponent<FiveStringButton>().pressed)
        {
            pp5.GetComponent<Renderer>().enabled = false;
            pp5.SetActive(false);
            GameObject.Destroy(door);
        }
    }

  
}
