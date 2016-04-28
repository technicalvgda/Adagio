using UnityEngine;
using System.Collections;

public class FinalPuzzleController : MonoBehaviour
{
	public bool FinalPuzzleDone;
	public bool FinalPuzzleReady;
    bool phase1 = true, phase2 = false, phase3 = false, start = true;
    public GameObject[] buttons = new GameObject[9];
    int[] sequence = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    int[] pressed = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0};
    int count = 0;
    public Vector3 startpoint;
    public GameObject player;
    bool collide = false;
    // Use this for initialization
    void Start () {
		FinalPuzzleDone = false;
		FinalPuzzleReady = false;
		player = GameObject.FindGameObjectWithTag ("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(collide == true && Input.GetKeyDown(KeyCode.E))
        {
            if (phase1)
            {
                
                for (int i = 0; i < 5; i++)
                {
                    buttons[i].GetComponent<FinalMovingPlatform>().speed = .05f;
                }
            }
            if (phase2)
            {
                count = 0;
                for (int i = 0; i < 7; i++)
                {
                    buttons[i].GetComponent<FinalMovingPlatform>().speed = .05f;
                    buttons[i].GetComponent<FinalMovingPlatform>().firsthit = true;

                }
                for (int i = 0; i < 7; i++)
                {
                    buttons[i].GetComponent<FinalMovingPlatform>().resetPlatform();
                }
            }
            if (phase3)
            {
                count = 0;
                for (int i = 0; i < 9; i++)
                {
                    buttons[i].GetComponent<FinalMovingPlatform>().speed = .05f;
                    buttons[i].GetComponent<FinalMovingPlatform>().sphere.GetComponent<FinalPuzzleSphere>().speed = .05f;
                    buttons[i].GetComponent<FinalMovingPlatform>().firsthit = true;
                }
                for (int i = 0; i < 9; i++)
                {
                    buttons[i].GetComponent<FinalMovingPlatform>().resetPlatform();
                }
            }
        }
        if (phase1)
        {
            if (start)
            {
                startpoint = player.transform.position;
                for (int i = 5; i < 9; i++)
                {
                    buttons[i].gameObject.SetActive(false);
                }

                start = false;
            }
            for (int i = 0; i < 5; i++)
            {
                
                if (buttons[i].gameObject.GetComponent<FinalMovingPlatform>().collide == true)
                {
                    pressed[count] = buttons[i].gameObject.GetComponent<FinalMovingPlatform>().value;
                    buttons[i].gameObject.GetComponent<FinalMovingPlatform>().collide = false;
                    count++;
                }
            }


            if (sequence[0] == pressed[0] && sequence[1] == pressed[1] && sequence[2] == pressed[2] && sequence[3] == pressed[3] && sequence[4] == pressed[4])
            {
                phase2 = true;
                phase1 = false;
                start = true;
                count = 0;
                for (int i = 0; i < 5; i++)
                {
                    buttons[i].gameObject.GetComponent<FinalMovingPlatform>().firsthit = true;
                }
            }
        }

        if (phase2 )
        {
            if (start)
            {
                player.transform.position = startpoint;
                for (int i = 0; i < 7; i++)
                {
                    buttons[i].GetComponent<FinalMovingPlatform>().activateSphere();
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].GetComponent<FinalMovingPlatform>().speed = .05f;
                    buttons[i].GetComponent<FinalMovingPlatform>().sphase = true;
                }
                for(int i = 0; i < 5; i++)
                {
                    buttons[i].GetComponent<FinalMovingPlatform>().resetPlatform();
                }
                start = false;
            }
            for (int i = 0; i < 7; i++)
            {
                if (buttons[i].gameObject.GetComponent<FinalMovingPlatform>().collide == true)
                {
                    pressed[count] = buttons[i].gameObject.GetComponent<FinalMovingPlatform>().value;
                    buttons[i].gameObject.GetComponent<FinalMovingPlatform>().collide = false;
                    count++;
                }
            }


            if (sequence[0] == pressed[0] && sequence[1] == pressed[1] && sequence[2] == pressed[2] 
                && sequence[3] == pressed[3] && sequence[4] == pressed[4] && sequence[5] == pressed[5] && sequence[6] == pressed[6])
            {
                phase3 = true;
                phase2 = false;
                start = true;
                count = 0;
                for (int i = 0; i < 7; i++)
                {
                    buttons[i].gameObject.GetComponent<FinalMovingPlatform>().firsthit = true;
                }
            }
        }
        if (phase3)
        {
            if (start)
            {
                player.transform.position = startpoint;
                for (int i = 0; i < 9; i++)
                {
                    buttons[i].GetComponent<FinalMovingPlatform>().activateSphere();
                    buttons[i].GetComponent<FinalMovingPlatform>().movingSphere();
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].GetComponent<FinalMovingPlatform>().speed = .05f;
                }
                for (int i = 0; i < 7; i++)
                {
                    buttons[i].GetComponent<FinalMovingPlatform>().resetPlatform();
                }
                start = false;
            }
            for (int i = 0; i < 9; i++)
            {
                if (buttons[i].gameObject.GetComponent<FinalMovingPlatform>().collide == true)
                {
                    pressed[count] = buttons[i].gameObject.GetComponent<FinalMovingPlatform>().value;
                    buttons[i].gameObject.GetComponent<FinalMovingPlatform>().collide = false;
                    count++;
                    buttons[i].GetComponent<FinalMovingPlatform>().sphere.GetComponent<FinalPuzzleSphere>().speed = 0;
                }
            }


            if (sequence[0] == pressed[0] && sequence[1] == pressed[1] && sequence[2] == pressed[2]
                && sequence[3] == pressed[3] && sequence[4] == pressed[4] && sequence[5] == pressed[5] && sequence[6] == pressed[6]
                && sequence[7] == pressed[7] && sequence[8] == pressed[8])
            {
				FinalPuzzleDone = true;
                Debug.Log("DONE");
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        collide = true;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        collide = false;
    }
}
