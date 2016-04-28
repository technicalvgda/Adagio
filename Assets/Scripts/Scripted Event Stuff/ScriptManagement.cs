using UnityEngine;
using System.Collections;

public class ScriptManagement : MonoBehaviour 
{
	public int currentLevel;
	public int currentEventNumber;
	public GameObject[] EventTriggers;
	public TextAsset[] level1TextFiles;
	public TextAsset[] level2TextFiles;
	public TextAsset[] level3TextFiles;
	public ScriptManagerNewTextBox textBox;
	private GameObject player;
	private PlayerController playerCont;
	private Animator playerAnimator;
	private Camera mainCamera;
	public GameObject teleporter;
	public GameObject[] puzzleZoomAreaObjects;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerCont = player.GetComponent<PlayerController> ();
		playerAnimator = player.GetComponent<Animator> ();
		mainCamera = Camera.main;
		currentLevel = 1;
		currentEventNumber = 0;
		textBox = gameObject.GetComponent<ScriptManagerNewTextBox> ();
		puzzleZoomAreaObjects = GameObject.FindGameObjectsWithTag ("PuzzleZoomArea");
		//textBox.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (currentLevel) 
		{
		case 1:
			{
				switch (currentEventNumber) 
				{
				case 0:
					if (EventTriggers [currentEventNumber].activeSelf == true) 
					{
						if (EventTriggers [currentEventNumber].GetComponent<ScriptedEventTrigger> ().startedScriptedEvent == true) 
						{
							StartCoroutine (_StartTalk (level1TextFiles [currentEventNumber]));
							EventTriggers [currentEventNumber].SetActive (false);
							currentEventNumber++;
						}
					}
					break;
				case 1:
					if (EventTriggers [currentEventNumber].activeSelf == true) 
					{
						if (EventTriggers [currentEventNumber].GetComponent<ScriptedEventTrigger> ().startedScriptedEvent == true) 
						{
							StartCoroutine (_StartTalkNoMovement (level1TextFiles [currentEventNumber]));
							EventTriggers [currentEventNumber].SetActive (false);
							currentEventNumber++;
						}
					}
					break;
				case 2:
					if (mainCamera.GetComponent<OpenGate>().doneCounter == 1) 
					{
						StartCoroutine (_StartTalk (level1TextFiles [currentEventNumber]));
						currentEventNumber++;
					}
					break;
				case 3:
					if (mainCamera.GetComponent<OpenGate>().doneCounter == 2) 
					{
						StopAllCoroutines ();
						StartCoroutine (_StartTalk (level1TextFiles [currentEventNumber]));
						currentEventNumber++;
					}
					break;
				case 4:
					if (mainCamera.GetComponent<OpenGate>().doneCounter == 0) 
					{
						for (int i = 0; i < puzzleZoomAreaObjects.Length; i++)
							puzzleZoomAreaObjects [i].SetActive (false);
						
						StartCoroutine (_StartTalkNoMovementCameraPan (level1TextFiles [currentEventNumber]));
						currentEventNumber++;
					}
					break;
				}
				if (currentEventNumber == level1TextFiles.Length)
					currentEventNumber = 0;
			}
			break;
		case 2: 



			break;




		case 3:
			break;
		}
	
	}
	void OnDisable()
	{
		Teleporter.OnTeleport -= IncreaseLevels;
	}
	void OnEnable()
	{
		Teleporter.OnTeleport += IncreaseLevels;
	}
	void IncreaseLevels()
	{
		currentLevel++;
	}

	IEnumerator _StartTalk(TextAsset theTextFile)
	{
		//Debug.Log ("TEST");
		textBox.enabled = true;

		textBox.setTextAndPrepareForOutput(theTextFile);
		textBox.startText = true;
		while (textBox.startText == true) 
		{
			yield return null;
		}
		//textBox.startText = false;
		textBox.startText = false;
		yield return null;
	}
	IEnumerator _StartTalkNoMovement(TextAsset theTextFile)
	{
		playerCont.enabled = false;
		playerAnimator.enabled = false;
		textBox.setTextAndPrepareForOutput(theTextFile);
		textBox.startText = true;
		while (textBox.startText == true) 
		{
			yield return null;
		}
		playerAnimator.enabled = true;
		playerCont.enabled = true;
		yield return null;
	}
	IEnumerator _StartTalkNoMovementCameraPan(TextAsset theTextFile)
	{
		mainCamera.GetComponent<CameraController> ().enabled = false;
		playerCont.enabled = false;
		playerAnimator.enabled = false;
		textBox.setTextAndPrepareForOutput(theTextFile);
		textBox.startText = true;
		//wait until the dialogue is finished
		while (textBox.startText == true) 
		{
			yield return null;
		}
		//pan camera to teleporter
		while (teleporter == null) 
		{
			teleporter = GameObject.FindGameObjectWithTag ("Teleporter");
			Debug.Log (teleporter);
			yield return null;
		}

		while ((Vector2)mainCamera.transform.position != (Vector2)teleporter.transform.position) 
		{
			mainCamera.transform.position = Vector3.MoveTowards (new Vector3(mainCamera.transform.position.x,mainCamera.transform.position.y,mainCamera.transform.position.z)
				, new Vector3(teleporter.transform.position.x,teleporter.transform.position.y,mainCamera.transform.position.z), Time.deltaTime*2);
			yield return null;
		}
		textBox.setTextAndPrepareForOutput(level1TextFiles[level1TextFiles.Length-1]);
		textBox.startText = true;
		//wait until the dialogue is finished
		while (textBox.startText == true) 
		{
			yield return null;
		}
		playerAnimator.enabled = true;
		playerCont.enabled = true;
		mainCamera.GetComponent<CameraController> ().enabled = true;
		yield return null;
	}

}
