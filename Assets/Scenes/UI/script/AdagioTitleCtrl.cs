using UnityEngine;
using System.Collections;

// Manages the flow of the screens, holds public functions to be called by buttons

public class AdagioTitleCtrl : MonoBehaviour
{
    [Header("Graphics")]
    public SpriteRenderer spriteFade;

    [Header("Audio")]
    public AdagioAudio audioCtrl;

    [Header("Sequence")]
    public GameObject objIntro;
    public GameObject objLogo;
    public GameObject objButtonBar;

    [Header("First menu")]
    public AdagioMenu menuTitle;
    AdagioMenu menuCurrent;

    void Start()
    {
        Init();
        StartCoroutine(_Run());
    }

    void Init()
    {
        // Unfreezes time
        Time.timeScale = 1f;

        // Sets the camera's size to match that of the canvas
        // This makes the sprites appear correctly
        // The value is 3.6 and can be set in the Camera object itself
        const int scrnHeight = 720;
        const int ppu = 100;
        FindObjectOfType<Camera>().orthographicSize = (scrnHeight / 2.0f) / ppu;
    }

    //Coroutine routine;
    //void SetRoutine(IEnumerator arg)
    //{
    //    if (routine != null) StopCoroutine(routine);
    //    routine = StartCoroutine(arg);
    //}

    IEnumerator _ForceLoad()
    {
        // fixes some fuckery in the editor where a GIANT object would
        // cause the game to flinch and desync when it's first enabled,
        // probably not necessary in the final build

        // Put a curtain up so that nobody sees what's happening
        spriteFade.color = Color.black;
        
        yield return null;

        // Enable everything
        // Disable everything
        objIntro.SetActive(true);
        objLogo.SetActive(true);
        objButtonBar.SetActive(true);
        yield return null;
        objIntro.SetActive(false);
        objLogo.SetActive(false);
        objButtonBar.SetActive(false);

        yield return null;

        // Remove curtain
        spriteFade.color = Color.clear;

        for (int i = 0; i < 60; i++) yield return null;
    }

    WaitForSeconds delay05s = new WaitForSeconds(0.5f);
    WaitForSeconds delay1s = new WaitForSeconds(1);
    IEnumerator _Run()
    {
        //yield return _ForceLoad();
        yield return delay05s;

        objIntro.SetActive(true);
        audioCtrl.PlayBgm(0, true);

        // Intro finishes after [1643 frames], the logo should be enabled around here

        yield return new WaitForSeconds(2.5f); // +150

        for (int i = 0; i < 1380; i++) // +1380
        {
            if (Input.anyKeyDown)
            {
                yield return SkipIntro();
                yield return LogoSequence();
                yield break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(2); // +120
        yield return LogoSequence();
    }

    IEnumerator SkipIntro()
    {
        while (spriteFade.color.a < 1)
        {
            spriteFade.color += new Color(0, 0, 0, 0.03f);
            yield return null;
        }
        spriteFade.color = Color.black;

        objIntro.SetActive(false);

        while (spriteFade.color.a > 0)
        {
            spriteFade.color -= new Color(0, 0, 0, 0.1f);
            yield return null;
        }
        spriteFade.color = Color.clear;
    }

    IEnumerator LogoSequence()
    {
        objLogo.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        objIntro.SetActive(false);

        objButtonBar.SetActive(true);

        yield return delay05s;

        menuTitle.Enter();
        menuCurrent = menuTitle;
    }

    public void TransitionToMenu(AdagioMenu menu)
    {
        menuCurrent.Exit();
        menu.Enter();
        menuCurrent = menu;
    }

    public void Quit()
    {
        print("User quits");
        Application.Quit();
    }
}
