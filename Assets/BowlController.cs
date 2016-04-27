using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BowlController : MonoBehaviour {

    SpriteRenderer sr, ss;
    public GameObject SimonSaysSwitch;
    public GameObject sb1, sb2, sb3, sb4, sb5, sb6, sb7, sb8, sb9;
    List<int> sequence = new List<int>();
    List<int> bpressed = new List<int>();
    public GameObject door;
    bool b1 = false, b2 = false, b3 = false, b4 = false;
    int i = 0;
    int state = 0;
    int rnd = 0;


    void Start()
     {
         // Get a reference to the SpriteRenderer so that we can change the button's color.
         gameObject.SetActive(true);
        Randomize();
        sb7.SetActive(false);
        sb8.SetActive(false);
        sb9.SetActive(false);

    }
    void Randomize()
    {
        System.Random rnd = new System.Random();
        int randomNumber = rnd.Next(0, 3);
        sequence.Add(randomNumber);
        while (sequence.Contains(randomNumber))
        {
            if (sequence.Count < 3)
            {
                randomNumber = rnd.Next(0, 3);
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
        if (sequence.IndexOf(i) == 2)
        {
            sb7.SetActive(true);
            sb8.SetActive(false);
            sb9.SetActive(false);

        }
        else if (sequence.IndexOf(i) == 1)
        {
            sb7.SetActive(false);
            sb8.SetActive(true);
            sb9.SetActive(false);
        }
        else if (sequence.IndexOf(i) == 0)
        {
            sb7.SetActive(false);
            sb8.SetActive(false);
            sb9.SetActive(true);
        }
    
        if (SimonSaysSwitch.GetComponent<BowlsSwitch>().pressed == true)
        {
            bpressed.Clear();
            sequence.Clear();
            Randomize();
            i = 0;
            SimonSaysSwitch.GetComponent<BowlsSwitch>().pressed = false;
        }
        if (SimonSaysSwitch.GetComponent<BowlsSwitch>().cheat == true)
        {
            GameObject.Destroy(door);
        }

        if (sb1.GetComponent<BowlsButton>().pressed == true && sb2.GetComponent<BowlsButton>().pressed == false &&
                sb3.GetComponent<BowlsButton>().pressed == false && sb4.GetComponent<BowlsButton>().pressed == false &&
           sb5.GetComponent<BowlsButton>().pressed == false && sb6.GetComponent<BowlsButton>().pressed == false)
        {
            Debug.Log("BLACK1");
            sb1.GetComponent<BowlsButton>().pressed = false;
            bpressed.Add(2);
            i++;
            Debug.Log(bpressed[0]);
        }

        if (sb1.GetComponent<BowlsButton>().pressed == false && sb2.GetComponent<BowlsButton>().pressed == true &&
           sb3.GetComponent<BowlsButton>().pressed == false && sb4.GetComponent<BowlsButton>().pressed == false &&
           sb5.GetComponent<BowlsButton>().pressed == false && sb6.GetComponent<BowlsButton>().pressed == false)
        {
            Debug.Log("BLACK2");
            bpressed.Add(2);
            i++;

            sb2.GetComponent<BowlsButton>().pressed = false;
        }

        if (sb1.GetComponent<BowlsButton>().pressed == false && sb2.GetComponent<BowlsButton>().pressed == false &&
           sb3.GetComponent<BowlsButton>().pressed == true && sb4.GetComponent<BowlsButton>().pressed == false &&
           sb5.GetComponent<BowlsButton>().pressed == false && sb6.GetComponent<BowlsButton>().pressed == false)
        {
            Debug.Log("GREEN1");
            bpressed.Add(1);
            i++;

            sb3.GetComponent<BowlsButton>().pressed = false;
        }

        if (sb1.GetComponent<BowlsButton>().pressed == false && sb2.GetComponent<BowlsButton>().pressed == false &&
           sb3.GetComponent<BowlsButton>().pressed == false && sb4.GetComponent<BowlsButton>().pressed == true &&
           sb5.GetComponent<BowlsButton>().pressed == false && sb6.GetComponent<BowlsButton>().pressed == false)
        {
            Debug.Log("GREEN2");
            bpressed.Add(1);
            i++;

            sb4.GetComponent<BowlsButton>().pressed = false;
        }

        if (sb1.GetComponent<BowlsButton>().pressed == false && sb2.GetComponent<BowlsButton>().pressed == false &&
           sb3.GetComponent<BowlsButton>().pressed == false && sb4.GetComponent<BowlsButton>().pressed == false &&
           sb5.GetComponent<BowlsButton>().pressed == true && sb6.GetComponent<BowlsButton>().pressed == false)
        {
            Debug.Log("BLUE1");
            bpressed.Add(0);
            i++;

            sb5.GetComponent<BowlsButton>().pressed = false;
        }

        if (sb1.GetComponent<BowlsButton>().pressed == false && sb2.GetComponent<BowlsButton>().pressed == false &&
          sb3.GetComponent<BowlsButton>().pressed == false && sb4.GetComponent<BowlsButton>().pressed == false &&
          sb5.GetComponent<BowlsButton>().pressed == false && sb6.GetComponent<BowlsButton>().pressed == true)
        {
            Debug.Log("BLUE2");
            bpressed.Add(0);
            i++;

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
                GameObject.Destroy(door);
            }
            
        }
        // Debug.Log(bpressed[0] + bpressed[1] + bpressed[2] + bpressed[3]);
    }
}
