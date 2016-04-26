using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GongController : MonoBehaviour {

    SpriteRenderer sr, ss;
    public GameObject SimonSaysSwitch;
    public GameObject sb1, sb2, sb3, sb4, sb5, sb6, sb7, sb8, sb9;
    List<int> sequence = new List<int>();
    List<int> bpressed = new List<int>();
    public GameObject door;
    bool b1 = false, b2 = false, b3 = false, b4 = false;

    int state = 0;
    int rnd = 0;

    void Start()
    {
        // Get a reference to the SpriteRenderer so that we can change the button's color.
        gameObject.SetActive(true);
        sequence.Add(3);
        sequence.Add(2);
        sequence.Add(1);
        sequence.Add(0);

    }

    // Update is called once per frame
    void Update()
    {
        if (SimonSaysSwitch.GetComponent<GongSwitch>().pressed == true)
        {
            bpressed.Clear();
            //sequence.Clear();
            SimonSaysSwitch.GetComponent<GongSwitch>().pressed = false;

        }
        if (sb1.GetComponent<GongButton>().pressed == true && sb2.GetComponent<GongButton>().pressed == false &&
                sb3.GetComponent<GongButton>().pressed == false && sb4.GetComponent<GongButton>().pressed == false)
        {
            Debug.Log("aaaaaaaaaaaaaa");
            sb1.GetComponent<GongButton>().pressed = false;
            bpressed.Add(3);
            Debug.Log(bpressed[0]);
        }
        if (sb1.GetComponent<GongButton>().pressed == false && sb2.GetComponent<GongButton>().pressed == true &&
           sb3.GetComponent<GongButton>().pressed == false && sb4.GetComponent<GongButton>().pressed == false)
        {
            bpressed.Add(2);
            sb2.GetComponent<GongButton>().pressed = false;
        }
        if (sb1.GetComponent<GongButton>().pressed == false && sb2.GetComponent<GongButton>().pressed == false &&
           sb3.GetComponent<GongButton>().pressed == true && sb4.GetComponent<GongButton>().pressed == false)
        {
            bpressed.Add(1);
            sb3.GetComponent<GongButton>().pressed = false;
        }

        if (sb1.GetComponent<GongButton>().pressed == false && sb2.GetComponent<GongButton>().pressed == false &&
           sb3.GetComponent<GongButton>().pressed == false && sb4.GetComponent<GongButton>().pressed == true)
        {
            bpressed.Add(0);
            sb4.GetComponent<GongButton>().pressed = false;
        }
        if (bpressed.Count > 3)
        {
            if (sequence[0] == bpressed[0])
            {
                Debug.Log("bbbbbbbbbbbbbbbb");
                b1 = true;
            }
            if (sequence[1] == bpressed[1])
            {
                b2 = true;
            }
            if (sequence[2] == bpressed[2])
            {
                b3 = true;
            }
            if (sequence[3] == bpressed[3])
            {
                b4 = true;
            }
            if (b1 == true && b2 == true && b3 == true && b4 == true)
            {
                GameObject.Destroy(door);
				Camera.main.GetComponent<OpenGate> ().doneCounter++;
				this.gameObject.SetActive (false);
            }
        }
      // Debug.Log(bpressed[0] + bpressed[1] + bpressed[2] + bpressed[3]);
    }

}

