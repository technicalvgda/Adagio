using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuCtrl : MonoBehaviour 
{	
	private PauseGame paused;
	public GameObject MainMenuCanvas;
	public GameObject QuitConfirmationCanvas;
	public GameObject OptionsCanvas;
	public bool isTitleScreen;
    public AudioControl audio;

    // Use this for initialization
    void Awake () 
	{
		MainMenuCanvas = GameObject.Find("MainMenuCanvas");
		paused = GetComponent<PauseGame> ();
        QuitConfirmationCanvas = GameObject.Find("QuitConfirmationCanvas");
        QuitConfirmationCanvas.SetActive(false);
		OptionsCanvas = GameObject.Find("OptionsCanvas");
		OptionsCanvas.SetActive(false);

        audio.LoopMusic(true);
        audio.PlayMusic(0);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void EnterOptions()
    {
		OptionsCanvas.SetActive (true);
		MainMenuCanvas.SetActive (false);
	}

	public void GoBacktoMainMenu()
    {
		OptionsCanvas.SetActive (false);
		MainMenuCanvas.SetActive (true);
	}

    public void LoadScene(string name)
    {
		if(isTitleScreen){
	    SceneManager.LoadScene(name); //Application.LoadLevel() is obsolete
		} else {
			paused.resumeGame ();
		}
    }

    public void ConfirmQuit()
	{
        QuitConfirmationCanvas.SetActive(true);
		MainMenuCanvas.SetActive (false);
	}

    public void ConfirmQuitNo()
    {
        Debug.Log("PRESSED NO");
        QuitConfirmationCanvas.SetActive(false);
		MainMenuCanvas.SetActive (true);
    }

    public void Quit()
    {
        Debug.Log("PRESSED YES");
        Application.Quit ();
    }
}
