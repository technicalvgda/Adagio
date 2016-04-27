using UnityEngine;
using System.Collections;

public class HorizontalStringPuzzle : MonoBehaviour {

    //top notch locations
    Vector2 tN1;
    Vector2 tN2;
    Vector2 tN3;
    //middle notch locations
    Vector2 mN1;
    Vector2 mN2;
    Vector2 mN3;
    Vector2 mN4;
    Vector2 mN5;
    //bottom notch locations
    Vector2 bN1;
    Vector2 bN2;
    Vector2 bN3;
    Vector2 bN4;
    Vector2 bN5;
    Vector2 bN6;
    Vector2 bN7;

    Vector2[] topNotches;
    Vector2[] middleNotches;
    Vector2[] bottomNotches;

    
    AudioSource audioSource;
    public AudioClip[] bottomClips;
    public AudioClip[] middleClips;
    public AudioClip[] topClips;

    public GameObject topString, middleString, bottomString;
    public GameObject topNote, midNote, bottomNote;
    public int topNoteIndex, midNoteIndex, bottomNoteIndex;

    public int[] solution;
    bool complete = false;
    float noteOffset = 0.8f;
	// Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        //set initial pozitions of notes
        tN2 = topString.transform.position;
        tN1 = tN2 - new Vector2(noteOffset,0);
        tN3 = tN2 + new Vector2(noteOffset, 0);
        mN3 = middleString.transform.position;
        mN2 = mN3 - new Vector2(noteOffset, 0);
        mN1 = mN2 - new Vector2(noteOffset, 0);
        mN4 = mN3 +new Vector2(noteOffset, 0);
        mN5 = mN4 + new Vector2(noteOffset, 0);
        bN4 = bottomString.transform.position;
        bN3 = bN4 - new Vector2(noteOffset, 0);
        bN2 = bN3 - new Vector2(noteOffset, 0);
        bN1 = bN2 - new Vector2(noteOffset, 0);
        bN5 = bN4 + new Vector2(noteOffset, 0);
        bN6 = bN5 + new Vector2(noteOffset, 0);
        bN7 = bN6 + new Vector2(noteOffset, 0);


        //initialize arrays of the various notch positions
        topNotches = new Vector2[]{tN1,tN2,tN3};
        middleNotches = new Vector2[] { mN1, mN2, mN3,mN4, mN5 };
        bottomNotches = new Vector2[] { bN1, bN2, bN3, bN4, bN5,bN6, bN7 };
        //set the position of notes to random notches
        //index variables hold position
        topNoteIndex = Random.Range(0, topNotches.Length - 1);
        topNote.transform.position = topNotches[topNoteIndex];
        midNoteIndex = Random.Range(0, middleNotches.Length - 1);
        midNote.transform.position = middleNotches[midNoteIndex];
        bottomNoteIndex = Random.Range(0, bottomNotches.Length - 1);
        bottomNote.transform.position = bottomNotches[bottomNoteIndex];
        //design solution
        solution = new int[]{ Random.Range(0, topNotches.Length - 1),Random.Range(0, middleNotches.Length - 1),Random.Range(0, bottomNotches.Length - 1) };
        //prevent the game from already being in solution position
        if (solution == new int[]{topNoteIndex,midNoteIndex, bottomNoteIndex})
        {
            if(topNoteIndex == topNotches.Length - 1)
            {
                topNoteIndex--;
            }
            else
            {
                topNoteIndex++;
            }
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (solution[0] == topNoteIndex && solution[1]== midNoteIndex && solution[2]== bottomNoteIndex  && complete == false)
        {
            Debug.Log("You solved the string puzzle");
            StartCoroutine(PlayWin());
            complete = true;
            Camera.main.GetComponent<OpenGate>().doneCounter++;
        }

    }
    public void MoveNote(string plate)
    {

        if (plate == "TopLeft" && topNoteIndex > 0)
        {
            
            topNoteIndex--;
            topNote.transform.position = topNotches[topNoteIndex];
            audioSource.clip = topClips[topNoteIndex];
            audioSource.Play();
            return;
        }
        
   
        if (plate == "TopRight" && topNoteIndex < topNotches.Length - 1)
        {
            
            topNoteIndex++;
            topNote.transform.position = topNotches[topNoteIndex];
            audioSource.clip = topClips[topNoteIndex];
            audioSource.Play();
            return;
        }
  
        if (plate == "MiddleLeft" && midNoteIndex > 0)
        {
           
            midNoteIndex--;
            midNote.transform.position = middleNotches[midNoteIndex];
            audioSource.clip = middleClips[midNoteIndex];
            audioSource.Play();
            return;
        }
   
        if (plate == "MiddleRight" && midNoteIndex < middleNotches.Length - 1)
        {
           
            midNoteIndex++;
            midNote.transform.position = middleNotches[midNoteIndex];
            audioSource.clip = middleClips[midNoteIndex];
            audioSource.Play();
            return;
        }
   
        if (plate == "BottomLeft" && bottomNoteIndex > 0)
        {
            
            bottomNoteIndex--;
            bottomNote.transform.position = bottomNotches[bottomNoteIndex];
            audioSource.clip = bottomClips[bottomNoteIndex];
            audioSource.Play();
            return;
        }
    
    
        if (plate == "BottomRight" && bottomNoteIndex < bottomNotches.Length - 1)
        {
            
            bottomNoteIndex++;
            bottomNote.transform.position = bottomNotches[bottomNoteIndex];
            audioSource.clip = bottomClips[bottomNoteIndex];
            audioSource.Play();
            return;
        }
        if (plate == "Test")
        {
            StartCoroutine(PlayDemo());
            return;
        }
    }

    IEnumerator PlayDemo()
    {
        audioSource.clip = topClips[solution[0]];
        audioSource.Play();
        yield return new WaitForSeconds(1);
        audioSource.clip = middleClips[solution[1]];
        audioSource.Play();
        yield return new WaitForSeconds(1);
        audioSource.clip = bottomClips[solution[2]];
        audioSource.Play();
        yield return new WaitForSeconds(1);
    }
    IEnumerator PlayWin()
    {
        yield return new WaitForSeconds(1);
        audioSource.clip = topClips[solution[0]];
        audioSource.Play();
        yield return new WaitForSeconds(0.2f);
        audioSource.clip = middleClips[solution[1]];
        audioSource.Play();
        yield return new WaitForSeconds(0.2f);
        audioSource.clip = bottomClips[solution[2]];
        audioSource.Play();
        yield return new WaitForSeconds(0.2f);
    }


}
