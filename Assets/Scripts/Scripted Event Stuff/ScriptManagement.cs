using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScriptManagement : MonoBehaviour 
{
	public int currentLevel;
	public int currentEventNumber;
	public GameObject[] tilesToDeactivate;
	public GameObject[] level1EventTriggers;
	public GameObject[] level2EventTriggers;
	public GameObject[] level3EventTriggers;
	public GameObject[] level4EventTriggers;
	public TextAsset[] level1TextFiles;
	public TextAsset[] level2TextFiles;
	public TextAsset[] level3TextFiles;
	public TextAsset[] level4TextFiles;
	public ScriptManagerNewTextBox textBox;
	private GameObject player;
	private PlayerController playerCont;
	private Animator playerAnimator;
	private Camera mainCamera;
	public GameObject teleporter;
	public GameObject[] puzzleZoomAreaObjects;
	public GameObject hubDoor;
	public GameObject FinalPuzzleButton;
	private FinalPuzzleController FinalPuzzleCont;
	// Use this for initialization
	void Start () 
	{
		FinalPuzzleCont = FinalPuzzleButton.GetComponent<FinalPuzzleController> ();
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
					if (level1EventTriggers [currentEventNumber].activeSelf == true) {
						if (level1EventTriggers [currentEventNumber].GetComponent<ScriptedEventTrigger> ().startedScriptedEvent == true) {
							StartCoroutine (_StartTalk (level1TextFiles [currentEventNumber]));
							level1EventTriggers [currentEventNumber].SetActive (false);
							currentEventNumber++;
						}
					}
					break;
				case 1:
					if (level1EventTriggers [currentEventNumber].activeSelf == true) {
						if (level1EventTriggers [currentEventNumber].GetComponent<ScriptedEventTrigger> ().startedScriptedEvent == true) {
							StartCoroutine (_StartTalkNoMovement (level1TextFiles [currentEventNumber]));
							level1EventTriggers [currentEventNumber].SetActive (false);
							currentEventNumber++;
						}
					}
					break;
				case 2:
					if (mainCamera.GetComponent<OpenGate> ().doneCounter == 1) {
						StartCoroutine (_StartTalk (level1TextFiles [currentEventNumber]));
						currentEventNumber++;
					}
					break;
				case 3:
					if (mainCamera.GetComponent<OpenGate> ().doneCounter == 2) {
						StopAllCoroutines ();
						StartCoroutine (_StartTalk (level1TextFiles [currentEventNumber]));
						currentEventNumber++;
					}
					break;
				case 4:
					if (mainCamera.GetComponent<OpenGate> ().doneCounter == 0) {
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
			{
				switch (currentEventNumber) 
				{
					case 0:
						if (level2EventTriggers [currentEventNumber].activeSelf == true) 
						{
							if (level2EventTriggers [currentEventNumber].GetComponent<ScriptedEventTrigger> ().startedScriptedEvent == true) 
							{
								StartCoroutine (_StartTalkNoMovement (level2TextFiles [currentEventNumber]));
								level2EventTriggers [currentEventNumber].SetActive (false);
								currentEventNumber++;
								hubDoor.GetComponent<HubDoor> ().Deactivate ();
							}
						}
						break;
					case 1:
						if (mainCamera.GetComponent<OpenGate> ().doneCounter == 1) {
							StartCoroutine (_StartTalk (level2TextFiles [currentEventNumber]));
							currentEventNumber++;
						}

						break;
					case 2:
						if (mainCamera.GetComponent<OpenGate> ().doneCounter == 2) {
							StartCoroutine (_StartTalk (level2TextFiles [currentEventNumber]));
							currentEventNumber++;
						}

						break;
					case 3:
						if (mainCamera.GetComponent<OpenGate> ().doneCounter == 0) 
						{
							puzzleZoomAreaObjects = GameObject.FindGameObjectsWithTag ("PuzzleZoomArea");
							for (int i = 0; i < puzzleZoomAreaObjects.Length; i++)
								puzzleZoomAreaObjects [i].SetActive (false);

							StartCoroutine (_StartTalkNoMovementCameraPanNoTextAfterCameraMovement (level2TextFiles [currentEventNumber]));
							currentEventNumber++;
						}

						break;

				}
				if (currentEventNumber == level2TextFiles.Length)
					currentEventNumber = 0;
			}
				break;
		case 3:
			{
				switch (currentEventNumber) 
				{
				case 0:
					if (level3EventTriggers [currentEventNumber].activeSelf == true) 
					{
						if (level3EventTriggers [currentEventNumber].GetComponent<ScriptedEventTrigger> ().startedScriptedEvent == true) 
						{
							StartCoroutine (_StartTalkNoMovement (level3TextFiles [currentEventNumber]));
							level3EventTriggers [currentEventNumber].SetActive (false);
							currentEventNumber++;
							hubDoor.GetComponent<HubDoor> ().Deactivate ();
						}
					}
					break;
				case 1:
					if (mainCamera.GetComponent<OpenGate> ().doneCounter == 1) {
						StartCoroutine (_StartTalk (level3TextFiles [currentEventNumber]));
						currentEventNumber++;
					}
					break;
				case 2:
					if (mainCamera.GetComponent<OpenGate> ().doneCounter == 2) {
						StartCoroutine (_StartTalk (level3TextFiles [currentEventNumber]));
						currentEventNumber++;
					}
					break;
				case 3:
					if (mainCamera.GetComponent<OpenGate> ().doneCounter == 0) 
					{
						puzzleZoomAreaObjects = GameObject.FindGameObjectsWithTag ("PuzzleZoomArea");
						for (int i = 0; i < puzzleZoomAreaObjects.Length; i++)
							puzzleZoomAreaObjects [i].SetActive (false);

						StartCoroutine (_StartTalkNoMovementCameraPanNoTextAfterCameraMovement (level3TextFiles [currentEventNumber]));
						currentEventNumber++;
					}
					break;
				}
				if (currentEventNumber == level3TextFiles.Length)
					currentEventNumber = 0;
			}
			break;
		case 4:
			{
				switch (currentEventNumber) 
				{
				case 0:
					if (level4EventTriggers [currentEventNumber].activeSelf == true) 
					{
						if (level4EventTriggers [currentEventNumber].GetComponent<ScriptedEventTrigger> ().startedScriptedEvent == true) 
						{
							StartCoroutine (_FinalPuzzleStartTalkNoMovement (level4TextFiles [currentEventNumber]));
							level4EventTriggers [currentEventNumber].SetActive (false);
							currentEventNumber++;
							//hubDoor.GetComponent<HubDoor> ().Deactivate ();
						}
					}
					break;
				case 1:
					if (FinalPuzzleCont.FinalPuzzleDone == false && FinalPuzzleCont.FinalPuzzleReady == true) {
						StartCoroutine (_StartFinalCutscene());
						currentEventNumber++;
					}
					break;
				case 2:
					break;
				}
				//if (currentEventNumber == level4TextFiles.Length)
				//	currentEventNumber = 0;
			}
			break;
		}//end switchcase(currentLeveL)
	}//end Update
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
	IEnumerator _StartTalkNoMovementCameraPanNoTextAfterCameraMovement(TextAsset theTextFile)
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
		playerAnimator.enabled = true;
		playerCont.enabled = true;
		mainCamera.GetComponent<CameraController> ().enabled = true;
		yield return null;
	}
	IEnumerator _StartFinalCutscene()
	{
		//playerCont.enabled = false;
		//playerAnimator.enabled = false;
		//Tiles open up
		//for (int i = 0; i < tilesToDeactivate.Length - 1; i++) {
		//	tilesToDeactivate [i].SetActive (false);
		//}
		//Player falls down
		//yield return new WaitForSeconds(5);
		//playerCont.enabled = true;
		//playerAnimator.enabled = true;

		//for (int i = 0; i < tilesToDeactivate.Length - 1; i++) {
		//	tilesToDeactivate [i].SetActive (true);
		//}
		while (FinalPuzzleCont.FinalPuzzleDone == false) 
		{
			yield return new WaitForSeconds (1);
		}
		SceneManager.LoadScene (1);
		yield return null;
	}

	IEnumerator _FinalPuzzleStartTalkNoMovement(TextAsset theTextFile)
	{
		playerCont.enabled = false;
		playerAnimator.enabled = false;
		textBox.setTextAndPrepareForOutput(theTextFile);
		textBox.startText = true;
		while (textBox.startText == true) 
		{
			yield return null;
		}
		//Deactivate tiles
		for (int i = 0; i < tilesToDeactivate.Length - 1; i++) {
			tilesToDeactivate [i].SetActive (false);
		}
		//Player falls down
		yield return new WaitForSeconds(5);
		//Activate Tiles
		for (int i = 0; i < tilesToDeactivate.Length - 1; i++) {
			tilesToDeactivate [i].SetActive (true);
		}
		playerAnimator.enabled = true;
		playerCont.enabled = true;
		FinalPuzzleCont.FinalPuzzleReady = true;
		yield return null;
	}

}
