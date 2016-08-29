using UnityEngine;
using System.Collections;

public class Codex : MonoBehaviour {

    private CodexEventHandler codexHandler;

	public bool onCodex;
	public float travelDistance = 1f;
	public float speed = 1;
	private Vector3 codexPosition;
	private Vector3 startingPosition;

	private Vector3 newPosition;
	//private bool goingUp;

    //variable of all codecies unlocked
    private string codexPref;
    private int codexNumber;
    private string codexText;
	// Use this for initialization
	void Start () 
	{
        //store a reference to the codex handler
        codexHandler = GameObject.Find("CodexEventHandler").GetComponent<CodexEventHandler>();

		//goingUp = true;
		onCodex = false;	
		startingPosition = this.gameObject.transform.position;
        //get list of all unlocked codecies
        codexPref = PlayerPrefs.GetString("Codecies");
        //if this is the tutorial codex
        if (gameObject.name == "IntroCodex")
        {
            //set this codex to number 7 (An adventurers journal I)
            codexNumber = 7;
            //find the codex text associated with that number
            if (CodexPrep.codexTextDict.ContainsKey(codexNumber))
            {
                codexText = CodexPrep.codexTextDict[codexNumber];
            }
            else //if the number doesnt exist
            {
                Debug.Log("Codex text is not assigned.");
            }
        }
        //if this is a spawned codex
        else
        {
            //set this codex to a random codex index
            codexNumber = Random.Range(1, CodexPrep.maxCodecies);
            //find the codex text associated with that number
            if (CodexPrep.codexTextDict.ContainsKey(codexNumber))
            {
                codexText = CodexPrep.codexTextDict[codexNumber];
            }
            else //if the number doesnt exist
            {
                Debug.Log("Codex text is not assigned.");
            }
        }
        
            

	}
	
	// Update is called once per frame
	void Update () 
	{	
        //if player is touching codex and if they press E or click the mouse (or tap)
		if (onCodex == true && (Input.GetKeyDown (KeyCode.E) || Input.GetMouseButtonDown(0))) 
		{
            //unlock respective codex
            UnlockCodex();
            //display text
            DisplayCodex();
            Debug.Log(codexText);
            Destroy (this.gameObject);
            
		}
		transform.position = new Vector3 (transform.position.x, Mathf.PingPong (Time.time*speed, travelDistance)+startingPosition.y, transform.position.z);

	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			onCodex = true;
		}
			
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			onCodex = false;
		}
	}
    void DisplayCodex()
    {
        codexHandler.ActivateCodexCanvax(codexText);
    }
    void UnlockCodex()
    {
        //remove the value at this codex's index
        codexPref = codexPref.Remove(codexNumber-1, 1);
        //insert the new value
        codexPref = codexPref.Insert(codexNumber-1, "1");
        //assign this to codecies playerpref
        PlayerPrefs.SetString("Codecies", codexPref);
    }
}
