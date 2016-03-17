using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject objFade;
    public GameObject objText;
    public GameObject objButtons;
    public GameObject OptionsCanvas;
    Image imgFade;
    bool busy;
    bool paused;

    // Use this for initialization
    void Start ()
    {
        OptionsCanvas = GameObject.Find("OptionsCanvas");
        imgFade = objFade.GetComponent<Image>();
        OptionsCanvas.SetActive(false);
        objText.SetActive(false);
        objButtons.SetActive(false);
    }
	
    // Update is called once per frame
    void Update ()
    {
        if (!busy && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
        {
            // Prevent input until transitioning is over
            busy = true;
            paused = !paused;
            if (paused)
                StartCoroutine(_TransitionIn());
            else
                StartCoroutine(_TransitionOut());
        }
    }

    IEnumerator _TransitionIn()
    {
        // Freeze the game
        Time.timeScale = 0;

        // Fade in over 10 frames
        imgFade.color = Color.clear;
        for (int i = 0; i < 10; i++)
        {
            imgFade.color += new Color(0, 0, 0, 0.04f);
            yield return null;
        }
        imgFade.color = new Color(0, 0, 0, 0.4f);

        // Show the PAUSED text and the buttons
       
        objText.SetActive(true);
        objButtons.SetActive(true);

        // Allow input again
        busy = false;
    }

    IEnumerator _TransitionOut()
    {
        // Resume the game
        Time.timeScale = 1;

        objText.SetActive(false);
        objButtons.SetActive(false);

        // Fade out over 10 frames
        imgFade.color = new Color(0, 0, 0, 0.4f);
        for (int i = 0; i < 10; i++)
        {
            imgFade.color -= new Color(0, 0, 0, 0.04f);
            yield return null;
        }
        imgFade.color = Color.clear;

        // Allow input again
        busy = false;
    }

    // This method is called by the Resume button
    public void Resume()
    {
        if (!busy)
        {
            busy = true;
            paused = !paused;
            StartCoroutine(_TransitionOut());
        }
    }
    //Opens up OptionsCanvas, disables the pause menu while the options menu is up
    public void Settings()
    {
        if (!busy)
        {
            OptionsCanvas.SetActive(true);
            busy = true;
        }
    }
    //Mapped to exit button for options menu
    public void closeSettings()
    {
        OptionsCanvas.SetActive(false);
        busy = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
