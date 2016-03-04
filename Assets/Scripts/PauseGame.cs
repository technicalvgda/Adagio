using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {
	private TimeManager timeManager;
	private MainMenuCtrl mainMenu;
	public bool gameStarted;
	// Use this for initialization
	void Awake () {
		
		timeManager = GetComponent<TimeManager> ();
		mainMenu = GetComponent<MainMenuCtrl> ();
	}

	void Start () {
		if (gameStarted) {
			mainMenu.MainMenuCanvas.SetActive (false);
		} else {
			mainMenu.MainMenuCanvas.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (gameStarted && Time.timeScale != 0) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				pauseGame ();
			}
		}

		if (!gameStarted && Time.timeScale == 0) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				resumeGame ();

			}
		}
	}

	public void setgameStarted(bool start) {
		gameStarted = start;
	}

	public void pauseGame(){
		gameStarted = false;
		mainMenu.MainMenuCanvas.SetActive (true);
		timeManager.ManipulateTime (0, 0.1f);
	}

	public void resumeGame(){
		gameStarted = true;
		timeManager.ManipulateTime (1, 0.1f);
		mainMenu.MainMenuCanvas.SetActive (false);
		mainMenu.OptionsCanvas.SetActive (false);
		mainMenu.QuitConfirmationCanvas.SetActive (false);
	}
}
