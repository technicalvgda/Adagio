using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuCtrl : MonoBehaviour 
{
	public GameObject MainMenuCanvas;
	public GameObject QuitConfirmationCanvas;
	public GameObject OptionsCanvas;
	// Use this for initialization
	void Start () 
	{
		MainMenuCanvas = GameObject.Find ("MainMenuCanvas");
        QuitConfirmationCanvas = GameObject.Find("QuitConfirmationCanvas");
        QuitConfirmationCanvas.SetActive(false);
		OptionsCanvas = GameObject.Find("OptionsCanvas");
		OptionsCanvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	public void EnterOptions(){
		OptionsCanvas.SetActive (true);
		MainMenuCanvas.SetActive (false);
	}

	public void GoBacktoMainMenu(){
		OptionsCanvas.SetActive (false);
		MainMenuCanvas.SetActive (true);
	}

public void LoadScene(string name) {
	SceneManager.LoadScene(name); //Application.LoadLevel() is obsolete
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
