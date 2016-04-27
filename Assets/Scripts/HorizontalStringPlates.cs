using UnityEngine;
using System.Collections;

public class HorizontalStringPlates : MonoBehaviour {

    public GameObject horizontalPuzzle;
    HorizontalStringPuzzle puzzleScript;
    public string plateName;
    bool activateButton = true;
    void Start()
    {
        puzzleScript = horizontalPuzzle.GetComponent<HorizontalStringPuzzle>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player" && activateButton == true)
        {
            activateButton = false;
            StartCoroutine(DelayButton());
            puzzleScript.MoveNote(plateName);
        }
    }

    IEnumerator DelayButton()
    {
        yield return new WaitForSeconds(1);
        activateButton = true;
        yield return null;
    }
}
