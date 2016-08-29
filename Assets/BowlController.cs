using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BowlController : MonoBehaviour {

    SpriteRenderer sr, ss;
    public GameObject SimonSaysSwitch;
    public GameObject sb1, sb2, sb3, sb4, sb5, sb6;
    List<int> sequence = new List<int>();
    List<int> bpressed = new List<int>();
    public GameObject door;
    bool b1 = false, b2 = false, b3 = false; //b4 = false;

   //int state = 0;
    //int rnd = 0;

    void Start()
    {
        // Get a reference to the SpriteRenderer so that we can change the button's color.
        gameObject.SetActive(true);
        sequence.Add(2);
        sequence.Add(1);
        sequence.Add(0);

    }

    // Update is called once per frame
    void Update()
    {
        if (SimonSaysSwitch.GetComponent<BowlsSwitch>().pressed == true)
        {
            bpressed.Clear();
            //sequence.Clear();
            SimonSaysSwitch.GetComponent<BowlsSwitch>().pressed = false;

        }

        if (sb1.GetComponent<BowlsButton>().pressed == true && sb2.GetComponent<BowlsButton>().pressed == false &&
                sb3.GetComponent<BowlsButton>().pressed == false && sb4.GetComponent<BowlsButton>().pressed == false &&
           sb5.GetComponent<BowlsButton>().pressed == false && sb6.GetComponent<BowlsButton>().pressed == false)
        {
            Debug.Log("BLACK1");
            sb1.GetComponent<BowlsButton>().pressed = false;
            bpressed.Add(2);
            Debug.Log(bpressed[0]);
        }

        if (sb1.GetComponent<BowlsButton>().pressed == false && sb2.GetComponent<BowlsButton>().pressed == true &&
           sb3.GetComponent<BowlsButton>().pressed == false && sb4.GetComponent<BowlsButton>().pressed == false &&
           sb5.GetComponent<BowlsButton>().pressed == false && sb6.GetComponent<BowlsButton>().pressed == false)
        {
            Debug.Log("BLACK2");
            bpressed.Add(2);
            sb2.GetComponent<BowlsButton>().pressed = false;
        }

        if (sb1.GetComponent<BowlsButton>().pressed == false && sb2.GetComponent<BowlsButton>().pressed == false &&
           sb3.GetComponent<BowlsButton>().pressed == true && sb4.GetComponent<BowlsButton>().pressed == false &&
           sb5.GetComponent<BowlsButton>().pressed == false && sb6.GetComponent<BowlsButton>().pressed == false)
        {
            Debug.Log("GREEN1");
            bpressed.Add(1);
            sb3.GetComponent<BowlsButton>().pressed = false;
        }

        if (sb1.GetComponent<BowlsButton>().pressed == false && sb2.GetComponent<BowlsButton>().pressed == false &&
           sb3.GetComponent<BowlsButton>().pressed == false && sb4.GetComponent<BowlsButton>().pressed == true &&
           sb5.GetComponent<BowlsButton>().pressed == false && sb6.GetComponent<BowlsButton>().pressed == false)
        {
            Debug.Log("GREEN2");
            bpressed.Add(1);
            sb4.GetComponent<BowlsButton>().pressed = false;
        }

        if (sb1.GetComponent<BowlsButton>().pressed == false && sb2.GetComponent<BowlsButton>().pressed == false &&
           sb3.GetComponent<BowlsButton>().pressed == false && sb4.GetComponent<BowlsButton>().pressed == false &&
           sb5.GetComponent<BowlsButton>().pressed == true && sb6.GetComponent<BowlsButton>().pressed == false)
        {
            Debug.Log("BLUE1");
            bpressed.Add(0);
            sb5.GetComponent<BowlsButton>().pressed = false;
        }

        if (sb1.GetComponent<BowlsButton>().pressed == false && sb2.GetComponent<BowlsButton>().pressed == false &&
          sb3.GetComponent<BowlsButton>().pressed == false && sb4.GetComponent<BowlsButton>().pressed == false &&
          sb5.GetComponent<BowlsButton>().pressed == false && sb6.GetComponent<BowlsButton>().pressed == true)
        {
            Debug.Log("BLUE2");
            bpressed.Add(0);
            sb6.GetComponent<BowlsButton>().pressed = false;
        }

        if (bpressed.Count > 2)
        {
            if (sequence[0] == bpressed[0])
            {
                Debug.Log("sdgsdgdsgdgdsgdgdsgdfggdgdsd");
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
            
            if (b1 == true && b2 == true && b3 == true)
            {
				Camera.main.GetComponent<OpenGate> ().doneCounter++;
                GameObject.Destroy(door);
				this.gameObject.SetActive (false);
            }
        }
        // Debug.Log(bpressed[0] + bpressed[1] + bpressed[2] + bpressed[3]);
    }
}
