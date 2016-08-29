using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
[RequireComponent(typeof(AudioSource))]

public class NewChimePuzzle : MonoBehaviour
{

    SpriteRenderer sr, ss;
    public GameObject sb1, sb2, sb3;
    public AudioSource audioSource;
    List<int> sequence = new List<int>();
    List<int> bpressed = new List<int>();
    public GameObject door;
    bool b1 = false, b2 = false, b3 = false;//sequence1 = false, sequence2 = false, sequence3 = false;

    //int state = 0;
    //int rnd = 0;


    void Start()
    {
        // Get a reference to the SpriteRenderer so that we can change the button's color.
        gameObject.SetActive(true);
        sequence.Add(5);
        sequence.Add(4);
        sequence.Add(3);

    }

    // Update is called once per frame
    void Update()
    {
      

        if (sb1.GetComponent<NewChimePuzzleButton>().pressed == true && sb2.GetComponent<NewChimePuzzleButton>().pressed == false &&
                sb3.GetComponent<NewChimePuzzleButton>().pressed == false)
        {
            sb1.GetComponent<NewChimePuzzleButton>().pressed = false;
            bpressed.Add(5);

            Debug.Log(b1);
        }

        if (sb1.GetComponent<NewChimePuzzleButton>().pressed == false && sb2.GetComponent<NewChimePuzzleButton>().pressed == true &&
            sb3.GetComponent<NewChimePuzzleButton>().pressed == false)
        {
            bpressed.Add(4);

            sb2.GetComponent<NewChimePuzzleButton>().pressed = false;
            Debug.Log(b2);
        }

        if (sb1.GetComponent<NewChimePuzzleButton>().pressed == false && sb2.GetComponent<NewChimePuzzleButton>().pressed == false &&
            sb3.GetComponent<NewChimePuzzleButton>().pressed == true)
        {
            bpressed.Add(3);

            sb3.GetComponent<NewChimePuzzleButton>().pressed = false;
            Debug.Log(b3);
        }



        if (bpressed.Count == 3)
        {
            if (sequence[0] == bpressed[0])
            {
                b1 = true;
                //sequence1 = true;
            }
            if (sequence[1] == bpressed[1])
            {
                b2 = true;
                //sequence2 = true;
            }
            if (sequence[2] == bpressed[2])
            {
                b3 = true;
                //sequence3 = true;
            }

            if (b1 == true && b2 == true && b3 == true)
            {
                GameObject.Destroy(door);
            }
            else
            {
                
                b1 = false;
                b2 = false;
                b3 = false;
                bpressed.Clear();
                audioSource.Play();
            }
            

        }
    }
}


