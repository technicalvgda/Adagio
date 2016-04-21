using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Manages the flow of the screens, holds the public functions to be called by buttons
// spr_: A sprite
// menu_: A set of UI elements
// scrn_: A menu on top of a background
public class AdagioTitleScreen : MonoBehaviour
{
    [Header("Graphics")]
    public SpriteRenderer spriteFade;

    [Header("Audio")]
    public AdagioAudio audioCtrl;

    [Header("Sequence")]
    public GameObject objIntro;
    public GameObject objButtonBar;
    public GameObject objLogo;

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
        objButtonBar.SetActive(true);
        yield return null;
        objIntro.SetActive(false);
        objButtonBar.SetActive(false);

        yield return null;

        // Remove curtain
        spriteFade.color = Color.clear;

        for (int i = 0; i < 60; i++) yield return null;
    }

    IEnumerator _Run()
    {
        yield return _ForceLoad();
        for (int i = 0; i < 60; i++) yield return null;

        objIntro.SetActive(true);
        audioCtrl.PlayBgm(0, true);

        for (int i = 0; i < 1480; i++)
        {
            if (Input.anyKeyDown)
            {
                break;
            }
            yield return null;
        }

        // Fade to the logo
        {
            while (spriteFade.color.a < 1)
            {
                spriteFade.color += new Color(0.05f, 0.05f, 0.05f, 0.05f);
                yield return null;
            }
            spriteFade.color = Color.white;

            objIntro.SetActive(false);
            objLogo.SetActive(true);

            for (int j = 0; j < 60; j++)
                yield return null;
            while (spriteFade.color.a > 0)
            {
                spriteFade.color -= new Color(0.02f, 0.02f, 0.02f, 0.02f);
                yield return null;
            }
            spriteFade.color = Color.clear;
        }

        objButtonBar.SetActive(true);

        for (int i = 0; i < 30; i++) yield return null;

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
