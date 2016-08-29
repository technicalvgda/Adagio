using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuCtrl : MonoBehaviour 
{
	public GameObject MainMenuCanvas;
	public GameObject QuitConfirmationCanvas;
	public GameObject OptionsCanvas;

    public Slider BrightnessSlider;

    public AudioControl audioControl;

    // Use this for initialization
    void Start () 
	{
		MainMenuCanvas = GameObject.Find("MainMenuCanvas");


        QuitConfirmationCanvas = GameObject.Find("QuitConfirmationCanvas");
        QuitConfirmationCanvas.SetActive(false);
		OptionsCanvas = GameObject.Find("OptionsCanvas");
		OptionsCanvas.SetActive(false);


        audioControl.LoopMusic(true);
        audioControl.PlayMusic(0);
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
        SceneManager.LoadSceneAsync(name);
        MainMenuCanvas.SetActive(false);
        SceneManager.LoadScene(name); //Application.LoadLevel() is obsolete
    }

    // Change the brightness by changing the ambient light
    // The ambient light can physically be found in Window -> Lighting -> Ambient Color
    // Only sprites with Material set to Default-Diffuse are affected by ambient color (see Preview object)
    public void UpdateBrightness()
    {
        // Set the ambient light to the slider's value
        float gamma = BrightnessSlider.value;
        RenderSettings.ambientLight = new Color(gamma, gamma, gamma, 1);
    }

    public void ConfirmQuit()
	{
        QuitConfirmationCanvas.SetActive (true);
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
