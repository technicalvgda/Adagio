using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MovingNote : MonoBehaviour {

    SpriteRenderer sr, ss;
    public GameObject SimonSaysSwitch;
    public GameObject sb1, sb2, sb3, sb4, sb5;
    List<int> sequence = new List<int>();
    List<int> bpressed = new List<int>();
    public GameObject door;
    bool b1 = false, b2 = false, b3 = false, b4 = false, b5 = false;

    int state = 0;
    int rnd = 0;


    void Start()
    {
        // Get a reference to the SpriteRenderer so that we can change the button's color.
        gameObject.SetActive(true);
        sequence.Add(4);
        sequence.Add(3);
        sequence.Add(2);
        sequence.Add(1);
        sequence.Add(0);

    }

    // Update is called once per frame
    void Update()
    {
        if (SimonSaysSwitch.GetComponent<NoteSwitch>().pressed == true)
        {
            bpressed.Clear();
            //sequence.Clear();
            SimonSaysSwitch.GetComponent<NoteSwitch>().pressed = false;

        }

        if (sb1.GetComponent<NotesButton>().pressed == true && sb2.GetComponent<NotesButton>().pressed == false &&
                sb3.GetComponent<NotesButton>().pressed == false && sb4.GetComponent<NotesButton>().pressed == false &&
           sb5.GetComponent<NotesButton>().pressed == false)
        {
            Debug.Log("ORANGE");
            sb1.GetComponent<NotesButton>().pressed = false;
            bpressed.Add(4);
        }

        if (sb1.GetComponent<NotesButton>().pressed == false && sb2.GetComponent<NotesButton>().pressed == true &&
           sb3.GetComponent<NotesButton>().pressed == false && sb4.GetComponent<NotesButton>().pressed == false &&
           sb5.GetComponent<NotesButton>().pressed == false)
        {
            Debug.Log("PINK");
            bpressed.Add(3);
            sb2.GetComponent<NotesButton>().pressed = false;
        }

        if (sb1.GetComponent<NotesButton>().pressed == false && sb2.GetComponent<NotesButton>().pressed == false &&
           sb3.GetComponent<NotesButton>().pressed == true && sb4.GetComponent<NotesButton>().pressed == false &&
           sb5.GetComponent<NotesButton>().pressed == false)
        {
            Debug.Log("YELLOW");
            bpressed.Add(2);
            sb3.GetComponent<NotesButton>().pressed = false;
        }

        if (sb1.GetComponent<NotesButton>().pressed == false && sb2.GetComponent<NotesButton>().pressed == false &&
           sb3.GetComponent<NotesButton>().pressed == false && sb4.GetComponent<NotesButton>().pressed == true &&
           sb5.GetComponent<NotesButton>().pressed == false)
        {
            Debug.Log("GREEN");
            bpressed.Add(1);
            sb4.GetComponent<NotesButton>().pressed = false;
        }

        if (sb1.GetComponent<NotesButton>().pressed == false && sb2.GetComponent<NotesButton>().pressed == false &&
           sb3.GetComponent<NotesButton>().pressed == false && sb4.GetComponent<NotesButton>().pressed == false &&
           sb5.GetComponent<NotesButton>().pressed == true)
        {
            Debug.Log("BLUE");
            bpressed.Add(0);
            sb5.GetComponent<NotesButton>().pressed = false;
        }

        
        if (bpressed.Count > 4)
        {
            if (sequence[0] == bpressed[0])
            {
                Debug.Log("b1 = true");
                b1 = true;
            }
            if (sequence[1] == bpressed[1])
            {
                Debug.Log("b2 = true");
                b2 = true;
            }
            if (sequence[2] == bpressed[2])
            {
                Debug.Log("b3 = true");
                b3 = true;
            }
            if (sequence[3] == bpressed[3])
            {
                Debug.Log("b4 = true");
                b4 = true;
            }
            if (sequence[4] == bpressed[4])
            {
                Debug.Log("b5 = true");
                b5 = true;
            }
            if (b1 == true && b2 == true && b3 == true && b4 == true && b5 == true)
            {
                GameObject.Destroy(door);
            }
        }
        // Debug.Log(bpressed[0] + bpressed[1] + bpressed[2] + bpressed[3]);
    }
}
