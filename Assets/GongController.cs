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
    int i = 0;
    int state = 0;

    void Start()
    {
        // Get a reference to the SpriteRenderer so that we can change the button's color.
        gameObject.SetActive(true);

        Randomize();
        
        
        sb5.SetActive(false);
        sb6.SetActive(false);
        sb7.SetActive(false);
        sb8.SetActive(false);
    }
    void Randomize()
    {
        System.Random rnd = new System.Random();
        int randomNumber = rnd.Next(0, 4);
        sequence.Add(randomNumber);
        while (sequence.Contains(randomNumber))
        {
            if (sequence.Count < 4)
            {
                randomNumber = rnd.Next(0, 4);
                if (!sequence.Contains(randomNumber))
                {
                    sequence.Add(randomNumber);
                }
            }
            else
            {
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {   
        if(sequence.IndexOf(i) == 3)
        {
            sb5.SetActive(false);
            sb6.SetActive(false);
            sb7.SetActive(false);
            sb8.SetActive(true);
        }
        else if (sequence.IndexOf(i) == 2)
        {
            sb5.SetActive(false);
            sb6.SetActive(false);
            sb7.SetActive(true);
            sb8.SetActive(false);
        }
        else if (sequence.IndexOf(i) == 1)
        {
            sb5.SetActive(false);
            sb6.SetActive(true);
            sb7.SetActive(false);
            sb8.SetActive(false);
        }
        else if (sequence.IndexOf(i) == 0)
        {
            sb5.SetActive(true);
            sb6.SetActive(false);
            sb7.SetActive(false);
            sb8.SetActive(false);
        }

        if (SimonSaysSwitch.GetComponent<GongSwitch>().pressed == true)
        {
            bpressed.Clear();
            sequence.Clear();
            
            i = 0;
            SimonSaysSwitch.GetComponent<GongSwitch>().pressed = false;
            Randomize();
        }
        if (SimonSaysSwitch.GetComponent<GongSwitch>().cheat == true)
        {
            GameObject.Destroy(door);

        }

        if (sb1.GetComponent<GongButton>().pressed == true && sb2.GetComponent<GongButton>().pressed == false &&
                sb3.GetComponent<GongButton>().pressed == false && sb4.GetComponent<GongButton>().pressed == false)
        {
            sb1.GetComponent<GongButton>().pressed = false;
            bpressed.Add(3);
            i++;
        }
        if (sb1.GetComponent<GongButton>().pressed == false && sb2.GetComponent<GongButton>().pressed == true &&
           sb3.GetComponent<GongButton>().pressed == false && sb4.GetComponent<GongButton>().pressed == false)
        {
            bpressed.Add(2);
            i++;
            sb2.GetComponent<GongButton>().pressed = false;
        }
        if (sb1.GetComponent<GongButton>().pressed == false && sb2.GetComponent<GongButton>().pressed == false &&
           sb3.GetComponent<GongButton>().pressed == true && sb4.GetComponent<GongButton>().pressed == false)
        {
            bpressed.Add(1);
            i++;
            sb3.GetComponent<GongButton>().pressed = false;
        }

        if (sb1.GetComponent<GongButton>().pressed == false && sb2.GetComponent<GongButton>().pressed == false &&
           sb3.GetComponent<GongButton>().pressed == false && sb4.GetComponent<GongButton>().pressed == true)
        {
            bpressed.Add(0);
            i++;
            sb4.GetComponent<GongButton>().pressed = false;
        }
        if (bpressed.Count > 3)
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
            if (b1 == true && b2 == true && b3 == true && b4 == true)
            {
                GameObject.Destroy(door);
            }
            
        }
      // Debug.Log(bpressed[0] + bpressed[1] + bpressed[2] + bpressed[3]);
    }

}

