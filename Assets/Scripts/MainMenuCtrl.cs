using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuCtrl : MonoBehaviour 
{
	public GameObject QuitConfirmationCanvas;
	// Use this for initialization
	void Start () 
	{
        QuitConfirmationCanvas = GameObject.Find("QuitConfirmationCanvas");
        QuitConfirmationCanvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
public void LoadScene(string name) {
	SceneManager.LoadScene(name); //Application.LoadLevel() is obsolete
		    					}
public void ConfirmQuit()
	{
        QuitConfirmationCanvas.SetActive(true);
	}
    public void ConfirmQuitNo()
    {
        Debug.Log("PRESSED NO");
        QuitConfirmationCanvas.SetActive(false);
    }
    public void Quit()
    {
        Debug.Log("PRESSED YES");
        Application.Quit ();
    }
}
