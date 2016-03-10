using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SimonSays : MonoBehaviour
{
    SpriteRenderer sr, ss;
    public GameObject SimonSaysSwitch;
    public GameObject sb1, sb2, sb3, sb4;
    List<int> sequence = new List<int>();
    List<int> bpressed = new List<int>();
    public GameObject door;
    bool b1 = false, b2 = false, b3 = false, b4 = false;
    
    int state = 0;
    int rnd = 0;


    void Awake()
    {
        // Get a reference to the SpriteRenderer so that we can change the button's color.
        sr = this.GetComponent<SpriteRenderer>();
        gameObject.SetActive(true);
        ss = SimonSaysSwitch.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (SimonSaysSwitch.GetComponent<SimonSaysSwitch>().pressed == true)
        {
            bpressed.Clear();
            sequence.Clear();
            SimonSaysSwitch.GetComponent<SimonSaysSwitch>().pressed = false; 
            StartCoroutine(CycleColors());

        }
        if (sb1.GetComponent<SimonSaysButton>().pressed == true && sb2.GetComponent<SimonSaysButton>().pressed == false &&
                sb3.GetComponent<SimonSaysButton>().pressed == false && sb4.GetComponent<SimonSaysButton>().pressed == false)
        {
            bpressed.Add(3);
            Debug.Log("aaaaaaaaaaaaaa");
            sb1.GetComponent<SimonSaysButton>().pressed = false;
        }
        if (sb1.GetComponent<SimonSaysButton>().pressed == false && sb2.GetComponent<SimonSaysButton>().pressed == true &&
           sb3.GetComponent<SimonSaysButton>().pressed == false && sb4.GetComponent<SimonSaysButton>().pressed == false)
        {
            bpressed.Add(2);
            sb2.GetComponent<SimonSaysButton>().pressed = false;
        }
        if (sb1.GetComponent<SimonSaysButton>().pressed == false && sb2.GetComponent<SimonSaysButton>().pressed == false &&
           sb3.GetComponent<SimonSaysButton>().pressed == true && sb4.GetComponent<SimonSaysButton>().pressed == false)
        {
            bpressed.Add(1);
            sb3.GetComponent<SimonSaysButton>().pressed = false;
        }

        if (sb1.GetComponent<SimonSaysButton>().pressed == false && sb2.GetComponent<SimonSaysButton>().pressed == false &&
           sb3.GetComponent<SimonSaysButton>().pressed == false && sb4.GetComponent<SimonSaysButton>().pressed == true)
        {
            bpressed.Add(0);
            sb4.GetComponent<SimonSaysButton>().pressed = false;
        }
	if (bpressed.Count  > 3) 
		{
		if (sequence [0] == bpressed [0]) 
		{
			b1 = true;
		}
		if (sequence [1] == bpressed [1]) 
		{
			b2 = true;
		}
		if (sequence [2] == bpressed [2]) 
		{
			b3 = true;
		}
		if (sequence [3] == bpressed [3]) 
		{
			b4 = true;
		}
		if (b1 == true && b2 == true && b3 == true && b4 == true) 
		{
			GameObject.Destroy (door);
		}
}
}
    public IEnumerator CycleColors()
    {
        int counter = 0;
        bool cycleDone = false;
        while (cycleDone == false)
        {
            switch (state)
            {
                case 0:
                    sr.color = new Color(1, 0.3f, 0.3f);
                    sequence.Add(state);
                    counter++;
                    break;
                case 1:
                    sr.color = new Color(0.3f, 1, 0.3f);
                    sequence.Add(state);
                    counter++;
                    break;
                case 2:
                    sr.color = new Color(0, 0, 1);
                    sequence.Add(state);
                    counter++;
                    break;
                case 3:
                    sr.color = new Color(0, 0, 0);
                    sequence.Add(state);
                    counter++;
                    break;
                default:
                    break;
            }
            rnd = (int)UnityEngine.Random.Range(0, 3);
            state = (state + rnd) % 4;
            if (counter == 4)
            {
                cycleDone = true;
                
            }
           
            yield return new WaitForSeconds(1);
            sr.color = new Color(1, 1, 1);
            yield return new WaitForSeconds(0.5f);
            Debug.Log(sequence.ToString());
        }
    }
   
    

}