using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ChimePuzzle : MonoBehaviour {
    SpriteRenderer sr, ss;
    public GameObject SimonSaysSwitch;
    public GameObject sb1, sb2, sb3, sb4, sb5, sb6, sb7, sb8, sb9, sb10;
    List<int> sequence = new List<int>();
    List<int> bpressed = new List<int>();
    public GameObject door;
    bool b1 = false, b2 = false, b3 = false, b4 = false, b5 = false, b6 = false; //b7 = false, b8 = false, b9 = false, b10 = false;

    //int state = 0;
    //int rnd = 0;


    void Start()
    {
        // Get a reference to the SpriteRenderer so that we can change the button's color.
        gameObject.SetActive(true);
        sequence.Add(5);
        sequence.Add(4);
        sequence.Add(3);
        sequence.Add(2);
        sequence.Add(1);
        sequence.Add(0);

    }

    // Update is called once per frame
    void Update()
    {
        if (SimonSaysSwitch.GetComponent<ChimeReset>().pressed == true)
        {
            bpressed.Clear();
            SimonSaysSwitch.GetComponent<ChimeReset>().pressed = false;

        }

        if (sb1.GetComponent<ChimeButton>().pressed == true && sb2.GetComponent<ChimeButton>().pressed == false &&
                sb3.GetComponent<ChimeButton>().pressed == false && sb4.GetComponent<ChimeButton>().pressed == false &&
                sb5.GetComponent<ChimeButton>().pressed == false && sb6.GetComponent<ChimeButton>().pressed == false &&
                sb7.GetComponent<ChimeButton>().pressed == false && sb8.GetComponent<ChimeButton>().pressed == false &&
                sb9.GetComponent<ChimeButton>().pressed == false && sb10.GetComponent<ChimeButton>().pressed == false)
        {
            sb1.GetComponent<ChimeButton>().pressed = false;
            bpressed.Add(5);
        }

        if (sb1.GetComponent<ChimeButton>().pressed == false && sb2.GetComponent<ChimeButton>().pressed == true &&
            sb3.GetComponent<ChimeButton>().pressed == false && sb4.GetComponent<ChimeButton>().pressed == false &&
            sb5.GetComponent<ChimeButton>().pressed == false && sb6.GetComponent<ChimeButton>().pressed == false &&
                sb7.GetComponent<ChimeButton>().pressed == false && sb8.GetComponent<ChimeButton>().pressed == false &&
                sb9.GetComponent<ChimeButton>().pressed == false && sb10.GetComponent<ChimeButton>().pressed == false)
        {
            bpressed.Clear();
            SimonSaysSwitch.GetComponent<ChimeReset>().pressed = true;
            sb2.GetComponent<ChimeButton>().pressed = false;
        }

        if (sb1.GetComponent<ChimeButton>().pressed == false && sb2.GetComponent<ChimeButton>().pressed == false &&
            sb3.GetComponent<ChimeButton>().pressed == true && sb4.GetComponent<ChimeButton>().pressed == false &&
            sb5.GetComponent<ChimeButton>().pressed == false && sb6.GetComponent<ChimeButton>().pressed == false &&
                sb7.GetComponent<ChimeButton>().pressed == false && sb8.GetComponent<ChimeButton>().pressed == false &&
                sb9.GetComponent<ChimeButton>().pressed == false && sb10.GetComponent<ChimeButton>().pressed == false)
        {
            bpressed.Add(4);
            sb3.GetComponent<ChimeButton>().pressed = false;
        }

        if (sb1.GetComponent<ChimeButton>().pressed == false && sb2.GetComponent<ChimeButton>().pressed == false &&
            sb3.GetComponent<ChimeButton>().pressed == false && sb4.GetComponent<ChimeButton>().pressed == true &&
            sb5.GetComponent<ChimeButton>().pressed == false && sb6.GetComponent<ChimeButton>().pressed == false &&
                sb7.GetComponent<ChimeButton>().pressed == false && sb8.GetComponent<ChimeButton>().pressed == false &&
                sb9.GetComponent<ChimeButton>().pressed == false && sb10.GetComponent<ChimeButton>().pressed == false)
        {
            bpressed.Add(3);
            sb4.GetComponent<ChimeButton>().pressed = false;
        }

        if (sb1.GetComponent<ChimeButton>().pressed == false && sb2.GetComponent<ChimeButton>().pressed == false &&
            sb3.GetComponent<ChimeButton>().pressed == false && sb4.GetComponent<ChimeButton>().pressed == false &&
            sb5.GetComponent<ChimeButton>().pressed == true && sb6.GetComponent<ChimeButton>().pressed == false &&
                sb7.GetComponent<ChimeButton>().pressed == false && sb8.GetComponent<ChimeButton>().pressed == false &&
                sb9.GetComponent<ChimeButton>().pressed == false && sb10.GetComponent<ChimeButton>().pressed == false)
        {
            bpressed.Add(2);
            sb5.GetComponent<ChimeButton>().pressed = false;
        }

        if (sb1.GetComponent<ChimeButton>().pressed == false && sb2.GetComponent<ChimeButton>().pressed == false &&
            sb3.GetComponent<ChimeButton>().pressed == false && sb4.GetComponent<ChimeButton>().pressed == false &&
            sb5.GetComponent<ChimeButton>().pressed == false && sb6.GetComponent<ChimeButton>().pressed == true &&
                sb7.GetComponent<ChimeButton>().pressed == false && sb8.GetComponent<ChimeButton>().pressed == false &&
                sb9.GetComponent<ChimeButton>().pressed == false && sb10.GetComponent<ChimeButton>().pressed == false)
        {
            bpressed.Clear();
            SimonSaysSwitch.GetComponent<ChimeReset>().pressed = true;
            sb6.GetComponent<ChimeButton>().pressed = false;
        }
        if (sb1.GetComponent<ChimeButton>().pressed == false && sb2.GetComponent<ChimeButton>().pressed == false &&
            sb3.GetComponent<ChimeButton>().pressed == false && sb4.GetComponent<ChimeButton>().pressed == false &&
            sb5.GetComponent<ChimeButton>().pressed == false && sb6.GetComponent<ChimeButton>().pressed == false &&
                sb7.GetComponent<ChimeButton>().pressed == true && sb8.GetComponent<ChimeButton>().pressed == false &&
                sb9.GetComponent<ChimeButton>().pressed == false && sb10.GetComponent<ChimeButton>().pressed == false)
        {
            
            bpressed.Add(1);
            sb7.GetComponent<ChimeButton>().pressed = false;
        }
    

        if (sb1.GetComponent<ChimeButton>().pressed == false && sb2.GetComponent<ChimeButton>().pressed == false &&
            sb3.GetComponent<ChimeButton>().pressed == false && sb4.GetComponent<ChimeButton>().pressed == false &&
            sb5.GetComponent<ChimeButton>().pressed == false && sb6.GetComponent<ChimeButton>().pressed == false &&
                sb7.GetComponent<ChimeButton>().pressed == false && sb8.GetComponent<ChimeButton>().pressed == true &&
                sb9.GetComponent<ChimeButton>().pressed == false && sb10.GetComponent<ChimeButton>().pressed == false)
        {
            bpressed.Clear();
            SimonSaysSwitch.GetComponent<ChimeReset>().pressed = true;
            sb8.GetComponent<ChimeButton>().pressed = false;
        }

        if (sb1.GetComponent<ChimeButton>().pressed == false && sb2.GetComponent<ChimeButton>().pressed == false &&
            sb3.GetComponent<ChimeButton>().pressed == false && sb4.GetComponent<ChimeButton>().pressed == false &&
            sb5.GetComponent<ChimeButton>().pressed == false && sb6.GetComponent<ChimeButton>().pressed == false &&
                sb7.GetComponent<ChimeButton>().pressed == false && sb8.GetComponent<ChimeButton>().pressed == false &&
                sb9.GetComponent<ChimeButton>().pressed == true && sb10.GetComponent<ChimeButton>().pressed == false)
        {
            bpressed.Clear();
            SimonSaysSwitch.GetComponent<ChimeReset>().pressed = true;
            sb8.GetComponent<ChimeButton>().pressed = false;
        }
        if (sb1.GetComponent<ChimeButton>().pressed == false && sb2.GetComponent<ChimeButton>().pressed == false &&
            sb3.GetComponent<ChimeButton>().pressed == false && sb4.GetComponent<ChimeButton>().pressed == false &&
            sb5.GetComponent<ChimeButton>().pressed == false && sb6.GetComponent<ChimeButton>().pressed == false &&
                sb7.GetComponent<ChimeButton>().pressed == false && sb8.GetComponent<ChimeButton>().pressed == false &&
                sb9.GetComponent<ChimeButton>().pressed == false && sb10.GetComponent<ChimeButton>().pressed == true)
        {
            
            bpressed.Add(0);
            sb10.GetComponent<ChimeButton>().pressed = false;
        }

        if (bpressed.Count > 5)
        {
            if (sequence[0] == bpressed[0])
            {
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
            if (sequence[4] == bpressed[4])
            {
                b5 = true;
            }

           
            if (sequence[5] == bpressed[5])
            {
                b6 = true;
            }
            if (b1 == true && b2 == true && b3 == true && b4 == true && b5 == true && b6 == true)
            {
                GameObject.Destroy(door);
            }
        }
    }

    
}